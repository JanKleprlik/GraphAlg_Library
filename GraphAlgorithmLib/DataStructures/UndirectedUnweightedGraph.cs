using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithmLib.DataStructures
{
    public class UndirectedUnweightedGraph :GraphBase
    {
        /// <summary>
        /// Konstruktor neorientovaného neohodnoceného grafu.
        /// </summary>
        public UndirectedUnweightedGraph(bool isMultigraph)
        {
            multigraph = isMultigraph;
        }

        #region Edge stuff
        /// <summary>
        /// Přidá hranu do grafu.
        /// </summary>
        public override void AddEdge(string sourceName, string destinationName)
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
                    Edge edge1 = new Edge(source, destination, false);
                    Edge edge2 = new Edge(destination, source, false);
                    source.outgoingEdges.Add(edge1);
                    source.incomingEdges.Add(edge2);
                    destination.incomingEdges.Add(edge1);
                    destination.outgoingEdges.Add(edge2);
                    AddNeighbour(source, destination);
                    AddNeighbour(destination, source);
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
            try
            {
                Vertex source = CheckAndFindVertex(sourceName);
                Vertex destination = CheckAndFindVertex(destinationName);
                Edge edge1 = FindEdge(source, destination);
                Edge edge2 = FindEdge(destination, source);
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
                Console.WriteLine("Edge from \"" + sourceName + "\" to \"" + destinationName + "\" is not in the graph");
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
        #endregion
    }
}
