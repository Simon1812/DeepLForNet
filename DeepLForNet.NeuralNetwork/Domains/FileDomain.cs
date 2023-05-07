using DeepLForNet.NeuralNetwork.Domains.Contracts;
using DeepLForNet.NeuralNetwork.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DeepLForNet.NeuralNetwork.Domains {
    internal class FileDomain : IFileDomain {

        public PixelFile GetPixelFile() {
            var pixelFile = new PixelFile();

            var imagePixelData = File.ReadAllBytes(@"..\..\..\..\TestData\train-images.idx3-ubyte");

            pixelFile.MagicNumber = GetIntFromArrayAtPostion(imagePixelData, 3);
            pixelFile.NumberOfItems = GetIntFromArrayAtPostion(imagePixelData, 7);
            pixelFile.Rows = GetIntFromArrayAtPostion(imagePixelData, 11);
            pixelFile.Columns = GetIntFromArrayAtPostion(imagePixelData, 15);

            var countPerFile = pixelFile.Rows * pixelFile.Columns;
            var imageGreyValues = new List<double>();
            for (int i = 16; i < imagePixelData.Length; i++) {
                var greyvalue = imagePixelData[i] / 255d;
                imageGreyValues.Add(greyvalue);

                if (imageGreyValues.Count >= countPerFile) {
                    pixelFile.ImageGreyValues.Add(imageGreyValues);
                    imageGreyValues = new List<double>();
                }
            }

            return pixelFile;
        }

        public LabelFile GetLabelFile() {
            var labelFile = new LabelFile();

            var imageData = File.ReadAllBytes(@"..\..\..\..\TestData\train-labels.idx1-ubyte");

            labelFile.MagicNumber = GetIntFromArrayAtPostion(imageData, 3);
            labelFile.NumberOfItems = GetIntFromArrayAtPostion(imageData, 7);

            for (int i = 8; i < imageData.Length; i++) {
                var label = (short)imageData[i];
                if (label < 0 || label > 10) {
                    Console.WriteLine($"Wrong number at Index {i}");
                } else {
                    labelFile.ImagesNumbers.Add(label);
                }
            }

            return labelFile;
        }

        private int GetIntFromArrayAtPostion(byte[] array, int endPos) {
            var bytes = new byte[] { array[endPos], array[--endPos], array[--endPos], array[--endPos] };
            return BitConverter.ToInt32(bytes);
        }
    }
}
