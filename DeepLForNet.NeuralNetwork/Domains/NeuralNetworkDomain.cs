using DeepLForNet.NeuralNetwork.Domains.Contracts;
using DeepLForNet.NeuralNetwork.Models;
using DeepLForNet.NeuralNetwork.Models.Contracts;
using DeepLForNet.NeuralNetwork.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeepLForNet.NeuralNetwork.Domains {
    internal class NeuralNetworkDomain : INeuralNetworkDomain {

        private Random _random;

        public NeuralNetworkDomain() {
            _random = new Random();
        }


        /// <summary>
        /// Creating a TrainingModel incl. NeuralNet
        /// </summary>
        /// <param name="inputLayerNeuronCount"></param>
        /// <param name="Layers"></param>
        /// <returns></returns>
        public TraningModel GetTraningModel(int inputLayerNeuronCount, Dictionary<int, Dictionary<Activationfunctions, int>> Layers, OutputFunctions outputFunctions, List<string> possibleOutputs) {
            var traningModel = new TraningModel();
            traningModel.Network = new NeuralNetwork.Models.NeuralNetwork();

            var lastLayerNeuronCount = inputLayerNeuronCount;
            foreach (var layer in Layers.OrderBy(x => x.Key)) {
                traningModel.Network.Layers.Add(CreateLayer(layer.Key, layer.Value, lastLayerNeuronCount, out lastLayerNeuronCount));
            }


            traningModel.Network.OutputLayer = CreateOutputLayer(traningModel.Network.Layers.Count(), outputFunctions, lastLayerNeuronCount, possibleOutputs);

            return traningModel;
        }

        public string Calcute(TraningModel traningModel, List<double> inputvalues) {
            traningModel.Network.Inputs = inputvalues;

            for (int i = 0; i < traningModel.Network.Layers.Count; i++) {
                var layer = traningModel.Network.Layers[i];
                //Special case layer 1 uses input as Activation of the perivouse layer
                if (layer.Layernumber == 1) {
                    Parallel.ForEach(layer.Neurons, neuron => {
                        var activation = 0d;

                        //Summe of all Activation of the pervious Layer multiplied by corresponding weight
                        for (int w = 0; w < inputvalues.Count; w++) {
                            activation += inputvalues[w] * neuron.Weights[w].WeightValue;
                        }

                        //Calculate the base activation
                        neuron.Z = activation + neuron.Basis;
                        //Calcution of the activation of the current Neuron
                        neuron.Activation = GetActivationFunctionResult(neuron);
                    });
                } else {
                    //Calculate the hidden layer

                    //Get the last layer
                    var previousLayer = traningModel.Network.Layers[i - 1];

                    Parallel.ForEach(layer.Neurons, neuron => {
                        var activation = 0d;

                        //Summe of all Activation of the pervious Layer multiplied by corresponding weight
                        for (int w = 0; w < previousLayer.Neurons.Count; w++) {
                            activation += previousLayer.Neurons[w].Activation * neuron.Weights[w].WeightValue;
                        }

                        //Calculate the base activation
                        neuron.Z = activation + neuron.Basis;
                        //Calcution of the activation of the current Neuron
                        neuron.Activation = GetActivationFunctionResult(neuron);
                    });
                }
            }

            var outputLayer = traningModel.Network.OutputLayer;
            var lastLayer = traningModel.Network.Layers.LastOrDefault();

            if (lastLayer != null) {
                Parallel.ForEach(outputLayer.Neurons, neuron => {
                    var activation = 0d;

                    //Summe of all Activation of the pervious Layer multiplied by corresponding weight
                    for (int w = 0; w < lastLayer.Neurons.Count; w++) {
                        activation += lastLayer.Neurons[w].Activation * neuron.Weights[w].WeightValue;
                    }

                    //Calculate the base activation
                    neuron.Z = activation + neuron.Basis;
                    ////Calcution of the activation of the current Neuron
                    //neuron.Activation = GetActivationFunctionResult(neuron);
                });
            }



            var result = CalcuteOutputLayerActivation(traningModel.Network);
            traningModel.Network.Success.Add(result == traningModel.Result.ToString());

            var error = 0d;
            foreach (var neuron in outputLayer.Neurons) {
                if (neuron.Output == traningModel.Result.ToString()) {
                    neuron.CurrentDesiredValue = 1;
                    error += Math.Pow(1 - neuron.Activation, 2);
                } else {
                    neuron.CurrentDesiredValue = 0;
                    error += Math.Pow(0 - neuron.Activation, 2);
                }
            }
            traningModel.Network.Errors.Add(error);
            traningModel.Network.Error = error;

            //Save(traningModel);

            return result;
        }

        #region Backpropagation

        public TraningModel Backpropagtion(TraningModel traningModel) {
            var lastLayer = traningModel.Network.Layers.LastOrDefault();
            var outputLayerNeurons = traningModel.Network.OutputLayer.Neurons.Count();

            //backwards for output layer
            if (lastLayer != null) {
                Parallel.ForEach(traningModel.Network.OutputLayer.Neurons, outputNeuron => {
                    outputNeuron.Delta = BackprogationOutputFunction(outputNeuron, traningModel.Network.OutputLayer.Functions) / outputLayerNeurons * 2 * (outputNeuron.Activation - outputNeuron.CurrentDesiredValue);
                    outputNeuron.BasisSmudges.Add(outputNeuron.Delta);
                    foreach (var weight in outputNeuron.Weights) {
                        var weightSmudge = outputNeuron.Delta * lastLayer.Neurons[weight.Position].Activation; 
                        lastLayer.Neurons[weight.Position].Delta += weight.WeightValue * outputNeuron.Delta;
                        weight.WeightSmudges.Add(weightSmudge);
                    }
                });
            }

            foreach (var layer in traningModel.Network.Layers.OrderByDescending(x => x.Layernumber)) {
                var previousLayer = traningModel.Network.Layers.FirstOrDefault(x => x.Layernumber == layer.Layernumber - 1);
                if (previousLayer != null) {
                    //backwards for hidden layer to hidden layer
                    BackpropagtionHiddenLayer(layer, previousLayer);
                } else {
                    //Backwards to input layer
                    BackpropagtionHiddenLayer(layer, traningModel.Network.Inputs);
                }
            }

            return traningModel;
        }

        public void UpdateNetwork(TraningModel traningModel) {
            traningModel.Network.RateOfSucces = traningModel.Network.Success.Where(x => x == true).Count() / (double)traningModel.Network.Success.Count();
            foreach (var layer in traningModel.Network.Layers) {
                Parallel.ForEach(layer.Neurons, neuron => {
                    neuron.Basis -= neuron.BasisSmudges.Average() * traningModel.Network.Learningrate;
                    neuron.BasisSmudges.Clear();
                    neuron.Delta = 0;
                    var test = neuron.Weights.Select(x => x.WeightSmudges.Average());
                    foreach (var weight in neuron.Weights) {
                        weight.WeightValue -= weight.WeightSmudges.Average() * traningModel.Network.Learningrate;
                        weight.WeightSmudges.Clear();
                        //weight.WeightValue *= (1 - traningModel.Network.Learningrate);
                    }
                });
            }

            Parallel.ForEach(traningModel.Network.OutputLayer.Neurons, neuron => {
                neuron.Basis -= neuron.BasisSmudges.Average() * traningModel.Network.Learningrate;
                neuron.BasisSmudges.Clear();
                neuron.Delta = 0;
                foreach (var weight in neuron.Weights) {
                    weight.WeightValue -= weight.WeightSmudges.Average() * traningModel.Network.Learningrate;
                    weight.WeightSmudges.Clear();
                    //weight.WeightValue *= (1 - traningModel.Network.Learningrate);
                }
            });
        }

        private void BackpropagtionHiddenLayer(ILayer layer, ILayer previousLayer) {
            if (layer != null) {
                Parallel.ForEach(layer.Neurons, neuron => {
                    neuron.Delta = BackprogationActivationFunction(neuron) * neuron.Delta;
                    neuron.BasisSmudges.Add(neuron.Delta);
                    foreach (var weight in neuron.Weights) {
                        var weightSmudge = neuron.Delta * previousLayer.Neurons[weight.Position].Activation;
                        previousLayer.Neurons[weight.Position].Delta += weight.WeightValue * neuron.Delta;
                        weight.WeightSmudges.Add(weightSmudge);
                    }
                });
            }
        }

        private void BackpropagtionHiddenLayer(ILayer layer, List<double> inputs) {
            if (layer != null) {
                Parallel.ForEach(layer.Neurons, neuron => {
                    neuron.Delta = BackprogationActivationFunction(neuron) * neuron.Delta;
                    neuron.BasisSmudges.Add(neuron.Delta);
                    foreach (var weight in neuron.Weights) {
                        var weightSmudge = neuron.Delta * inputs[weight.Position];
                        weight.WeightSmudges.Add(weightSmudge);
                    }
                });
            }
        }

        private double GetActivationFunctionResult(INeuron neuron) {
            switch (neuron.Activationfunction) {
                case Activationfunctions.None:
                    throw new Exception("Activationfunction must be set");
                case Activationfunctions.Sigmoid:
                    return Sigmoid(neuron.Z);
                case Activationfunctions.TanH:
                    return TanH(neuron.Z);
                case Activationfunctions.ReLU:
                    return ReLu(neuron.Z);
                case Activationfunctions.Identity:
                    return neuron.Z;
                case Activationfunctions.ZeroOne:
                    return 0;
                case Activationfunctions.Softplus:
                    return SoftPuls(neuron.Activation);

                default:
                    return 0;
            }
        }

        private double BackprogationActivationFunction(INeuron neuron) {
            switch (neuron.Activationfunction) {
                case Activationfunctions.None:
                    throw new Exception("Activationfunction must be set");
                case Activationfunctions.Sigmoid:
                    return SigmoidDerivative(neuron.Activation);
                case Activationfunctions.TanH:
                    return TanHDerivative(neuron.Activation);
                case Activationfunctions.ReLU:
                    return ReLuDerivative(neuron.Z);
                case Activationfunctions.Identity:
                    return 1; //  1 = Derivative of Identity
                case Activationfunctions.ZeroOne:
                    return 0;
                case Activationfunctions.Softplus:
                    return SoftPulsDerivative(neuron.Activation);

                default:
                    return 0;
            }
        }

        private double BackprogationOutputFunction(OutputNeuron neuron, OutputFunctions functions) {
            switch (functions) {
                case OutputFunctions.None:
                    throw new Exception("Activationfunction must be set");
                case OutputFunctions.Sigmoid:
                    return SigmoidDerivative(neuron.Activation);
                case OutputFunctions.TanH:
                    return TanHDerivative(neuron.Activation);
                case OutputFunctions.Softmax:
                    return neuron.Activation * (1 - neuron.Activation);

                default:
                    break;
            }


            return 0;
        }

        private string CalcuteOutputLayerActivation(INeuralNetwork network) {
            //var result = outputLayer.OrderByDescending(x => x.Activation).FirstOrDefault();
            switch (network.OutputLayer.Functions) {
                case OutputFunctions.None:
                    break;
                case OutputFunctions.Sigmoid:
                    foreach (var neuron in network.OutputLayer.Neurons) {
                        neuron.Activation = Sigmoid(neuron.Z);
                    }

                    break;
                case OutputFunctions.TanH:
                    foreach (var neuron in network.OutputLayer.Neurons) {
                        neuron.Activation = TanH(neuron.Z);
                    }

                    break;
                case OutputFunctions.Softmax:
                    var baseNum = 0.0;

                    foreach (var neuron in network.OutputLayer.Neurons) {
                        baseNum += Math.Exp(neuron.Z);
                    }

                    foreach (var neuron in network.OutputLayer.Neurons) {
                        if (baseNum == 0) {
                            neuron.Activation = 0;
                        } else {
                            neuron.Activation = Math.Exp(neuron.Z) / baseNum;
                        }
                        if (double.IsNaN(neuron.Activation)) {
                            neuron.Activation = Math.Exp(neuron.Z) / double.MaxValue;
                        }
                    }

                    break;
                default:
                    break;

            }
            return network.OutputLayer.Neurons.OrderByDescending(x => x.Activation).FirstOrDefault().Output;
        }

        #endregion

        private Layer CreateLayer(int layerNumber, Dictionary<Activationfunctions, int> layerInformations, int neuronsInThePreviousLayer, out int neuronCount) {
            var Layer = new Layer();
            Layer.Layernumber = layerNumber;
            var previousLayer = layerNumber - 1;
            Layer.Neurons = new List<INeuron>();
            neuronCount = 0;
            foreach (var layerInformation in layerInformations) {
                for (int i = 1; i <= layerInformation.Value; i++) {
                    var weights = new List<IWeight>();
                    for (int z = 0; z < neuronsInThePreviousLayer; z++) {
                        weights.Add(new Weight {
                            Layer = previousLayer,
                            Position = z,
                            WeightValue = GetNewRandomWeight()
                        });
                    }
                    Layer.Neurons.Add(new Neuron {
                        Basis = 0,
                        Layer = layerNumber,
                        Position = Layer.Neurons.Count,
                        Activationfunction = layerInformation.Key,
                        Weights = weights,
                    });
                }
                neuronCount += layerInformation.Value;
            }
            return Layer;
        }

        private OutputLayer CreateOutputLayer(int layerNumber, OutputFunctions functions, int lastLayerNeuronCount, List<string> possibleOutputs) {
            var previousLayer = layerNumber - 1;

            //Create OutputNeurons
            var neurons = new List<OutputNeuron>();
            var position = 0;
            foreach (var possibleOutput in possibleOutputs) {
                var weights = new List<IWeight>();
                for (int z = 0; z < lastLayerNeuronCount; z++) {
                    weights.Add(new Weight {
                        Layer = previousLayer,
                        Position = z,
                        WeightValue = GetNewRandomWeight()
                    });
                }
                neurons.Add(new OutputNeuron {
                    Basis = 0,
                    Layer = layerNumber,
                    Position = position,
                    Weights = weights,
                    Activationfunction = Activationfunctions.Sigmoid,
                    Output = possibleOutput
                });
                position += 1;
            }

            return new OutputLayer() {
                Layernumber = layerNumber,
                Functions = functions,
                Neurons = neurons
            };
        }

        private double GetNewRandomWeight() {
            var randomvalue = _random.NextDouble();
            var test = _random.Next(2);
            bool sign = test == 1;
            var randomweight = randomvalue;

            if (!sign) {
                randomweight = randomvalue * -1;
            }

            return randomweight;
        }

        private static double Sigmoid(double value) => 1.0 / (1.0 + Math.Pow(Math.E, -value));
        private static double SigmoidDerivative(double x) => x * (1d - x);

        private static double TanH(double value) => Math.Tanh(value);
        private static double TanHDerivative(double result) => 1.0 - result * result;

        private static double Identity(double value) => value;

        private static double ReLuDerivative(double value) {
            if (value <= 0)
                return 0.1;

            return 1;
        }

        private static double ReLu(double value) {
            if (value <= 0)
                return value * 0.1;

            return value;
        }

        private static double SoftPuls(double value) {
            return Math.Log(1 + Math.Pow(Math.E, -value));
        }

        private static double SoftPulsDerivative(double value) => Sigmoid(value);

        public string Save(TraningModel traningModel) {
            return System.Text.Json.JsonSerializer.Serialize(traningModel);
        }

        public TraningModel Load(string json) {
            try {
                return System.Text.Json.JsonSerializer.Deserialize<TraningModel>(json);
            } catch (Exception) {
                return null;
            }
        }

        public TraningModel DeepCopy(TraningModel traningModel) {
            var newModel = new TraningModel();

            var newNetwork = new NeuralNetwork.Models.NeuralNetwork();
            var oldNetwork = traningModel.Network;

            newNetwork.Mutation = oldNetwork.Mutation;
            newNetwork.RateOfSucces = oldNetwork.RateOfSucces;
            newNetwork.Error = oldNetwork.Error;
            newModel.Network = newNetwork;

            foreach (var oldLayer in oldNetwork.Layers) {
                var newlayer = new Layer();
                newlayer.Layernumber = oldLayer.Layernumber;
                newlayer.Neurons = new List<INeuron>();
                foreach (var oldNeuron in oldLayer.Neurons) {
                    var newNeuron = new Neuron();
                    newNeuron.Basis = oldNeuron.Basis;
                    newNeuron.Weights = new List<IWeight>();
                    foreach (var oldWeight in oldNeuron.Weights) {
                        newNeuron.Weights.Add(CopyWeight(oldWeight));
                    }
                    newlayer.Neurons.Add(newNeuron);
                }
                newNetwork.Layers.Add(newlayer);
            }

            newNetwork.OutputLayer = new OutputLayer() {
                Layernumber = oldNetwork.OutputLayer.Layernumber,
                Functions = oldNetwork.OutputLayer.Functions
            };


            foreach (var oldNeuron in oldNetwork.OutputLayer.Neurons) {
                var newNeuron = new OutputNeuron();
                newNeuron.Basis = oldNeuron.Basis;
                newNeuron.Output = oldNeuron.Output;
                newNeuron.Weights = new List<IWeight>();
                foreach (var oldWeight in oldNeuron.Weights) {
                    newNeuron.Weights.Add(CopyWeight(oldWeight));
                }
                newNetwork.OutputLayer.Neurons.Add(newNeuron);
            }

            return newModel;
        }

        private Weight CopyWeight(IWeight oldWeight) {
            return new Weight {
                Position = oldWeight.Position,
                Layer = oldWeight.Layer,
                WeightValue = oldWeight.WeightValue
            };
        }
    }
}