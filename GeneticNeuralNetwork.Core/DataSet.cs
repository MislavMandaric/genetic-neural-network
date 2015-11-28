using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GeneticNeuralNetwork.Core
{
    public class DataSet : IDictionary<IList<double>, IList<double>>
    {
        private IDictionary<IList<double>, IList<double>> data;

        public DataSet(string path, int inputSize, int outputSize)
        {
            this.data = new Dictionary<IList<double>, IList<double>>();
            foreach (var line in File.ReadAllLines(path))
            {
                string[] elements = line.Split(' ', '\t');
                IList<double> input = new List<double>(inputSize);
                for (int i = 0; i < inputSize; i++)
                {
                    input.Add(double.Parse(elements[i]));
                }
                IList<double> output = new List<double>(outputSize);
                for (int i = inputSize; i < inputSize + outputSize; i++)
                {
                    output.Add(double.Parse(elements[i]));
                }
                this.data.Add(input, output);
            }
        }

        public void Add(IList<double> key, IList<double> value)
        {
            this.data.Add(key, value);
        }

        public bool ContainsKey(IList<double> key)
        {
            return this.data.ContainsKey(key);
        }

        public ICollection<IList<double>> Keys
        {
            get { return this.data.Keys; }
        }

        public bool Remove(IList<double> key)
        {
            return this.data.Remove(key);
        }

        public bool TryGetValue(IList<double> key, out IList<double> value)
        {
            return this.data.TryGetValue(key, out value);
        }

        public ICollection<IList<double>> Values
        {
            get { return this.data.Values; }
        }

        public IList<double> this[IList<double> key]
        {
            get
            {
                return this.data[key];
            }
            set
            {
                this.data[key] = value;
            }
        }

        public void Add(KeyValuePair<IList<double>, IList<double>> item)
        {
            this.data.Add(item);
        }

        public void Clear()
        {
            this.data.Clear();
        }

        public bool Contains(KeyValuePair<IList<double>, IList<double>> item)
        {
            return this.data.Contains(item);
        }

        public void CopyTo(KeyValuePair<IList<double>, IList<double>>[] array, int arrayIndex)
        {
            this.data.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return this.data.Count; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public bool Remove(KeyValuePair<IList<double>, IList<double>> item)
        {
            return this.data.Remove(item);
        }

        public IEnumerator<KeyValuePair<IList<double>, IList<double>>> GetEnumerator()
        {
            return this.data.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.data.GetEnumerator();
        }
    }
}
