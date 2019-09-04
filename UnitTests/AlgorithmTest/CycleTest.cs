using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphAlgorithmLib.DataStructures;
using GraphAlgorithmLib.Algorithms;

namespace UnitTests.AlgorithmTest
{
    [TestClass]
    public class CycleTest
    {
        [TestMethod]
        public void Test()
        {
            #region Orientovaná kružnice

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
            graph1.AddEdge("e", "b", 7);


            Assert.IsTrue(Cycle.ContainsCycle<DirectedWeightedGraph>(graph1));

            graph1.Clear();

            #endregion

            #region Orientovaný bez kružnice

            // orientovaný ohodnocený.
            var graph3 = new DirectedUnweightedGraph(false);

            for (int i = 'a'; i < 'h'; i++)
            {
                graph3.AddVertex(Convert.ToString(Convert.ToChar(i)));
            }

            
            graph3.AddEdge("a", "b");
            graph3.AddEdge("b", "c");
            graph3.AddEdge("c", "d");
            graph3.AddEdge("a", "g");
            graph3.AddEdge("b", "f");
            graph3.AddEdge("e", "b");

            Assert.IsFalse(Cycle.ContainsCycle<DirectedUnweightedGraph>(graph3));

            graph3.Clear();

            #endregion
        }
    }
}
