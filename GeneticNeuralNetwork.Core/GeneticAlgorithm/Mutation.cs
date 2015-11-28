using Meta.Numerics;
using Meta.Numerics.Statistics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticNeuralNetwork.Core
{
    public static class Mutation
    {
        private const double MEAN = 0;
        private const double SIGMA_1 = 0.5;
        private const double SIGMA_2 = 5;
        private const double SELECTION_PROBABILITY = 0.7;
        private const double MUTATION_PROBABILITY = 0.01;

        public static void Mutate(Chromosome chromosome, Random random)
        {
            NormalDistribution normal;
            UniformDistribution uniform = new UniformDistribution(Interval.FromEndpoints(0, 1));
            for (int i = 0; i < chromosome.Values.Count; i++)
            {
                if (uniform.GetRandomValue(random) <= MUTATION_PROBABILITY)
                {
                    if (uniform.GetRandomValue(random) <= SELECTION_PROBABILITY)
                        normal = new NormalDistribution(MEAN, SIGMA_1);
                    else
                        normal = new NormalDistribution(MEAN, SIGMA_2);
                    chromosome.Values[i] += normal.GetRandomValue(random);
                }
            }
        }
    }
}
