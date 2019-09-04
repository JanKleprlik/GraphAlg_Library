using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithmLib.DataStructures
{
    public class UndirectedWeightedGraph :GraphBase
    {
        /// <summary>
        /// Konstruktor neorientovaného ohodnoceného grafu.
        /// </summary>
        public UndirectedWeightedGraph(bool isMultigraph)
        {
            multigraph = isMultigraph;
        }

        #region Basic stuff
        /// <summary>
        /// Vrátí TRUE když je graf ohodnocený, jinak FALSE.
        /// </summary>
        public override bool weighted { get; } = true;
        #endregion

        #region Vertex stuff

        public override void RemoveVertex(string name)
        {
            if (CheckVertex(name))
            {
                Stack<Edge> stack = new Stack<Edge>();
                Vertex vertex = FindVertex(name);
                foreach (Edge edge in vertex.outgoingEdges)
                {
                    stack.Push(edge);
                }
                while (stack.Count > 0)
                {
                    Edge removeEdge = stack.Pop();
                    RemoveEdge(removeEdge.source.name, removeEdge.destination.name, removeEdge.weight);
                }
                foreach (Edge edge in vertex.incomingEdges)
                {
                    stack.Push(edge);
                }
                while (stack.Count > 0)
                {
                    Edge removeEdge = stack.Pop();
                    RemoveEdge(removeEdge.source.name, removeEdge.destination.name, removeEdge.weight);
                }
                vertices.Remove(vertex);
            }
            else
            {
                Console.WriteLine("Can not remove vertex \"" + name + "\". It is not in the graph.");
            }
        }

        #endregion

        #region Edge stuff

        /// <summary>
        /// Přidá hranu do grafu.
        /// </summary>
        public override void AddEdge(string sourceName, string destinationName)
        {
            Console.WriteLine("Add weight parameter to AddEdge method.");
        }

        /// <summary>
        /// Přidá hranu do grafu.
        /// </summary>
        public void AddEdge(string sourceName, string destinationName, long weight)
        {
            try
            {
                Vertex source = CheckAndFindVertex(sourceName);
                Vertex destination = CheckAndFindVertex(destinationName);
                if (source.Equals(destination))
                {
                    Console.WriteLine("Loops are not allowed.");
                    return;
                }

                if (!CheckEdge(source, destination) || multigraph)
                {
                    Edge edge1 = new Edge(source, destination, weight, false);
                    Edge edge2 = new Edge(destination, source, weight, false);
                    source.outgoingEdges.Add(edge1);
                    source.incomingEdges.Add(edge2);
                    destination.incomingEdges.Add(edge1);
                    destination.outgoingEdges.Add(edge2);
                    AddNeighbour(source, destination);
                    AddNeighbour(destination, source);
                    edges.Add(edge1);
                    edgesCount++;
                }
            }
            catch (VertexNotInGraph)
            {
                Console.WriteLine("Chosen vertices \"" + sourceName + "\" or \"" + destinationName + "\" are not in the graph.");
            }
        }

        /// <summary>
        /// Odebere hranu z grafu.
        /// </summary>
        public override void RemoveEdge(string sourceName, string destinationName)
        {
            Console.WriteLine("Add weight parameter to RemoveEdge method.");
        }

        /// <summary>
        /// Odebere hranu z grafu.
        /// </summary>
        public void RemoveEdge(string sourceName, string destinationName, long weight)
        {
            try
            {
                Vertex source = CheckAndFindVertex(sourceName);
                Vertex destination = CheckAndFindVertex(destinationName);
                Edge edge1 = FindEdge(source, destination, weight);
                Edge edge2 = FindEdge(destination, source, weight);
                source.neighbours.Remove(destination);
                destination.neighbours.Remove(source);
                source.outgoingEdges.Remove(edge1);
                source.incomingEdges.Remove(edge2);
                destination.incomingEdges.Remove(edge1);
                destination.outgoingEdges.Remove(edge2);
                edgesCount--;
            }
            catch (VertexNotInGraph)
            {
                Console.WriteLine("Chosen vertices \"" + sourceName + "\" or \"" + destinationName + "\" are not in the graph.");
            }
            catch (EdgeNotInGraph)
            {
                Console.WriteLine("Edge from \"" + sourceName + "\" to \"" + destinationName + "\" weighted \"" + weight + "\" is not in the graph");
            }
        }

        /// <summary>
        /// Vrátí TRUE když hrana mezi vrcholy existuje, jinak FALSE.
        /// </summary>
        public override bool ExistEdge(string sourceName, string destinationName)
        {
            try
            {
                Vertex source = CheckAndFindVertex(sourceName);
                Vertex destination = CheckAndFindVertex(destinationName);
                if (CheckEdge(source, destination) && CheckEdge(destination, source))
                {
                    return true;
                }
                else return false;
            }
            catch (VertexNotInGraph)
            {
                Console.WriteLine("Chosen vertices \"" + sourceName + "\" or \"" + destinationName + "\" are not in the graph.");
                return false;
            }

        }

        /// <summary>
        /// Vrátí cenu hrany
        /// </summary>
        public override long GetEdgeWeight(string sourceName, string destinationName)
        {
            try
            {
                if (!multigraph)
                {
                    Vertex source = CheckAndFindVertex(sourceName);
                    Vertex destination = CheckAndFindVertex(destinationName);
                    Edge edge = FindEdge(source, destination);
                    return edge.weight;
                }
                else
                {
                    Console.WriteLine("Edge weight can be obtained only in non multigraphs.");
                    return 1;
                }

            }
            catch (VertexNotInGraph)
            {
                Console.WriteLine("Chosen vertices \"" + sourceName + "\" or \"" + destinationName + "\" are not in the graph.");
                return long.MaxValue;
            }
            catch (EdgeNotInGraph)
            {
                Console.WriteLine("Edge from \"" + sourceName + "\" to \"" + destinationName + "is not in the graph");
                return long.MaxValue;
            }
            catch (NotImplementedException)
            {
                Console.WriteLine("This graph is supposed to be weighted. Something must ve fixed");
                return long.MaxValue;
            }
        }

        /// <summary>
        /// Změní cenu hrany z oldWeight na newWeight.
        /// </summary>
        public override void ChangeWeight(string sourceName, string destinationName, long oldWeight, long newWeight)
        {
            try
            {
                Vertex source = CheckAndFindVertex(sourceName);
                Vertex destination = CheckAndFindVertex(destinationName);
                if (CheckEdge(source, destination))
                {
                    Edge edge1 = FindEdge(source, destination, oldWeight);
                    Edge edge2 = FindEdge(destination, source, oldWeight);
                    edge1.weight = newWeight;
                    edge2.weight = newWeight;

                }
            }
            catch (VertexNotInGraph)
            {
                Console.WriteLine("Chosen vertices \"" + sourceName + "\" or \"" + destinationName + "\" are not in the graph.");
            }
            catch (EdgeNotInGraph)
            {
                Console.WriteLine("Edge from \"" + sourceName + "\" to \"" + destinationName + "\" weighted \"" + oldWeight + "\" is not in the graph");
            }
        }


        /// <summary>
        /// Vrátí hranu s určitým oceněním, pokud hrana neexistuje vyhodí exception
        /// </summary>
        private Edge FindEdge(Vertex source, Vertex destination, long weight)
        {
            foreach (Edge candidate1 in source.outgoingEdges)
            {
                foreach (Edge candidate2 in destination.incomingEdges)
                {
                    if ((candidate1 == candidate2) && candidate1.weight == weight && candidate2.weight == weight)
                    {
                        return candidate1;
                    }
                }
            }
            throw new EdgeNotInGraph();
        }

        #endregion
    }
}
