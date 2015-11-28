using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticNeuralNetwork.Core
{
    public static class NeuronFactory
    {
        public static Neuron CreateNeuron(int inputSize, int layerDepth)
        {
            if (layerDepth == 2)
                return new TypeOneNeuron(inputSize);
            else
                return new TypeTwoNeuron(inputSize);
        }
    }
}
