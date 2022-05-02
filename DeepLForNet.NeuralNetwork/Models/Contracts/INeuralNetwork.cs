using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DeepLForNet.NeuralNetwork.Models.Contracts {
    internal interface INeuralNetwork {
        /// <summary>
        /// The Layers different layers of the network
        /// </summary>
        List<ILayer> Layers { get; set; }

        /// <summary>
        /// The last layer of the network
        /// </summary>
        IOutputLayer OutputLayer { get; set; }
        
        /// <summary>
        ///  The accuracy of the network
        /// </summary>
        double RateOfSucces { get; set; }
        
        /// <summary>
        /// The Iteration of the network
        /// </summary>
        int Mutation { get; set; }
         
        /// <summary>
        /// List of the Errors of the last executions
        /// </summary>
        [JsonIgnore]
        List<double> Errors { get; set; }
        
        /// <summary>
        /// List that reprenests the correctness of the last predictions 
        /// </summary>
        [JsonIgnore]
        List<bool> Success { get; set; }

        /// <summary>
        /// The Inputs for the current calculation
        /// </summary>
        [JsonIgnore]
        List<double> Inputs { get; set; }
    }
}
