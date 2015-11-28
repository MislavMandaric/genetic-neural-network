using Meta.Numerics;
using Meta.Numerics.Statistics.Distributions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticNeuralNetwork.Core
{
    public class Chromosome : IComparable<Chromosome>
    {
        private const double MIDPOINT_RANDOM = 0;
        private const double WIDTH_RANDOM = 5;

        private double fitness;
        private double penalty;
        private IList<double> values;

        public Chromosome(Random random, int parameterSize)
        {
            this.values = new List<double>(parameterSize);
            UniformDistribution uniform = new UniformDistribution(Interval.FromMidpointAndWidth(MIDPOINT_RANDOM, WIDTH_RANDOM));
            for (int i = 0; i < parameterSize; i++)
            {
                this.values.Add(uniform.GetRandomValue(random));
            }
        }
        public Chromosome(IList<double> values)
        {
            this.values = new List<double>(values);
        }

        public double Fitness { get { return fitness; } set { fitness = value; } }

        public double Penalty { get { return penalty; } set { penalty = value; } }

        public IList<double> Values { get { return values; } }

        public int CompareTo(Chromosome other)
        {
            if (other == null)
                return -1;
            return this.penalty.CompareTo(other.penalty);
        }
    }
}
