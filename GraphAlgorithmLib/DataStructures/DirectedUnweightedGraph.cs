using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithmLib.DataStructures
{
    public class DirectedUnweightedGraph :GraphBase, IGraph 
    {
        /// <summary>
        /// Konstruktor orientovaného neohodnoceného grafu.
        /// </summary>
        public DirectedUnweightedGraph(bool isMultigraph)
        {
            multigraph = isMultigraph;
        }
        
        #region Basic stuff
        /// <summary>
        /// Vrátí TRUE když je graf orientovaný, jinak FALSE.
        /// </summary>
        public override bool directed { get; } = true;

        /// <summary>
        /// Vrátí TRUE když je graf ohodnocený, jinak FALSE.
        /// </summary>
        public override bool weighted { get; } = false;

        #endregion

        #region Edge stuff
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
                    Edge edge = new Edge(source, destination, true);
                    source.outgoingEdges.Add(edge);
                    destination.incomingEdges.Add(edge);
                    AddNeighbour(source, destination);
                    edgesCount++;
                }
            }
            catch (VertexNotInGraph)
            {
                Console.WriteLine("Chosen vertices \"" + sourceName + "\" or \"" + destinationName + "\" are not in the graph.");
            }
        }


        #endregion
    }
}
