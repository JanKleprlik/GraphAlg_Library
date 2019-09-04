using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphAlgorithmLib.DataStructures;


namespace UnitTests.DataStrucureTests
{
    [TestClass]
    public class UndirectedUnweightedGraphTest
    {
        [TestMethod]
        public void Test()
        {
            UndirectedUnweightedGraph graph1 = new UndirectedUnweightedGraph(false);

            #region Basic stuff
            Assert.AreEqual(false, graph1.directed);
            Assert.AreEqual(false, graph1.weighted);
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
            graph1.AddEdge("a", "b");
            graph1.AddEdge("a", "b");

            Assert.AreEqual(1, graph1.edgesCount);

            graph1.AddEdge("a", "a");

            Assert.AreEqual(1, graph1.edgesCount);

            graph1.AddEdge("a", "z");
            graph1.AddEdge("z", "a");
            graph1.AddEdge("y", "z");

            Assert.AreEqual(1, graph1.edgesCount);

            graph1.AddEdge("b", "c");
            graph1.AddEdge("c", "d");
            graph1.AddEdge("d", "e");
            graph1.AddEdge("e", "f");
            graph1.AddEdge("f", "g");

            Assert.AreEqual(6, graph1.edgesCount);

            graph1.RemoveEdge("a", "c");
            graph1.RemoveEdge("f", "g");

            Assert.AreEqual(5, graph1.edgesCount);
            Assert.AreEqual(true, graph1.ExistEdge("a", "b"));
            Assert.AreEqual(true, graph1.ExistEdge("b", "a"));

            graph1.RemoveVertex("d");
            Assert.AreEqual(3, graph1.edgesCount);

            Assert.AreEqual(1, graph1.GetEdgeWeight("a", "b"));
            #endregion

            graph1.Clear();

            Assert.AreEqual(0, graph1.verticesCount);
            Assert.AreEqual(0, graph1.edgesCount);


            UndirectedUnweightedGraph graph2 = new UndirectedUnweightedGraph(true);

            #region Basic stuff
            Assert.AreEqual(false, graph2.directed);
            Assert.AreEqual(false, graph2.weighted);
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
            graph2.AddEdge("a", "b");
            graph2.AddEdge("a", "b");

            Assert.AreEqual(2, graph2.edgesCount);

            graph2.AddEdge("a", "a");

            Assert.AreEqual(2, graph2.edgesCount);

            graph2.AddEdge("a", "z");
            graph2.AddEdge("z", "a");
            graph2.AddEdge("y", "z");

            Assert.AreEqual(2, graph2.edgesCount);

            graph2.AddEdge("b", "c");
            graph2.AddEdge("c", "d");
            graph2.AddEdge("d", "e");
            graph2.AddEdge("e", "f");
            graph2.AddEdge("f", "g");

            Assert.AreEqual(7, graph2.edgesCount);

            graph1.RemoveEdge("a", "c");
            graph2.RemoveEdge("f", "g");

            Assert.AreEqual(6, graph2.edgesCount);
            Assert.AreEqual(true, graph2.ExistEdge("a", "b"));

            graph2.RemoveVertex("d");
            Assert.AreEqual(4, graph2.edgesCount);
            #endregion

            graph2.Clear();

            Assert.AreEqual(0, graph2.verticesCount);
            Assert.AreEqual(0, graph2.edgesCount);
        }
    }
}
