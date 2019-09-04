using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithmLib
{
    public class VertexNotInGraph : Exception
    {
        public VertexNotInGraph()
        {            
        }
        public VertexNotInGraph(string message) :base(message)
        {
        }
        public VertexNotInGraph(string message, Exception inner) :base(message, inner)
        {
        }

    }

    public class EdgeNotInGraph : Exception
    {
        public EdgeNotInGraph()
        {
        }
        public EdgeNotInGraph(string message) : base(message)
        {
        }
        public EdgeNotInGraph(string message, Exception inner) : base(message, inner)
        {
        }

    }
}
