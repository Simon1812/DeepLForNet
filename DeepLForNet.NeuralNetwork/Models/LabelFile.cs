using DeepLForNet.NeuralNetwork.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLForNet.NeuralNetwork.Models {
    internal class LabelFile : ILabelFile {
        public LabelFile() {
            ImagesNumbers = new List<short>();
        }
        public int MagicNumber { get; set; }
        public int NumberOfItems { get; set; }
        public List<short> ImagesNumbers { get; set; }
    }
}
