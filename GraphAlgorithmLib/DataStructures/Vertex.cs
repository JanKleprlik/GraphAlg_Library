using System.Collections.Generic;

namespace GraphAlgorithmLib.DataStructures
{
    public class Vertex
    {
        /// <summary>
        /// jméno vrcholu
        /// </summary>
        public string name { get; }
        /// <summary>
        /// vzdálenost vrcholu - pro algoritmy
        /// </summary>
        public long distance { get; set; } = long.MinValue;
        /// <summary>
        /// předchůdce vrcholu - pro algoritmy
        /// </summary>
        public Vertex predecessor { get; set; }
        /// <summary>
        /// flag - pro algoritmy
        /// </summary>
        public bool visited { get; set; }

        /// <summary>
        /// Seznam sousedů.
        /// </summary>
        public List<Vertex> neighbours { get; } = new List<Vertex>();
        /// <summary>
        /// Seznam hran jdoucích do vrcholu.
        /// </summary>
        public List<Edge> incomingEdges { get; } = new List<Edge>();
        /// <summary>
        /// Seznam hran jdoucích z vrhcolu.
        /// </summary>
        public List<Edge> outgoingEdges { get; } = new List<Edge>();

        /// <summary>
        /// Konstruktor vrcholu
        /// </summary>
        public Vertex(string name)
        {
            this.name = name;
        }

    }
}
