using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticNeuralNetwork.Core
{
    public class TypeTwoNeuron : Neuron
    {
        public TypeTwoNeuron(int inputSize) : base(inputSize) { }

        public override int ParameterSize
        {
            get { return this.InputSize + 1; }
        }

        public override double Calculate(IList<double> inputs, IList<double> parameters)
        {
            if (this.InputSize != inputs.Count || this.ParameterSize != parameters.Count)
                throw new ApplicationException("Nubmber of inputs or number of parameters is not right.");
            double sum = 0;
            for (int i = 0; i < this.InputSize; i++)
            {
                sum += (inputs[i] * parameters[i]);
            }
            sum += parameters[parameters.Count - 1];
            return 1 / (1 + Math.Pow(Math.E, -sum));
        }
    }
}
