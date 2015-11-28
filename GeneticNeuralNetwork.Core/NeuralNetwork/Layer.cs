using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneticNeuralNetwork.Core
{
    public class Layer : IEnumerable<Neuron>
    {
        private IList<Neuron> neurons;

        public Layer(int layerSize, int inputSize, int layerDepth)
        {
            this.neurons = new List<Neuron>(layerSize);
            for (int i = 0; i < layerSize; i++)
            {
                this.neurons.Add(NeuronFactory.CreateNeuron(inputSize, layerDepth));
            }
        }

        public IEnumerator<Neuron> GetEnumerator()
        {
            return this.neurons.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.neurons.GetEnumerator();
        }
    }
}
