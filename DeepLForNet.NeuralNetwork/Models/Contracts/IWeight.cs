
namespace DeepLForNet.NeuralNetwork.Models.Contracts {
    internal interface IWeight {
        double WeightValue { get; set; }
        int Layer { get; set; }
        int Position { get; set; }
    }
}
