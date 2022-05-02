using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLForNet.NeuralNetwork.Models.Contracts {
    internal interface IOutputNeuron : INeuron {
        public string Output { get; set; }
    }
}
