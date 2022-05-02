using DeepLForNet.NeuralNetwork.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DeepLForNet.NeuralNetwork.Models.Contracts {
    internal interface INeuron {

        /// <summary>
        /// Layer in which the neuron is placed
        /// </summary>
        int Layer { get; set; }

        /// <summary>
        /// Position with in the layer
        /// </summary>
        int Position { get; set; }

        /// <summary>
        /// All the weights to the previous neurons
        /// </summary>
        List<IWeight> Weights { get; set; }

        /// <summary>
        /// A Nummer which the Summe of all activation*weight-summe should exceed
        /// </summary>
        double Basis { get; set; }

        /// <summary>
        /// Defines which activation funstion is used
        /// </summary>
        Activationfunctions Activationfunction { get; set; }

        /// <summary>
        /// Number between 0 and 1
        /// </summary>
        [JsonIgnore]
        double Activation { get; set; }

        /// <summary>
        /// The weighted sum + Basis befor activation functions is applied
        /// </summary>
        [JsonIgnore]
        double Z { get; set; }

        /// <summary>
        /// The derivative of the current Neuron 
        /// </summary>
        [JsonIgnore]
        double Delta { get; set; }

        /// <summary>
        /// List of the Changes which need to be applied to the Basis of the layer
        /// </summary>
        [JsonIgnore]
        List<double> BasisSmudges { get; set; }
    }
}
