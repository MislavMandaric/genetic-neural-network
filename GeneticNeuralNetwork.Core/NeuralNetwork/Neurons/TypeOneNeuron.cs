using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticNeuralNetwork.Core
{
    public class TypeOneNeuron : Neuron
    {
        public TypeOneNeuron(int inputSize) : base(inputSize) { }

        public override int ParameterSize
        {
            get { return 2 * this.InputSize; }
        }

        public override double Calculate(IList<double> inputs, IList<double> parameters)
        {
            if (this.InputSize != inputs.Count || this.ParameterSize != parameters.Count)
                throw new ApplicationException("Nubmber of inputs or number of parameters is not right.");
            double sum = 0;
            for (int i = 0; i < this.InputSize; i++)
            {
                sum += (Math.Abs(inputs[i] - parameters[2 * i]) / Math.Abs(parameters[2 * i + 1]));
            }
            return 1 / (1 + sum);
        }
    }
}
