using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticNeuralNetwork.Core
{
    public abstract class Neuron
    {
        private int inputSize;

        public Neuron(int inputSize)
        {
            this.inputSize = inputSize;
        }

        public int InputSize { get { return inputSize; } }

        public abstract int ParameterSize { get; }

        public abstract double Calculate(IList<double> inputs, IList<double> parameters);
    }
}
