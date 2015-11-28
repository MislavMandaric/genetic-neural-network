using GeneticNeuralNetwork.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticNeuralNetwork.App
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet dataset = new DataSet("dataset.txt", 2, 3);
            NeuralNetwork network = new NeuralNetwork(2, 8, 3);
            GeneticAlgorithm algorithm = new GeneticAlgorithm(network, dataset, network.NumberOfParameters);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            IList<double> parameters = algorithm.Learn();
            stopwatch.Stop();

            network.Run(dataset, parameters);

            Write(parameters);
            Console.WriteLine("Time: {0}", stopwatch.Elapsed.TotalSeconds);
        }

        private static void Write(IList<double> parameters)
        {
            using (StreamWriter writter = new StreamWriter("parameters.txt"))
            {
                for (int i = 0; i < 8 * 2 * 2; i++)
                {
                    if (i % 4 == 0)
                        writter.Write("{0},", parameters[i]);
                    else if (i % 4 == 2)
                        writter.WriteLine(parameters[i]);
                }
                writter.WriteLine();
                for (int i = 0; i < 8 * 2 * 2; i++)
                {
                    if (i % 4 == 1)
                        writter.Write("{0},", parameters[i]);
                    else if (i % 4 == 3)
                        writter.WriteLine(parameters[i]);
                }
                writter.WriteLine();
                for (int i = 0; i < 3 * 9; i++)
                {
                    if (i % 9 == 8)
                        writter.WriteLine(parameters[8 * 2 * 2 + i]);
                    else
                        writter.Write("{0},", parameters[8 * 2 * 2 + i]);
                }
            }
        }
    }
}
