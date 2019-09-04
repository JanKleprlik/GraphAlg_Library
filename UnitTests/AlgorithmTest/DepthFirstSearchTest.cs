using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphAlgorithmLib.Algorithms;
using GraphAlgorithmLib.DataStructures;

namespace UnitTests.AlgorithmTest
{
    [TestClass]
    public class DepthFirstSearchTest
    {
        [TestMethod]
        public void Test()
        {
            // orientovaný ohodnocený.
            var graph1 = new DirectedWeightedGraph(false);

            for (int i = 'a'; i < 'h'; i++)
            {
                graph1.AddVertex(Convert.ToString(Convert.ToChar(i)));
            }

            graph1.AddEdge("a", "b", 1);
            graph1.AddEdge("b", "c", 2);
            graph1.AddEdge("c", "d", 3);
            graph1.AddEdge("d", "e", -4);
            graph1.AddEdge("f", "c", 5);
            graph1.AddEdge("a", "g", 600);
            graph1.AddEdge("b", "f", 0);
            graph1.AddEdge("b", "e", 7);

            DepthFirstSearch<DirectedWeightedGraph>.Search(graph1, "a");

            Assert.AreEqual(0, graph1.GetVertex("a").distance);
            Assert.AreEqual(1, graph1.GetVertex("b").distance);
            Assert.AreEqual(2, graph1.GetVertex("c").distance);
            Assert.AreEqual(3, graph1.GetVertex("d").distance);
            Assert.AreEqual(4, graph1.GetVertex("e").distance);
            Assert.AreEqual(2, graph1.GetVertex("f").distance);
            Assert.AreEqual(1, graph1.GetVertex("g").distance);



        }
    }
    
}
