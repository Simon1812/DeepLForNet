using DeepLForNet.NeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLForNet.NeuralNetwork.Domains.Contracts {
    internal interface IFileDomain {
        public LabelFile GetLabelFile();
        public PixelFile GetPixelFile();
    }
}
