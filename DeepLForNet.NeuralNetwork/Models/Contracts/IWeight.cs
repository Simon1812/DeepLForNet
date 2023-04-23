
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DeepLForNet.NeuralNetwork.Models.Contracts {
    internal interface IWeight {
        double WeightValue { get; set; }
        int Layer { get; set; }
        int Position { get; set; }
        [JsonIgnore]
        public List<double> WeightSmudges { get; set; }
    }
}
