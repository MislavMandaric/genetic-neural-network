using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticNeuralNetwork.Core
{
    public class NeuralNetwork
    {
        private IList<Layer> layers;

        public NeuralNetwork(params int[] layerSizes)
        {
            if (layerSizes.Length < 3)
                throw new ApplicationException("Neural network must have at least 3 layers.");
            this.layers = new List<Layer>(layerSizes.Length - 1);
            for (int i = 1; i < layerSizes.Length; i++)
            {
                this.layers.Add(new Layer(layerSizes[i], layerSizes[i - 1], i + 1));
            }
        }

        public int NumberOfParameters
        {
            get
            {
                int sum = 0;
                foreach (var layer in this.layers)
                {
                    foreach (var neuron in layer)
                    {
                        sum += neuron.ParameterSize;
                    }
                }
                return sum;
            }
        }

        public IList<double> CalculateOutput(IList<double> inputs, IList<double> parameters)
        {
            int parameterStart = 0;
            IList<double> input = new List<double>(inputs);
            IList<double> output = new List<double>();
            foreach (var layer in this.layers)
            {
                output.Clear();
                foreach (var neuron in layer)
                {
                    IList<double> parameter = parameters.ToList().GetRange(parameterStart, neuron.ParameterSize);
                    output.Add(neuron.Calculate(input, parameter));
                    parameterStart += neuron.ParameterSize;
                }
                input = new List<double>(output);
            }
            return output;
        }

        public double CalculateError(DataSet dataset, IList<double> parameters)
        {
            double error = 0;
            foreach (var input in dataset.Keys)
            {
                IList<double> output = this.CalculateOutput(input, parameters);
                foreach (var err in dataset[input].Zip(output, (x, y) => Math.Pow((x - y), 2)))
                {
                    error += err;
                }
            }
            return error / dataset.Count;
        }

        public void Run(DataSet dataset, IList<double> parameters)
        {
            int correct = 0;
            Console.WriteLine("\tTrueOut\tNetOut");
            foreach (var input in dataset.Keys)
            {
                IList<int> output = this.Binarization(this.CalculateOutput(input, parameters));
                IList<int> trueOutput = this.Binarization(dataset[input]);
                this.Write(trueOutput, output, ref correct);
            }
            Console.WriteLine("Correct: {0}", correct);
            Console.WriteLine("Incorrent: {0}", dataset.Count - correct);
        }

        private IList<int> Binarization(IList<double> list)
        {
            return list.Select(x => Convert.ToInt32(Math.Round(x))).ToList();
        }

        private void Write(IList<int> trueOutput, IList<int> output, ref int correct)
        {
            if (output.SequenceEqual(trueOutput))
            {
                correct++;
                Console.Write("+\t");
            }
            else
                Console.Write("-\t");
            foreach (var element in trueOutput)
            {
                Console.Write(element);
            }
            Console.Write("\t");
            foreach (var element in output)
            {
                Console.Write(element);
            }
            Console.WriteLine();
        }
    }
}
