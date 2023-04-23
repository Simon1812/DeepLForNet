using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLForNet.NeuralNetwork.Models.Contracts {
    internal interface ILabelFile {
        public int MagicNumber { get; set; }
        public int NumberOfItems { get; set; }
        public List<short> ImagesNumbers { get; set; }
    }
}
