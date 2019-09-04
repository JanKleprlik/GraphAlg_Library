using System;
using System.Collections.Generic;
using System.Text;
using GraphAlgorithmLib.DataStructures;

namespace GraphAlgorithmLib.Algorithms
{
    public static class DijkstraShortestPath<TGraph> where TGraph : IGraph
    {        /// <summary>
             /// Aplikuje na graf Dijkstrův algoritmus.
             /// </summary>
        public static void Search(TGraph graph, string startName)
        {
            if (graph == null)
            {
                throw new ArgumentNullException("Use graph that is not empty.");
            }

            if (!graph.weighted)
            {
                throw new ArgumentException("Graph must be weighted");
            }

            Vertex start = graph.GetVertex(startName);
            var heap = new MinHeap<TGraph>(graph);
            Initialize(graph);

            start.predecessor = null;
            start.distance = 0;

            foreach(Vertex vertex in graph.vertices)
            {
                heap.Add(vertex);
            }

            while (!heap.Empty())
            {
                Vertex activeVertex = heap.GetMin();
                foreach (Vertex neighbour in activeVertex.neighbours)
                {
                    if ((activeVertex.distance + graph.GetEdgeWeight(activeVertex.name, neighbour.name)) < neighbour.distance)
                    {
                        neighbour.distance = activeVertex.distance + graph.GetEdgeWeight(activeVertex.name, neighbour.name);
                        neighbour.predecessor = activeVertex;
                        heap.Update(neighbour);
                    }
                }
            }
            if(!CheckOptimality(graph, start)) throw new Exception("The graph does not meet optimality criteria for Dijsktra's algorithm.");
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

        /// <summary>
        /// Připravý vrcholy pro algoritmus.
        /// </summary>
        private static void Initialize(TGraph graph)
        {
            foreach (Vertex vertex in graph.vertices)
            {
                vertex.distance = long.MaxValue;
                vertex.visited = false;
                vertex.predecessor = null;
            }
        }


        /// <summary>
        /// Ověří splnění podmínek pro dijkstův algoritmus.
        /// </summary>
        private static bool CheckOptimality(TGraph graph, Vertex source)
        {
            foreach (Vertex vertex in graph.vertices)
            {
                foreach (Edge edge in vertex.outgoingEdges)
                {
                    if (edge.weight < 0)
                    {
                        Console.WriteLine("Edge with negative weight was found.");
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

            foreach (Vertex vertex in graph.vertices)
            {
                foreach(Edge edge in vertex.outgoingEdges)
                {
                    if (edge.destination.distance > vertex.distance + edge.weight)
                    {
                        Console.WriteLine("Not all edges are relaxed");
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
