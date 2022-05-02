using DeepLForNet.NeuralNetwork.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DeepLForNet.NeuralNetwork.Models.Contracts {
    interface IOutputLayer {
        OutputFunctions Functions { get; set; }
        [JsonIgnore]
        string Output { get; set; }
        List<OutputNeuron> Neurons { get; set; }
        int Layernumber { get; set; }
    }
}
