using System;
using System.Collections.Generic;
using System.Text;

namespace GraphAlgorithmLib.DataStructures
{
    public class GraphBase :IGraph
    {
        #region Basic stuff
        /// <summary>
        /// Vrátí TRUE když je graf orientovaný, jinak FALSE.
        /// </summary>
        public virtual bool directed { get; } = false;

        /// <summary>
        /// Vrátí TRUE když je graf ohodnocený, jinak FALSE.
        /// </summary>
        public virtual bool weighted { get; } = false;

        /// <summary>
        /// Vrátí počet vrcholů.
        /// </summary>
        public virtual int verticesCount
        {
            get
            {
                return vertices.Count;
            }
        }

        /// <summary>
        /// Vrátí počet hran.
        /// </summary>
        public virtual int edgesCount { get; set; } = 0;

        /// <summary>
        /// Vrátí TRUE když jsou povoleny multihrany, jinak FALSE.
        /// </summary>
        public virtual bool multigraph { get; set; }

        /// <summary>
        /// Vrátí list s vrcholy.
        /// </summary>
        public List<Vertex> vertices { get; } = new List<Vertex>();

        public List<Edge> edges { get; } = new List<Edge>();

        #endregion



        #region Vertex stuff
        /// <summary>
        /// Přidá vrchol do grafu.
        /// </summary>
        public virtual void AddVertex(string name)
        {
            if (!CheckVertex(name))
            {
                Vertex vertex = new Vertex(name);
                vertices.Add(vertex);
            }
            else
            {
                Console.WriteLine("Can not add vertex\"" + name + "\". It is already in the graph.");
            }
        }

        /// <summary>
        /// Odebere vrchol z grafu.
        /// </summary>
        public virtual void RemoveVertex(string name)
        {
            if (CheckVertex(name))
            {
                Stack<Edge> stack = new Stack<Edge>();
                Vertex vertex = FindVertex(name);
                foreach (Edge edge in vertex.outgoingEdges)
                {
                    stack.Push(edge);
                }
                while (stack.Count > 0)
                {
                    Edge removeEdge = stack.Pop();
                    RemoveEdge(removeEdge.source.name, removeEdge.destination.name);
                }
                foreach (Edge edge in vertex.incomingEdges)
                {
                    stack.Push(edge);
                }
                while (stack.Count > 0)
                {
                    Edge removeEdge = stack.Pop();
                    RemoveEdge(removeEdge.source.name, removeEdge.destination.name);
                }
                vertices.Remove(vertex);
            }
            else
            {
                Console.WriteLine("Can not remove vertex \"" + name + "\". It is not in the graph.");
            }
        }

        /// <summary>
        /// Vrátí TRUE když vrchol existuje, jinak FALSE.
        /// </summary>
        public virtual bool ExistVertex(string name)
        {
            if (CheckVertex(name))
            {
                return true;
            }
            else return false;
        }

        /// <summary>
        /// Najde vrchol podle jména.
        /// </summary>
        public virtual Vertex GetVertex(string name)
        {
            return FindVertex(name);
        }


        protected virtual bool CheckVertex(string name)
        {
            try
            {
                Vertex vertex = FindVertex(name);
                return true;
            }
            catch (VertexNotInGraph)
            {
                return false;
            }
        }

        protected virtual Vertex FindVertex(string name)
        {
            foreach (Vertex vertex in vertices)
            {
                if (vertex.name == name)
                {
                    return vertex;
                }
            }
            throw new VertexNotInGraph();
        }

        protected virtual Vertex CheckAndFindVertex(string name)
        {

            if (!CheckVertex(name))
            {
                throw new VertexNotInGraph();
            }
            else return FindVertex(name);

        }

        #endregion


        #region Edge stuff

        /// <summary>
        /// Přidá hranu do grafu.
        /// </summary>
        public virtual void AddEdge(string sourceName, string destinationName)
        {
            try
            {
                Vertex source = CheckAndFindVertex(sourceName);
                Vertex destination = CheckAndFindVertex(destinationName);
                if (source.Equals(destination))
                {
                    Console.WriteLine("Loops are not allowed.");
                    return;
                }


                if (!CheckEdge(source, destination) || multigraph)
                {
                    Edge edge = new Edge(source, destination,false);
                    source.outgoingEdges.Add(edge);
                    destination.incomingEdges.Add(edge);
                    AddNeighbour(source, destination);
                    edges.Add(edge);
                    edgesCount++;
                }
            }
            catch (VertexNotInGraph)
            {
                Console.WriteLine("Chosen vertices \"" + sourceName + "\" or \"" + destinationName + "\" are not in the graph.");
            }
        }

        /// <summary>
        /// Odebere hranu z grafu.
        /// </summary>
        public virtual void RemoveEdge(string sourceName, string destinationName)
        {
            try
            {
                Vertex source = CheckAndFindVertex(sourceName);
                Vertex destination = CheckAndFindVertex(destinationName);
                Edge edge = FindEdge(source, destination);
                source.neighbours.Remove(destination);
                source.outgoingEdges.Remove(edge);
                destination.incomingEdges.Remove(edge);
                edgesCount--;
            }
            catch (VertexNotInGraph)
            {
                Console.WriteLine("Chosen vertices \"" + sourceName + "\" or \"" + destinationName + "\" are not in the graph.");
            }
            catch (EdgeNotInGraph)
            {
                Console.WriteLine("Edge from \"" + sourceName + "\" to \"" + destinationName + "is not in the graph");
            }
        }

        /// <summary>
        /// Vrátí TRUE když hrana mezi vrcholy existuje, jinak FALSE.
        /// </summary>
        public virtual bool ExistEdge(string sourceName, string destinationName)
        {
            try
            {
                Vertex source = CheckAndFindVertex(sourceName);
                Vertex destination = CheckAndFindVertex(destinationName);
                if (CheckEdge(source, destination))
                {
                    return true;
                }
                else return false;
            }
            catch (VertexNotInGraph)
            {
                Console.WriteLine("Chosen vertices \"" + sourceName + "\" or \"" + destinationName + "\" are not in the graph.");
                return false;
            }
        }

        /// <summary>
        /// Změní cenu hrany z oldWeight na newWeight.
        /// </summary>
        public virtual void ChangeWeight(string sourceName, string destinationName, long oldWeight, long newWeight)
        {
            if(weighted==false)
            {
                Console.WriteLine("This graph is unweighted. You can not change weights.");
            }
            else
            {
                throw new NotImplementedException("This graph is supposed to be unweighted. Method must be implemented.");
            }
        }

        /*
          
         
            try
            {
                Vertex source = CheckAndFindVertex(sourceName);
                Vertex destination = CheckAndFindVertex(destinationName);
                if (CheckEdge(source, destination))
                {
                    Edge edge = FindEdge(source, destination, oldWeight);
                    edge.weight = newWeight;
                }
            }
            catch (VertexNotInGraph)
            {
                Console.WriteLine("Chosen vertices \"" + sourceName + "\" or \"" + destinationName + "\" are not in the graph.");
            }
            catch (EdgeNotInGraph)
            {
                Console.WriteLine("Edge from \"" + sourceName + "\" to \"" + destinationName + "\" weighted \"" + oldWeight + "\" is not in the graph");
            }


        */

        public virtual long GetEdgeWeight(string sourceName, string destinationName)
        {
            try
            {
                if (weighted)
                {
                    Vertex source = FindVertex(sourceName);
                    Vertex destination = FindVertex(destinationName);

                    Edge edge = FindEdge(source, destination);
                    return edge.weight;
            }
                else
                {
                    Console.WriteLine("This graph is not weighted.");
                    return 1;
                }
            }
            catch (VertexNotInGraph)
            {
                Console.WriteLine("Chosen vertices \"" + sourceName + "\" or \"" + destinationName + "\" are not in the graph.");
                return long.MaxValue;
            }
            catch (EdgeNotInGraph)
            {
                Console.WriteLine("Edge from \"" + sourceName + "\" to \"" + destinationName + "is not in the graph");
                return long.MaxValue;
            }
        }
        /// <summary>
        /// Přidá souseda k vrcholu.
        /// </summary>
        protected virtual void AddNeighbour(Vertex vertex, Vertex neighbour)
        {
            vertex.neighbours.Add(neighbour);
        }

        /// <summary>
        /// Vrátí TRUE, když hrana existuje, jinak FALSE
        /// </summary>
        protected virtual bool CheckEdge(Vertex source, Vertex destination)
        {
            if (source.neighbours.Contains(destination))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Vrátí neoceněnou hranu s určitým oceněním, pokud hrana neexistuje vyhodí exception
        /// </summary>
        protected virtual Edge FindEdge(Vertex source, Vertex destination)
        {
            foreach (Edge candidate1 in source.outgoingEdges)
            {
                foreach (Edge candidate2 in destination.incomingEdges)
                {
                    if (candidate1.Equals(candidate2))
                    {
                        return candidate1;
                    }
                }
            }
            throw new EdgeNotInGraph();
        }

        // TOHLE NAJDE OHODNOCENOU HRANU
        /* 
         
   
        /// <summary>
        /// Vrátí oceněnou hranu s určitým oceněním, pokud hrana neexistuje vyhodí exception
        /// </summary>
        protected Edge FindEdge(Vertex source, Vertex destination, long weight)
        {
            foreach (Edge candidate1 in source.outgoingEdges)
            {
                foreach (Edge candidate2 in destination.incomingEdges)
                {
                    if ((candidate1 == candidate2) && candidate1.weight == weight && candidate2.weight == weight)
                    {
                        return candidate1;
                    }
                }
            }
            throw new EdgeNotInGraph();
        }

    */
        #endregion



        public virtual void Clear()
        {
            vertices.Clear();
            edgesCount = 0;
        }
    }
}
