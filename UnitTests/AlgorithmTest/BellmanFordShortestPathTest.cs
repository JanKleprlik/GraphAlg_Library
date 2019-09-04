using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphAlgorithmLib.DataStructures;
using GraphAlgorithmLib.Algorithms;
using System.Collections.Generic;

namespace UnitTests.AlgorithmTest
{
    [TestClass]
    public class BellmanFordShortestPathTest
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

            graph1.AddEdge("a", "b", 10);
            graph1.AddEdge("b", "c", 20);
            graph1.AddEdge("c", "d", 3);
            graph1.AddEdge("d", "e", 4);
            graph1.AddEdge("f", "c", -20);
            graph1.AddEdge("b", "f", 3);
            graph1.AddEdge("e", "b", 10);

            BellmanFordShortestPath<DirectedWeightedGraph>.Search(graph1, "a");

            Assert.AreEqual(0, graph1.GetVertex("a").distance);
            Assert.AreEqual(10, graph1.GetVertex("b").distance);
            Assert.AreEqual(-7, graph1.GetVertex("c").distance);
            Assert.AreEqual(-4, graph1.GetVertex("d").distance);
            Assert.AreEqual(0, graph1.GetVertex("e").distance);
            Assert.AreEqual(13, graph1.GetVertex("f").distance);
            Assert.AreEqual(long.MaxValue, graph1.GetVertex("g").distance);


            Stack<Vertex> stack = new Stack<Vertex>();

            stack.Push(graph1.GetVertex("e"));
            stack.Push(graph1.GetVertex("d"));
            stack.Push(graph1.GetVertex("c"));
            stack.Push(graph1.GetVertex("f"));
            stack.Push(graph1.GetVertex("b"));
            stack.Push(graph1.GetVertex("a"));

            var stackTest = BellmanFordShortestPath<DirectedWeightedGraph>.GetShortestPath(graph1, "e");

            while (stack.Count > 0)
            {
                Assert.AreEqual(stack.Pop(), stackTest.Pop());
            }
        }
    }
}
