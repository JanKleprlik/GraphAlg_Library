using System;
using System.Collections.Generic;
using System.Text;
using GraphAlgorithmLib.DataStructures;

namespace GraphAlgorithmLib.Algorithms
{
    public static class TopologicalSort<TGraph> where TGraph : IGraph
    {
        /// <summary>
        /// Topologicky uspořádá podle času uzavření.
        /// </summary>
        public static Stack<Vertex> Sort(TGraph graph)
        {
            Stack<Vertex> TopologicalSortStack = new Stack<Vertex>();
            
            if (graph == null)
            {
                throw new ArgumentException("Use graph that is not empty.");
            }
            
            if (Cycle.ContainsCycle(graph))
            {
                throw new Exception("This graph contains cycle, therefore is not DAG");
            }
            if (!graph.directed)
            {
                throw new Exception("Graph must be directed.");
            }
            Initialize(graph);
            int order = graph.verticesCount;
            foreach (Vertex vertex in graph.vertices)
            {
                if (!vertex.visited)
                {
                    Visit(vertex, ref TopologicalSortStack,ref order);
                }
            }
            return TopologicalSortStack;
        }

        /// <summary>
        /// Nastaví hodnoty u vrcholů.
        /// </summary>
        private static void Initialize(TGraph graph)
        {
            foreach (Vertex vertex in graph.vertices)
            {
                vertex.visited = false;
                vertex.distance = 0;
            }
        }

        /// <summary>
        /// Navštívý vrchol.
        /// </summary>
        private static void Visit(Vertex activeVertex, ref Stack<Vertex> stack,ref int order)
        {
            activeVertex.visited = true;
            foreach (Vertex neighbour in activeVertex.neighbours)
            {
                if (!neighbour.visited)
                {
                    Visit(neighbour,ref stack,ref order);
                }
            }
            stack.Push(activeVertex);
            activeVertex.distance = order;
            order--;
        }
    }
}
