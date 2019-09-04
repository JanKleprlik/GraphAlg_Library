using System;
using System.Collections.Generic;
using System.Text;
using GraphAlgorithmLib.DataStructures;

namespace GraphAlgorithmLib.Algorithms
{
    public static class BreathFirstSearch<TGraph> where TGraph: IGraph
    {

        /// <summary>
        /// Prohledá graf do šířky ze zadaného vrcholu.
        /// </summary>
        public static void Search (TGraph graph, string start_name)
        {

            if (graph == null)
            {
                throw new ArgumentException("Use graph that is not empty.");
            }

            Queue<Vertex> queue = new Queue<Vertex>();
            Vertex start = graph.GetVertex(start_name);

            Initialize(graph);
            Vertex activeVertex;
            start.distance = 0;
            start.predecessor = null;
            start.visited = true;
            queue.Enqueue(start);

            while (queue.Count != 0)
            {
                activeVertex = queue.Dequeue();
                foreach(Vertex neighbour in activeVertex.neighbours)
                {
                    long edgeWeight = graph.GetEdgeWeight(activeVertex.name, neighbour.name);
                    if (!neighbour.visited)
                    {
                        neighbour.predecessor = activeVertex;
                        neighbour.distance = activeVertex.distance + 1;
                        neighbour.visited = true;

                        queue.Enqueue(neighbour);
                    }
                }                
            }

            CheckCorectness(graph, start);
        }

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
        /// Inicializuje hodnoty u vrchyolů v grafu
        /// </summary>
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
        /// Zkonroluje, jestli vzádlenost do startu je 0, a v žádné dvojici sousedních vrcholů neexistuje kratší cesta.
        /// </summary>
        private static bool CheckCorectness (TGraph graph, Vertex start)
        {
            if (start.distance != 0)
            {
                return false;
            }

            foreach (Vertex source in graph.vertices)
            {
                foreach(Vertex destination in source.neighbours)
                {
                    if (!(destination.distance <= source.distance + 1))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
