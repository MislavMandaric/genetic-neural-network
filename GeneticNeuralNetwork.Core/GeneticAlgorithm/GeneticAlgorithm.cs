using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticNeuralNetwork.Core
{
    public class GeneticAlgorithm
    {
        private const int POPULATION_SIZE = 50;
        private const int GENERATION_LIMIT = 5000;
        private const double ERROR_LIMIT = 0.0000001;

        private Random random;
        private NeuralNetwork network;
        private DataSet dataset;
        private IList<Chromosome> population;
        private int parameterSize;

        public GeneticAlgorithm(NeuralNetwork network, DataSet dataset, int parameterSize)
        {
            this.population = new List<Chromosome>(POPULATION_SIZE);
            this.random = new Random();
            this.network = network;
            this.dataset = dataset;
            this.parameterSize = parameterSize;
        }

        public IList<double> Learn()
        {
            this.CreatePopulation();
            this.Evaluate();
            int iteration = 0;
            while (iteration / POPULATION_SIZE < GENERATION_LIMIT && this.population.Min().Penalty > ERROR_LIMIT)
            {
                Chromosome worst;
                Tuple<Chromosome, Chromosome> parents = this.Select(out worst);
                Chromosome child = this.Cross(parents);
                this.Mutate(child);
                this.Evaluate(child);
                this.Replace(worst, child);
                iteration++;
                if (iteration % POPULATION_SIZE == 0)
                {
                    Console.WriteLine("{0}\t{1}", iteration / POPULATION_SIZE, this.population.Min().Penalty);
                }
            }
            return this.population.Min().Values;
        }

        private void Replace(Chromosome worst, Chromosome child)
        {
            this.population.Remove(worst);
            this.population.Add(child);
        }

        private void Mutate(Chromosome child)
        {
            Mutation.Mutate(child, this.random);
        }

        private Chromosome Cross(Tuple<Chromosome, Chromosome> parents)
        {
            return Crossover.Cross(parents, this.random);
        }

        private Tuple<Chromosome, Chromosome> Select(out Chromosome worst)
        {
            return Selection.Select(this.population, this.random, out worst);
        }

        private void Evaluate(Chromosome child)
        {
            child.Penalty = this.network.CalculateError(this.dataset, child.Values);
        }

        private void Evaluate()
        {
            foreach (var chromosome in this.population)
            {
                chromosome.Penalty = this.network.CalculateError(this.dataset, chromosome.Values);
            }
        }

        private void CreatePopulation()
        {
            this.population.Clear();
            for (int i = 0; i < POPULATION_SIZE; i++)
            {
                this.population.Add(new Chromosome(this.random, this.parameterSize));
            }
        }
    }
}
