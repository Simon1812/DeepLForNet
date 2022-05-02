using DeepLForNet.NeuralNetwork.Models.Contracts;
using DeepLForNet.NeuralNetwork.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DeepLForNet.NeuralNetwork.Models {
    internal class OutputLayer : IOutputLayer {
        public OutputFunctions Functions { get; set; }
        [JsonIgnore]
        public string Output { get; set; }
        public List<OutputNeuron> Neurons { get; set; }
        public int Layernumber { get; set; }
    }
}
