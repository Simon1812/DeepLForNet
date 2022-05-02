using DeepLForNet.NeuralNetwork.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DeepLForNet.NeuralNetwork.Models {
    internal class OutputNeuron : Neuron, IOutputNeuron {
        public string Output { get; set; }

        [JsonIgnore]
        public double CurrentDesiredValue { get; set; }
        [JsonIgnore]
        public double Error { get; set; }
    }
}
