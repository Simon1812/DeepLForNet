using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLForNet.NeuralNetwork.Models.Contracts {
    internal interface ILayer {
        int Layernumber { get; set; }
        List<INeuron> Neurons { get; set; }
    }
}
