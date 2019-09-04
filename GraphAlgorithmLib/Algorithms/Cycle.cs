using System;
using System.Collections.Generic;
using System.Text;
using GraphAlgorithmLib.DataStructures;

namespace GraphAlgorithmLib.Algorithms
{
    public class Cycle
    {
        /// <summary>
        /// Prohledá graf do hloubky ze zadaného vrcholu.
        /// Vrátí TRUE, když graf obsahuje cyklus, jinak FALSE;
        /// </summary>
        public static bool ContainsCycle<TGraph>(TGraph graph) where TGraph : IGraph
        {
            if (graph == null)
            {
                throw new ArgumentException("Use graph that is not empty.");
            }

            Stack<Vertex> visitedInRecursion = new Stack<Vertex>();

            if (graph == null)
            {
                throw new ArgumentException();
            }
            if (graph.directed)
            {
                foreach(Vertex vertex in graph.vertices)
                {
                    if(DirectedCycle(graph, vertex, ref visitedInRecursion))
                    {
                        return true;
                    }
                }
            }
            else
            {
                Console.WriteLine("Graph checking works only on oriented graphs.");
                return true;
            }
            return false;
        }
        /// <summary>
        /// Zkotroluje cykly v orientovaném grafu.
        /// Vrátí TRUE, pokud existuje cyklus.
        /// </summary>
        private static bool DirectedCycle<TGraph>(TGraph graph, Vertex vertex, ref Stack<Vertex> stack)
        {
            if (!vertex.visited)
            {
                vertex.visited = true;
                stack.Push(vertex);
                foreach(Vertex neighbour in vertex.neighbours)
                {
                    if (stack.Contains(neighbour))
                    {
                        return true;
                    }
                    if (!neighbour.visited && DirectedCycle(graph, neighbour, ref stack))
                    {
                        return true;
                    }

                }
                stack.Pop();
            }
            return false;
        }
    }
}
