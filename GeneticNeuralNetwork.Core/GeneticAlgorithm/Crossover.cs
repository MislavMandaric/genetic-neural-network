using Meta.Numerics;
using Meta.Numerics.Statistics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticNeuralNetwork.Core
{
    public static class Crossover
    {
        private const double SELECTION_1_PROBABILITY = 0.5;
        private const double SELECTION_2_PROBABILITY = 0.4;

        public static Chromosome Cross(Tuple<Chromosome, Chromosome> pair, Random random)
        {
            IList<double> values;
            UniformDistribution uniform = new UniformDistribution(Interval.FromEndpoints(0, 1));
            DiscreteUniformDistribution discrete = new DiscreteUniformDistribution(0, 1);
            double randomNumber = uniform.GetRandomValue(random);
            if (randomNumber <= SELECTION_1_PROBABILITY) // whole arithmetic recombination
                values = pair.Item1.Values.Zip(pair.Item2.Values, (x, y) => (x + y) / 2).ToList();
            else if (randomNumber <= SELECTION_1_PROBABILITY + SELECTION_2_PROBABILITY) // discrete recombination
                values = pair.Item1.Values.Zip(pair.Item2.Values, (x, y) => discrete.GetRandomValue(random) == 0 ? x : y).ToList();
            else // simple arithmetic recombination
                values = pair.Item1.Values.Zip(pair.Item2.Values, (x, y) => pair.Item1.Values.IndexOf(x) < pair.Item1.Values.Count / 2 ? x : (x + y) / 2).ToList();
            return new Chromosome(values);
        }
    }
}
