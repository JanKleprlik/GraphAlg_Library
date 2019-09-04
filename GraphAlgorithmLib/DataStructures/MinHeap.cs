using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithmLib.DataStructures
{
    public class MinHeap<TGraph> where TGraph : IGraph
    {
        private int count;
        private Vertex[] elements { get; }
        private long[] keys { get; }

        /// <summary>
        /// Konstruktor haldy s minimálním prvkem na vrcholu.
        /// </summary>
        public MinHeap(TGraph graph)
        {
            elements = new Vertex[graph.verticesCount];
            keys = new long[graph.verticesCount];
            count = 0;
        }

        /// <summary>
        /// Přidá vrchol do haldy.
        /// </summary>
        public void Add(Vertex vertex)
        {
            if (count == elements.Length)
            {
                throw new IndexOutOfRangeException();
            }

            elements[count] = vertex;
            keys[count] = vertex.distance;

            BubbleUp(count);

            count++;

        }

        /// <summary>
        /// Vrátí a odebere z haldy vrchol s nejmenším klíčem.
        /// </summary>
        public Vertex GetMin()
        {

            if (count == 0)
            {
                throw new IndexOutOfRangeException();
            }

            Vertex vertex = elements[0];
            Swap(0, count - 1);
            count--;

            BubbleDown(0);

            return vertex;
        }

        /// <summary>
        /// Vrátí vrchol s nejmenším klíčem.
        /// </summary>
        public Vertex Peek()
        {
            return elements[0];
        }

        /// <summary>
        /// Vrátí TRUE, když je halda prázdná, jinak false.
        /// </summary>
        public bool Empty()
        {
            if (count == 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Updatuje pozici vrcholu v haldě.
        /// </summary>
        public void Update(Vertex vertex)
        {
            try
            {
                int position = FindPosition(vertex);
                keys[position] = vertex.distance;
                BubbleUp(position);
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Vertex \"" + vertex.name + "\" is not in the heap.");
            }

        }

        private int FindPosition(Vertex vertex)
        {
            for (int i = 0; i<=count; i++)
            {
                if (elements[i] == vertex)
                {
                    return i;
                }
            }
            return elements.Length + 1;
        }
        private void BubbleUp(int position)
        {
            while ((position > 0) && keys[Parent(position)] > keys[position])
            {
                int parentPosition = Parent(position);
                Swap(position, parentPosition);
                position = parentPosition;
            }
        }

        private void BubbleDown(int position)
        {
            int leftChild = LeftChild(position);
            int rightChild = RightChild(position);
            int max = 0;

            if ((leftChild < count) && (keys[leftChild] < keys[position]))
            {
                max = leftChild;
            }
            else
            {
                max = position;
            }
            if ((rightChild < count) && (keys[rightChild] < keys[max]))
            {
                max = rightChild;
            }
            if (max != position)
            {
                Swap(position, max);
                BubbleDown(max);
            }
        }

        private void Swap(int position1, int position2)
        {
            Vertex temp1 = elements[position1];
            elements[position1] = elements[position2];
            elements[position2] = temp1;

            long temp2 = keys[position1];
            keys[position1] = keys[position2];
            keys[position2] = temp2;
        }

        private int LeftChild(int position)
        {
            return 2 * position + 1;
        }
        private int RightChild(int position)
        {
            return 2 * position + 2;
        }
        private int Parent(int position)
        {
            return (position - 1) / 2;
        }

    }
}