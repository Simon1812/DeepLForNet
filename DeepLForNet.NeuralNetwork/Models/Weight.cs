using DeepLForNet.NeuralNetwork.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DeepLForNet.NeuralNetwork.Models {
    internal class Weight : IWeight {
        public double WeightValue { get; set; }
        public int Layer { get; set; }
        public int Position { get; set; }
        [JsonIgnore]
        public double CurrentWeightSmudge { get; set; }
        [JsonIgnore]
        public List<double> WeightSmudges { get; set; }

        public Weight() {
            WeightSmudges = new List<double>();
        }
    }
}
