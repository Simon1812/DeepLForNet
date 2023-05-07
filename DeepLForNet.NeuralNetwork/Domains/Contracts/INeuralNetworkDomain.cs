using DeepLForNet.NeuralNetwork.Models;
using DeepLForNet.NeuralNetwork.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLForNet.NeuralNetwork.Domains.Contracts {
    internal interface INeuralNetworkDomain {

        public TraningModel GetTraningModel(int inputLayerNeuronCount, Dictionary<int, Dictionary<Activationfunctions, int>> Layers, OutputFunctions outputFunctions, List<string> possibleOutputs);
        public string Calcute(TraningModel traningModel, List<double> inputvalues);
        public TraningModel Backpropagtion(TraningModel traningModel);
        public void UpdateNetwork(TraningModel traningModel);
        public string Save(TraningModel traningModel);
        public TraningModel Load(string json);
        public TraningModel DeepCopy(TraningModel traningModel);
    }
}
