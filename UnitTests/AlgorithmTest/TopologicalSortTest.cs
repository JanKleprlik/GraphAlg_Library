using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphAlgorithmLib.Algorithms;
using GraphAlgorithmLib.DataStructures;
using System.Collections.Generic;

namespace UnitTests.AlgorithmTest
{
    [TestClass]
    public class TopologicalSortTest
    {
        [TestMethod]
        public void Test()
        {

            var graph1 = new DirectedWeightedGraph(false);

            for (int i = 'a'; i <= 'h'; i++)
            {
                graph1.AddVertex(Convert.ToString(Convert.ToChar(i)));
            }

            graph1.AddEdge("a", "b", 1);
            graph1.AddEdge("b", "c", 2);
            graph1.AddEdge("c", "d", 3);
            graph1.AddEdge("a", "g", 600);
            graph1.AddEdge("b", "f", 0);
            graph1.AddEdge("e", "b", 7);


            Stack<Vertex> stack = new Stack<Vertex>();
            stack.Push(graph1.GetVertex("d"));
            stack.Push(graph1.GetVertex("c"));
            stack.Push(graph1.GetVertex("f"));
            stack.Push(graph1.GetVertex("b"));
            stack.Push(graph1.GetVertex("g"));
            stack.Push(graph1.GetVertex("a"));
            stack.Push(graph1.GetVertex("e"));
            stack.Push(graph1.GetVertex("h"));

            var testStack = TopologicalSort<DirectedWeightedGraph>.Sort(graph1);
            for (int i = 'a'; i <= 'h'; i++) 
            {
                Assert.AreEqual(stack.Pop(), testStack.Pop());
            }

        }
    }
}
