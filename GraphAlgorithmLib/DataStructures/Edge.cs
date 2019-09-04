using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithmLib.DataStructures
{
    public class Edge
    {
        /// <summary>
        /// Vrchol, ze kterého hrana vychází.
        /// </summary>
        public Vertex source { get; }
        /// <summary>
        /// Vrchol, do kterého hrana míří.
        /// </summary>
        public Vertex destination { get; }
        /// <summary>
        /// Váha hrany
        /// </summary>
        public long weight { get; set; }
        /// <summary>
        /// Vrátí TRUE, když je hrana ohodnocen, jinak FALSE.
        /// </summary>
        public bool weighted { get; } = false;
        /// <summary>
        /// Vrátí TRUE, když je hrana orientovaná, jinak FALSE.
        /// </summary>
        public bool directed { get; } = false;
        /// <summary>
        /// Konstruktor ohodnocené hrany.
        /// </summary>
        public Edge(Vertex source, Vertex destination, long weight, bool directed)
        {
            this.source = source;
            this.destination = destination;
            this.weight = weight;
            this.directed = directed;
            this.weighted = true;
        }
        /// <summary>
        /// Konstruktor neohodnocené hrany.
        /// </summary>
        public Edge(Vertex source, Vertex destination, bool directed)
        {
            this.source = source;
            this.destination = destination;
            this.weight = 0;
            this.weighted = false;
            this.directed = directed;
        }

    }
}
