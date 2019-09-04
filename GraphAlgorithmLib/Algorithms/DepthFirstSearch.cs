using System;
using System.Collections.Generic;
using System.Text;
using GraphAlgorithmLib.DataStructures;

namespace GraphAlgorithmLib.Algorithms
{
    public static class DepthFirstSearch<TGraph> where TGraph : IGraph
    {

        /// <summary>
        /// Prohledá graf do hloubky ze zadaného vrcholu.
        /// </summary>
        public static void Search(TGraph graph, string start_name)
        {
            if (graph == null)
            {
                throw new ArgumentException("Use graph that is not empty.");
            }

            Initialize(graph);
            Vertex start = graph.GetVertex(start_name);
            start.distance = 0;
            start.predecessor = null;
            start.visited = true;

            foreach (Vertex neighbour in start.neighbours)
            {
                if (!neighbour.visited)
                {
                    neighbour.visited = true;
                    neighbour.predecessor = start;
                    neighbour.distance = start.distance + 1;
                    Visit(neighbour);
                }
            }
        }
        /// <summary>
        /// Nastaví hodnoty u vrcholů.
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
        /// Navštíví vrchol.
        /// </summary>
        private static void Visit(Vertex activeVertex)
        {
            foreach (Vertex neighbour in activeVertex.neighbours)
            {
                if (!neighbour.visited)
                {
                    neighbour.visited = true;
                    neighbour.predecessor = activeVertex;
                    neighbour.distance = activeVertex.distance + 1;
                    Visit(neighbour);
                }
            }
        }

        /// <summary>
        /// Vrátí cestu do vrcholu.
        /// </summary>
        public static Stack<Vertex> GetPath(TGraph graph, string destination)
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
    }
}
