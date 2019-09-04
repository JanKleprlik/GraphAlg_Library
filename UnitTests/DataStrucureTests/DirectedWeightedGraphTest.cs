using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphAlgorithmLib.DataStructures;

namespace UnitTests.DataStrucureTests
{
    [TestClass]
    public class DirectedWeightedGraphTest
    {
        [TestMethod]
        public void Test()
        {
            DirectedWeightedGraph graph1 = new DirectedWeightedGraph(false);

            #region Basic stuff
            Assert.AreEqual(true, graph1.directed);
            Assert.AreEqual(true, graph1.weighted);
            Assert.AreEqual(false, graph1.multigraph);
            #endregion

            #region Vertex stuff
            for (char i = 'a'; i <= 'h'; i++)
            {
                graph1.AddVertex(Convert.ToString(i));
            }
            graph1.AddVertex("a");

            Assert.AreEqual(8, graph1.verticesCount);

            graph1.RemoveVertex("h");
            graph1.RemoveVertex("z");

            Assert.AreEqual(7, graph1.verticesCount);
            Assert.AreEqual(true, graph1.ExistVertex("a"));
            Assert.AreEqual(false, graph1.ExistVertex("z"));
            #endregion

            #region Edge stuff
            graph1.AddEdge("a", "b", 10);
            graph1.AddEdge("a", "b", 2);

            Assert.AreEqual(1, graph1.edgesCount);

            graph1.AddEdge("a", "a");
            graph1.AddEdge("a", "a", 2);

            Assert.AreEqual(1, graph1.edgesCount);

            graph1.AddEdge("a", "z", 2);
            graph1.AddEdge("z", "a", 2);
            graph1.AddEdge("y", "z", 2);

            Assert.AreEqual(1, graph1.edgesCount);

            graph1.AddEdge("b", "c", 1);
            graph1.AddEdge("c", "d", 1);
            graph1.AddEdge("d", "e", 1);
            graph1.AddEdge("e", "f", 1);
            graph1.AddEdge("f", "g", 1);

            Assert.AreEqual(6, graph1.edgesCount);

            graph1.RemoveEdge("a", "b");
            graph1.RemoveEdge("a", "c", 1);
            graph1.RemoveEdge("f", "g", 1);

            Assert.AreEqual(5, graph1.edgesCount);
            Assert.AreEqual(true, graph1.ExistEdge("a", "b"));

            Assert.AreEqual(10, graph1.GetEdgeWeight("a", "b"));

            graph1.ChangeWeight("a", "b", 10, 20);

            Assert.AreEqual(20, graph1.GetEdgeWeight("a", "b"));

            graph1.RemoveVertex("d");
            Assert.AreEqual(3, graph1.edgesCount);
            #endregion

            graph1.Clear();

            Assert.AreEqual(0, graph1.verticesCount);
            Assert.AreEqual(0, graph1.edgesCount);


            DirectedWeightedGraph graph2 = new DirectedWeightedGraph(true);

            #region Basic stuff
            Assert.AreEqual(true, graph2.directed);
            Assert.AreEqual(true, graph2.weighted);
            Assert.AreEqual(true, graph2.multigraph);
            #endregion

            #region Vertex stuff
            for (char i = 'a'; i <= 'h'; i++)
            {
                graph2.AddVertex(Convert.ToString(i));
            }
            graph2.AddVertex("a");

            Assert.AreEqual(8, graph2.verticesCount);

            graph2.RemoveVertex("h");
            graph2.RemoveVertex("z");

            Assert.AreEqual(7, graph2.verticesCount);
            Assert.AreEqual(true, graph2.ExistVertex("a"));
            Assert.AreEqual(false, graph2.ExistVertex("z"));
            #endregion

            #region Edge stuff
            graph2.AddEdge("a", "b", 10);
            graph2.AddEdge("a", "b", 2);

            Assert.AreEqual(2, graph2.edgesCount);

            graph2.AddEdge("a", "a");
            graph2.AddEdge("a", "a", 2);

            Assert.AreEqual(2, graph2.edgesCount);

            graph2.AddEdge("a", "z", 2);
            graph2.AddEdge("z", "a", 2);
            graph2.AddEdge("y", "z", 2);

            Assert.AreEqual(2, graph2.edgesCount);

            graph2.AddEdge("b", "c", 1);
            graph2.AddEdge("c", "d", 1);
            graph2.AddEdge("d", "e", 1);
            graph2.AddEdge("e", "f", 1);
            graph2.AddEdge("f", "g", 1);

            Assert.AreEqual(7, graph2.edgesCount);

            graph2.RemoveEdge("a", "b");
            graph1.RemoveEdge("a", "c", 1);
            graph2.RemoveEdge("f", "g", 1);

            Assert.AreEqual(6, graph2.edgesCount);
            Assert.AreEqual(true, graph2.ExistEdge("a", "b"));

            Assert.AreEqual(1, graph2.GetEdgeWeight("a", "b"));

            graph2.ChangeWeight("a", "b", 10, 20);

            Assert.AreEqual(1, graph2.GetEdgeWeight("a", "b"));

            graph2.RemoveVertex("d");
            Assert.AreEqual(4, graph2.edgesCount);
            #endregion

            graph2.Clear();

            Assert.AreEqual(0, graph2.verticesCount);
            Assert.AreEqual(0, graph2.edgesCount);
        }
    }
}
