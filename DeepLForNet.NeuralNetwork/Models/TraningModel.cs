using DeepLForNet.NeuralNetwork.Models.Contracts;
using System.Text.Json.Serialization;

namespace DeepLForNet.NeuralNetwork.Models {
    internal class TraningModel : ITraningModel {
        public INeuralNetwork Network { get; set; }

        [JsonIgnore]
        public int Result { get; set; }
    }
}
