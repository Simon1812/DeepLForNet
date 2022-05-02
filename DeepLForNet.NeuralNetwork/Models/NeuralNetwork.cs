using DeepLForNet.NeuralNetwork.Models.Contracts;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DeepLForNet.NeuralNetwork.Models {
    internal class NeuralNetwork : INeuralNetwork {
        public NeuralNetwork() {
            Layers = new List<ILayer>();
            OutputLayer = new OutputLayer();
            Errors = new List<double>();
            Inputs = new List<double>();
            Success = new List<bool>();
            Mutation = 0;
        }

        public List<ILayer> Layers { get; set; }
        public double RateOfSucces { get; set; }
        public double Error { get; set; }
        public IOutputLayer OutputLayer { get; set; }
        public int Mutation { get; set; }

        public readonly double Learningrate = 0.3d;

        [JsonIgnore]
        public List<double> Errors { get; set; }

        [JsonIgnore]
        public List<bool> Success { get; set; }
        [JsonIgnore]
        public List<double> Inputs { get; set; }
    }
}
