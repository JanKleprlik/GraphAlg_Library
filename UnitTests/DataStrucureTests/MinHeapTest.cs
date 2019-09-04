using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphAlgorithmLib.DataStructures;

namespace UnitTests.DataStrucureTests
{
    [TestClass]
    public class MinHeapTest
    {
        [TestMethod]
        public void Test()
        {
            var graph = new DirectedUnweightedGraph(false);
            int x = 0;

            for (int i = 'a'; i < 'h'; i++)
            {

                graph.AddVertex(Convert.ToString(Convert.ToChar(i)));
                graph.GetVertex(Convert.ToString(Convert.ToChar(i))).distance = x;
                x++;
            }

            var heap = new MinHeap<DirectedUnweightedGraph>(graph);


            for (int i = 'a'; i < 'g'; i++)
            {
                heap.Add(graph.GetVertex(Convert.ToString(Convert.ToChar(i))));
            }

            Assert.AreEqual(graph.GetVertex("a"), heap.Peek());
            Assert.AreEqual(graph.GetVertex("a"), heap.GetMin());
            Assert.AreEqual(graph.GetVertex("b"), heap.GetMin());

            graph.GetVertex("g").distance = -2;
            heap.Add(graph.GetVertex("g"));

            Assert.AreEqual(graph.GetVertex("g"), heap.GetMin());

            Assert.IsFalse(heap.Empty());

            graph.GetVertex("d").distance = -20;
            heap.Update(graph.GetVertex("d"));

            Assert.AreEqual(graph.GetVertex("d"), heap.GetMin());
            Assert.AreEqual(graph.GetVertex("c"), heap.GetMin());
            Assert.AreEqual(graph.GetVertex("e"), heap.GetMin());
            Assert.AreEqual(graph.GetVertex("f"), heap.GetMin());

            Assert.IsTrue(heap.Empty());
        }
    }
}
