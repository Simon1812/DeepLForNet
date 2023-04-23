using System;
using System.Collections.Generic;
using System.Text;

namespace DeepLForNet.NeuralNetwork.Models.Contracts {
    internal interface IPixelFile {
        public int MagicNumber { get; set; }
        public int NumberOfItems { get; set; }
        public int Rows { get; set; }
        public int Columns { get; set; }
        public List<List<double>> ImageGreyValues { get; set; }
    }
}
