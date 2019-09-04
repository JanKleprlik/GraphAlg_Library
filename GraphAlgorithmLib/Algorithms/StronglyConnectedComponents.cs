using System;
using System.Collections.Generic;
using GraphAlgorithmLib.DataStructures;

namespace GraphAlgorithmLib.Algorithms
{
    public static class StronglyConnectedComponents<TGraph> where TGraph : IGraph
    {
        /// <summary>
        /// Najde silně souvislé komponenty grafu.
        /// </summary>
        public static List<List<Vertex>> GetComponents(TGraph graph) 
        {
            if (graph == null)
            {
                throw new ArgumentException("Use graph that is not empty.");
            }

            if (!graph.directed)
            {
                throw new Exception("Graph must be directed.");
            }

            List<List<Vertex>> components = new List<List<Vertex>>();

            Stack<Vertex>topologicalStack = CloseTime(graph);

            Stack<Vertex> revertedTopologicalStack = RevertStack(topologicalStack);

            foreach (Vertex vertex in graph.vertices)
            {
                vertex.visited = false;
            }

            while (revertedTopologicalStack.Count > 0)
            {
                Vertex activeVertex = revertedTopologicalStack.Pop();
                if (!activeVertex.visited)
                {
                    components.Add(Visit(activeVertex));
                }
            }
            return components;
        }
        /// <summary>
        /// Spočítá časy uzavčení vrcholů.
        /// </summary>
        private static Stack<Vertex> CloseTime(TGraph graph)
        {
            var stack = new Stack<Vertex>();

            foreach (Vertex vertex in graph.vertices)
            {
                vertex.visited = false;
            }

            foreach (Vertex vertex in graph.vertices)
            {
                if (!vertex.visited)
                {
                    vertex.visited = true;
                    foreach (Vertex neighbour in vertex.neighbours)
                    {
                        if (!neighbour.visited)
                        {
                            neighbour.visited = true;
                            VisitCloseTime(neighbour, ref stack);
                            stack.Push(neighbour);
                        }
                    }
                    stack.Push(vertex);
                }


            }

            return stack;
        }

        /// <summary>
        /// Pomocná funkce pro počítání času uzavření vrcholů.
        /// </summary>
        private static void VisitCloseTime(Vertex vertex, ref Stack<Vertex> stack)
        {
            foreach (Vertex neighbour in vertex.neighbours)
            {
                if (!neighbour.visited)
                {
                    neighbour.visited = true;
                    VisitCloseTime(neighbour, ref stack);
                    stack.Push(neighbour);
                }
            }

        }
        /// <summary>
        /// Obrátí pořadní prvků v zásobníku.
        /// </summary>
        private static Stack<Vertex> RevertStack(Stack<Vertex> stack)
        {
            Stack<Vertex> revertedStack = new Stack<Vertex>();
            while (stack.Count > 0)
            {
                revertedStack.Push(stack.Pop());
            }
            return revertedStack;
        }
        /// <summary>
        /// Navštíví vrchol a vrátí komponentu.
        /// </summary>
        private static List<Vertex> Visit(Vertex vertex)
        {
            List<Vertex> component = new List<Vertex>();
            Queue<Vertex> queue = new Queue<Vertex>();

            queue.Enqueue(vertex);

            while (queue.Count > 0)
            {
                Vertex activeVertex = queue.Dequeue();

                if (!activeVertex.visited)
                {
                    activeVertex.visited = true;
                    component.Add(activeVertex);

                    foreach (Vertex neighbour in vertex.neighbours)
                    {
                        if (!neighbour.visited)
                        {
                            queue.Enqueue(neighbour);
                        }
                    }
                }
            }
            return component;

        }
    }
}
