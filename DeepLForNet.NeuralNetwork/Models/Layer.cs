using DeepLForNet.NeuralNetwork.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLForNet.NeuralNetwork.Models {
    internal class Layer : ILayer {
        public List<INeuron> Neurons { get; set; }
        public int Layernumber { get; set; }
    }
}
