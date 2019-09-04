using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphAlgorithmLib.Algorithms;
using GraphAlgorithmLib.DataStructures;
using System.Collections.Generic;

namespace UnitTests.AlgorithmTest
{
    [TestClass]
    public class StronglyConnectedComponentsTest
    {
        [TestMethod]
        public void Test()
        {
            var graph1 = new DirectedWeightedGraph(false);

            for (int i = 'a'; i <= 'e'; i++)
            {
                graph1.AddVertex(Convert.ToString(Convert.ToChar(i)));
            }

            graph1.AddEdge("a", "b", 1);
            graph1.AddEdge("b", "a", 2);
            graph1.AddEdge("c", "a", 3);
            graph1.AddEdge("c", "b", 600);
            graph1.AddEdge("c", "d", 0);
            graph1.AddEdge("d", "c", 7);
            graph1.AddEdge("d", "b", 7);

            var list = new List<List<Vertex>>();

            var subList1 = new List<Vertex>();
            var subList2 = new List<Vertex>();
            var subList3 = new List<Vertex>();

            subList1.Add(graph1.GetVertex("b"));
            subList1.Add(graph1.GetVertex("a"));

            subList2.Add(graph1.GetVertex("d"));
            subList2.Add(graph1.GetVertex("c"));

            subList3.Add(graph1.GetVertex("e"));

            list.Add(subList1);
            list.Add(subList2);
            list.Add(subList3);

            var testList = StronglyConnectedComponents<DirectedWeightedGraph>.GetComponents(graph1);


            Assert.AreEqual(list.Count, testList.Count);

            for (int subList = 0; subList <= 1; subList++)
            {
                for (int vertex = 0; vertex <=1; vertex++)
                {
                    Assert.AreEqual(list[subList][vertex], testList[subList][vertex]);
                }
            }

            Assert.AreEqual(list[2][0], testList[2][0]);

        }
    }
}
