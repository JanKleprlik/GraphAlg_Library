using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithmLib.DataStructures
{
    public interface IGraph
    {
        
        #region Basic stuff
        /// <summary>
        /// Vrátí TRUE když je graf orientovaný, jinak FALSE.
        /// </summary>
        bool directed { get;  }

        /// <summary>
        /// Vrátí TRUE když je graf ohodnocený, jinak FALSE.
        /// </summary>
        bool weighted { get; }

        /// <summary>
        /// Vrátí počet vrcholů.
        /// </summary>
        int verticesCount { get; }

        /// <summary>
        /// Vrátí počet hran.
        /// </summary>
        int edgesCount { get; set; }

        /// <summary>
        /// Vrátí TRUE když jsou povoleny multihrany, jinak FALSE.
        /// </summary>
        bool multigraph { get; set; }

        /// <summary>
        /// Vrátí list s vrcholy.
        /// </summary>
        List<Vertex> vertices { get; }

        List<Edge> edges { get; }

        #endregion
        

        
        #region Vertex stuff
        /// <summary>
        /// Přidá vrchol do grafu.
        /// </summary>
        void AddVertex(string name);

        /// <summary>
        /// Odebere vrchol z grafu.
        /// </summary>
        void RemoveVertex(string name);

        /// <summary>
        /// Vrátí TRUE když vrchol existuje, jinak FALSE.
        /// </summary>
        bool ExistVertex(string name);

        /// <summary>
        /// Najde vrchol podle jména.
        /// </summary>
        Vertex GetVertex(string name);

        #endregion
        


        #region Edge stuff
        /// <summary>
        /// Přidá hranu do grafu.
        /// </summary>
        void AddEdge(string sourceName, string destinationName);

        /// <summary>
        /// Odebere hranu z grafu.
        /// </summary>
        void RemoveEdge(string sourceName, string destinationName);

        /// <summary>
        /// Vrátí TRUE když hrana mezi vrcholy existuje, jinak FALSE.
        /// </summary>
        bool ExistEdge(string sourceName, string destinationName);

        /// <summary>
        /// Změní cenu hrany z oldWeight na newWeight.
        /// </summary>
        void ChangeWeight(string sourceName, string destinationName, long oldWeight, long newWeight);

        long GetEdgeWeight(string sourceName, string destinationName);

        #endregion



        /// <summary>
        /// Smaže informace o grafu.
        /// </summary>
        void Clear();

    }
}
