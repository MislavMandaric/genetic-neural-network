using Meta.Numerics;
using Meta.Numerics.Statistics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticNeuralNetwork.Core
{
    public static class Selection
    {
        private const int K = 8;

        public static Tuple<Chromosome, Chromosome> Select(IList<Chromosome> population, Random random, out Chromosome worst)
        {
            SortedSet<Chromosome> tournament = new SortedSet<Chromosome>();
            DiscreteUniformDistribution discrete = new DiscreteUniformDistribution(0, population.Count - 1);
            while (tournament.Count < K)
            {
                int index = discrete.GetRandomValue(random);
                tournament.Add(population[index]);
            }
            worst = tournament.Max;
            tournament.Remove(worst);
            double sumFitness;
            CalculateFitness(tournament, out sumFitness);
            Chromosome parent1 = SelectParent(tournament, sumFitness, random);
            tournament.Remove(parent1);
            CalculateFitness(tournament, out sumFitness);
            Chromosome parent2 = SelectParent(tournament, sumFitness, random);
            return new Tuple<Chromosome, Chromosome>(parent1, parent2);
        }

        private static Chromosome SelectParent(SortedSet<Chromosome> tournament, double sumFitness, Random random)
        {
            UniformDistribution uniform = new UniformDistribution(Interval.FromEndpoints(0, 1));
            double randomNumber = uniform.GetRandomValue(random);
            double sumLength = 0;
            foreach (var chromosome in tournament)
            {
                sumLength += chromosome.Fitness / sumFitness;
                if (randomNumber < sumLength)
                    return chromosome;
            }
            return tournament.Last();
        }

        private static void CalculateFitness(SortedSet<Chromosome> tournament, out double sumFitness)
        {
            double sum = 0;
            foreach (var chromosome in tournament)
            {
                chromosome.Fitness = tournament.Max.Penalty - chromosome.Penalty;
                sum += chromosome.Fitness;
            }
            sumFitness = sum;
        }
    }
}
