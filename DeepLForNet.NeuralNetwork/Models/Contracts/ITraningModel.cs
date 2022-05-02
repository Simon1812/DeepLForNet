using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLForNet.NeuralNetwork.Models.Contracts {
    internal interface ITraningModel {
        INeuralNetwork Network { get; set; }

        int Result { get; set; }
    }
}
