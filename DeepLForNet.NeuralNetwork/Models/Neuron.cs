using DeepLForNet.NeuralNetwork.Models.Contracts;
using DeepLForNet.NeuralNetwork.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DeepLForNet.NeuralNetwork.Models {
    internal class Neuron : INeuron {

        public Neuron() {
            Weights = new List<IWeight>();
            BasisSmudges = new List<double>();
        }

        public double Basis { get; set; }
        public int Layer { get; set; }
        public int Position { get; set; }
        public Activationfunctions Activationfunction { get; set; }
        public List<IWeight> Weights { get; set; }

        [JsonIgnore]
        public double Z { get; set; }
        [JsonIgnore]
        public double Delta { get; set; }
        [JsonIgnore]
        public double Activation { get; set; }
        [JsonIgnore]
        public List<double> BasisSmudges { get; set; }
    }
}
