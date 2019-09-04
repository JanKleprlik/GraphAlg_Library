using System;
using System.Collections.Generic;
using GraphAlgorithmLib.DataStructures;

namespace GraphAlgorithmLib.Algorithms
{
    public static class BellmanFordShortestPath<TGraph> where TGraph : IGraph
    {
        /// <summary>
        /// Aplikuje na graf Bellamův-Fordův algoritmus.
        /// </summary>
        public static void Search(TGraph graph, string startName)
        {
            if (graph == null)
            {
                throw new ArgumentNullException();
            }

            if (!graph.weighted)
            {
                throw new ArgumentException("Graph must be weighted");
            }

            Vertex start = graph.GetVertex(startName);
            Initialize(graph);

            start.distance = 0;
            start.predecessor = null;

            for (int i = 1; i < graph.verticesCount-1; i++)
            {
                foreach (Edge edge in graph.edges)
                {
                    Vertex source = edge.source;
                    Vertex destination = edge.destination;
                    long temp = source.distance + edge.weight;

                    if (temp < destination.distance)
                    {
                        destination.distance = temp;
                        destination.predecessor = source;
                    }
                }
            }


            foreach (Edge edge in graph.edges)
            {
                Vertex source = edge.source;
                Vertex destination = edge.destination;
                long temp = source.distance + edge.weight;

                if (temp < destination.distance)
                {
                    throw new ArgumentException("Graph contains negative cycle.");
                }
            }

            if (!CheckOptimality(graph, start)) throw new Exception("The graph does not meet optimality criteria for Belman-Ford's algorithm.");

        }

        /// <summary>
        /// Vrátí nejkratší cestu do vrcholu destination.
        /// </summary>
        public static Stack<Vertex> GetShortestPath(TGraph graph, string destination)
        {
            Stack<Vertex> path = new Stack<Vertex>();

            Vertex activeVertex = graph.GetVertex(destination);
            while (activeVertex.predecessor != null)
            {
                path.Push(activeVertex);
                activeVertex = activeVertex.predecessor;
            }
            path.Push(activeVertex);

            return path;
        }

        private static void Initialize(TGraph graph)
        {
            foreach (Vertex vertex in graph.vertices)
            {
                vertex.distance = long.MaxValue;
                vertex.predecessor = null;
                vertex.visited = false;
            }
        }
        /// <summary>
        /// Ověří splnění podmínek pro Bellmaův-Fordův algoritmus.
        /// </summary>
        private static bool CheckOptimality(TGraph graph, Vertex source)
        { 
            foreach (Vertex vertex in graph.vertices)
            {
                foreach (Edge edge in vertex.outgoingEdges)
                {
                    if (edge.destination.distance > vertex.distance + edge.weight)
                    {
                        Console.WriteLine("Not all edges are relaxed");
                        return false;
                    }
                }
            }
            foreach (Vertex vertex in graph.vertices)
            {
                if (vertex.predecessor == null && vertex.distance != long.MaxValue)
                {
                    if (vertex == source) continue;
                    Console.WriteLine("Unreachable vertex doesn not have maximal distance.");
                    return false;
                }
            }
            return true;
        }
    }
}
