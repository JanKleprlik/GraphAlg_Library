using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GraphAlgorithmLib.DataStructures;
using GraphAlgorithmLib.Algorithms;
using System.Collections.Generic;

namespace UnitTests.AlgorithmTest
{
    [TestClass]
    public class FloydWarshallShortestPathTest
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

            long[,] distances = new long[graph1.verticesCount, graph1.verticesCount];
            Vertex[,] followers = new Vertex[graph1.verticesCount, graph1.verticesCount];

            for (int i = 0; i < graph1.verticesCount; i++)
            {
                for (int j = 0; j < graph1.verticesCount; j++)
                {
                    distances[i, j] = long.MaxValue;
                    followers[i, j] = null;

                }
            }

            for (int i = 0; i <graph1.verticesCount; i++)
            {
                distances[i, i] = 0;
            }

            #region distances
            // vzdálenosti z "a"
            distances[0, 1] = 10;
            distances[0, 2] = -7;
            distances[0, 3] = -4;
            distances[0, 4] = 0;
            distances[0, 5] = 13;

            // vzdálenosti z "b"
            distances[1, 2] = -17;
            distances[1, 3] = -14;
            distances[1, 4] = -10;
            distances[1, 5] = 3;

            //vzdálenposti z "c"
            distances[2, 1] = 17;
            distances[2, 3] = 3;
            distances[2, 4] = 7;
            distances[2, 5] = 20;

            //vzdálenposti z "d"
            distances[3, 1] = 14;
            distances[3, 2] = -3;
            distances[3, 4] = 4;
            distances[3, 5] = 17;

            //vzdálenposti z "e"
            distances[4, 1] = 10;
            distances[4, 2] = -7;
            distances[4, 3] = -4;
            distances[4, 5] = 13;

            //vzdálenposti z "f"
            distances[5, 1] = -3;
            distances[5, 2] = -20;
            distances[5, 3] = -17;
            distances[5, 4] = -13;

            //"g" je nedosažitelný

            #endregion

            #region followers
            // předchůdci k cestě z "a"
            followers[0, 0] = graph1.GetVertex("a");
            followers[0, 1] = graph1.GetVertex("b");
            followers[0, 2] = graph1.GetVertex("b");
            followers[0, 3] = graph1.GetVertex("b");
            followers[0, 4] = graph1.GetVertex("b");
            followers[0, 5] = graph1.GetVertex("b");

            // předchůdci k cestě z "b"
            followers[1, 1] = graph1.GetVertex("b");
            followers[1, 2] = graph1.GetVertex("f");
            followers[1, 3] = graph1.GetVertex("f");
            followers[1, 4] = graph1.GetVertex("f");
            followers[1, 5] = graph1.GetVertex("f");

            // předchůdci k cestě z "c"
            followers[2, 2] = graph1.GetVertex("c");
            followers[2, 1] = graph1.GetVertex("d");
            followers[2, 3] = graph1.GetVertex("d");
            followers[2, 4] = graph1.GetVertex("d");
            followers[2, 5] = graph1.GetVertex("d");

            // předchůdci k cestě z "d"
            followers[3, 3] = graph1.GetVertex("d");
            followers[3, 1] = graph1.GetVertex("e");
            followers[3, 2] = graph1.GetVertex("e");
            followers[3, 4] = graph1.GetVertex("e");
            followers[3, 5] = graph1.GetVertex("e");

            // předchůdci k cestě "e"
            followers[4, 4] = graph1.GetVertex("e");
            followers[4, 1] = graph1.GetVertex("b");
            followers[4, 2] = graph1.GetVertex("b");
            followers[4, 3] = graph1.GetVertex("b");
            followers[4, 5] = graph1.GetVertex("b");

            // předchůdci k cestě "f"
            followers[5, 5] = graph1.GetVertex("f");
            followers[5, 1] = graph1.GetVertex("c");
            followers[5, 2] = graph1.GetVertex("c");
            followers[5, 3] = graph1.GetVertex("c");
            followers[5, 4] = graph1.GetVertex("c");

            //"g" je nedosažitelný
            followers[6, 6] = graph1.GetVertex("g");

            #endregion

            var (distancesTest, followersTest) = FloydWarshallShortestPath<DirectedWeightedGraph>.Search(graph1).ToTuple();

            for (int i = 0; i < graph1.verticesCount; i++)
            {
                for (int j = 0; j < graph1.verticesCount; j++)
                {
                    Assert.AreEqual(distances[i, j], distancesTest[i, j]);
                    Assert.AreEqual(followers[i, j], followersTest[i, j]);

                }
            }

            Stack<Vertex> path = new Stack<Vertex>();

            path.Push(graph1.GetVertex("a"));
            path.Push(graph1.GetVertex("b"));
            path.Push(graph1.GetVertex("f"));
            path.Push(graph1.GetVertex("c"));
            path.Push(graph1.GetVertex("d"));
            path.Push(graph1.GetVertex("e"));


            Stack<Vertex> pathTest = FloydWarshallShortestPath<DirectedWeightedGraph>.GetShortestPath(graph1, "a", "e", ref followersTest);

            while (path.Count > 0)
            {
                Assert.AreEqual(path.Pop(), pathTest.Pop());
            }

        }
    }
}
