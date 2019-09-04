using System;
using System.Collections.Generic;
using GraphAlgorithmLib.DataStructures;

namespace GraphAlgorithmLib.Algorithms
{
    public static class FloydWarshallShortestPath<TGraph> where TGraph : IGraph
    {
        /// <summary>
        /// Aplikuje Floyd-Warshalův algoritmus na graf.
        /// </summary>
        /// <returns>Tuple, první je matice vzádleností, druhá je matice předchůdců. Index v matici odpovídá parametru "distance" u vrcholů.</returns>
        public static (long[,], Vertex[,]) Search(TGraph graph)
        {
            if (graph == null)
            {
                throw new ArgumentNullException("Use graph that is not empty.");
            }

            if (!graph.weighted)
            {
                throw new ArgumentException("Graph must be weighted");
            }

            int counterInMatrix = 0;
            foreach (Vertex vertex in graph.vertices)
            {
                vertex.distance = counterInMatrix++;
            }

            long[,] distances = new long[graph.verticesCount, graph.verticesCount];
            Vertex[,] followers = new Vertex[graph.verticesCount, graph.verticesCount];

            for (int i = 0; i <graph.verticesCount; i++)
            {
                for (int j = 0; j <graph.verticesCount; j++)
                {
                    distances[i, j] = long.MaxValue;
                    followers[i, j] = null;

                }
            }

            foreach(Edge edge in graph.edges)
            {
                Vertex source = edge.source;
                Vertex destination = edge.destination;

                distances[source.distance, destination.distance] = edge.weight;
                followers[source.distance, destination.distance] = destination;
            }

            foreach(Vertex vertex in graph.vertices)
            {
                distances[vertex.distance, vertex.distance] = 0;
                followers[vertex.distance, vertex.distance] = vertex;
            }

            //algoritmus

            for (int x =0; x < graph.verticesCount; x++)
            {
                for (int y = 0; y < graph.verticesCount; y++)
                {
                    for (int z = 0; z < graph.verticesCount; z++)
                    {
                        if (distances[y,z] > distances[y, x] + distances[x, z])
                        {
                            if (distances[y,x] != long.MaxValue && distances[x,z] != long.MaxValue)
                            {
                                distances[y, z] = distances[y, x] + distances[x, z];
                                followers[y, z] = followers[y, x];
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < graph.verticesCount; i++)
            {
                if (distances[i, i] < 0)
                {
                    throw new ArgumentException("Graph can not contain negative cycle.");
                }

            }

            return (distances, followers);
        }

        /// <summary>
        /// Vrátí nejkratší cestu z vrcholu sourcename do vrcholu destination name.
        /// </summary>
        public static Stack<Vertex> GetShortestPath(TGraph graph, string sourceName, string destinationName, ref Vertex[,] followers)
        {
            var path = new Stack<Vertex>();

            Vertex source = graph.GetVertex(sourceName);
            Vertex destination = graph.GetVertex(destinationName);

            path.Push(source);

            int counterInMatrix = 0;
            foreach (Vertex vertex in graph.vertices)
            {
                vertex.distance = counterInMatrix++;
            }

            while (source != destination)
            {
                source = followers[source.distance, destination.distance];
                path.Push(source);
            }

            return path;
        }
    }
}
