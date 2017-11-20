using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundAndRound
{
    public class PublicTransportGraph
    {
        public List<Vertex> vertices = new List<Vertex>();
        public List<Edge> edges = new List<Edge>();

        private int lines = 0;
        private Random random = new Random();

        public int maxLength = 6;

        public List<List<Edge>> GetAllRoutesDontRepeatVertices(int start)
        {
            List<List<Edge>> result = new List<List<Edge>>();
            bool[] used = new bool[vertices.Count];

            used[start]= true;
            List<Edge> temp = new List<Edge>();
            FindRouteRecDontRepeatVertices(start, start, used, temp, result);

            return result;
        }

        public List<List<Edge>> GetAllRoutesDontRepeatAnything(int start)
        {
            List<List<Edge>> result = new List<List<Edge>>();
            bool[] usedLines = new bool[lines];
            bool[] usedVertices = new bool[vertices.Count];

            usedLines[start] = true;
            List<Edge> temp = new List<Edge>();
            FindRouteRecDontRepeatEdges(start, start, usedVertices, usedLines, temp, result);

            return result;
        }

        public List<Edge> GetRandomRoute(int start, int length)
        {
            List<Edge> route = new List<Edge>();
            bool[] usedLines = new bool[lines];
            bool[] usedVerts = new bool[vertices.Count];

            GetRandomRouteRec(start, start, usedLines, usedVerts, length, route);
            return route;
        }

        private bool GetRandomRouteRec(int current, int start, bool[] usedLines, bool[] usedVertices, int length, List<Edge> route)
        {
            List<Edge> currentEdges = vertices[current].edges;
            ChooseList order = new ChooseList(currentEdges.Count, random);

            for (int i = 0; i < currentEdges.Count; i++)
            {
                Edge e = currentEdges[order.Choose()];
                int other = e.GetOther(current);

                if (usedLines[e.line]) continue;

                if (other == start)
                {
                    if (route.Count == length - 1)
                    {
                        route.Add(e);
                        return true;
                    }
                    else return false;
                }

                if (route.Count >= length - 1)
                {
                    return false; // too long
                }
                if (usedVertices[other]) continue;

                usedVertices[other] = true;
                usedLines[e.line] = true;
                route.Add(e);

                if (GetRandomRouteRec(other, start, usedLines, usedVertices, length, route)) return true;

                route.RemoveAt(route.Count - 1);
                usedLines[e.line] = false;
                usedVertices[other] = false;
            }

            return false;
        }

        public void AddVertex(Vertex v)
        {
            vertices.Add(v);
        }

        public void AddEdge(Edge e)
        {
            edges.Add(e);
            vertices[e.vertexId1].edges.Add(e);
            vertices[e.vertexId2].edges.Add(e);
            if (e.line >= lines) lines = e.line + 1;
        }

        private void FindRouteRecDontRepeatEdges(int v, int target, bool[] usedVertices, bool[] usedLines, List<Edge> route, List<List<Edge>> allRoutes)
        {
            if (route.Count >= maxLength) return;
            Vertex current = vertices[v];
            foreach (Edge edge in current.edges)
            {
                if (usedLines[edge.line]) continue;

                int otherId = edge.GetOther(v);
                if (usedVertices[otherId]) continue;

                if (otherId == target)
                {
                    List<Edge> finishedRoute = new List<Edge>(route.Count);
                    foreach (Edge e in route) finishedRoute.Add(e);
                    finishedRoute.Add(edge);
                    allRoutes.Add(finishedRoute);
                    continue;
                }

                usedVertices[otherId] = true;
                usedLines[edge.line] = true;
                route.Add(edge);
                FindRouteRecDontRepeatEdges(otherId, target, usedVertices, usedLines, route, allRoutes);
                route.RemoveAt(route.Count - 1);
                usedLines[edge.line] = false;
                usedVertices[otherId] = false;
            }
        }

        private void FindRouteRecDontRepeatVertices(int v, int target, bool[] used, List<Edge> route, List<List<Edge>> allRoutes)
        {
            Vertex current = vertices[v];
            foreach (Edge edge in current.edges)
            {                
                int otherId = edge.GetOther(v);

                if (otherId == target)
                {
                    List<Edge> finishedRoute = new List<Edge>(route.Count);
                    foreach (Edge e in route) finishedRoute.Add(e);
                    finishedRoute.Add(edge);
                    allRoutes.Add(finishedRoute);
                    continue;
                }

                if (!used[otherId])
                {
                    used[otherId] = true;
                    route.Add(edge);
                    FindRouteRecDontRepeatVertices(otherId, target, used, route, allRoutes);
                    route.RemoveAt(route.Count - 1);
                    used[otherId] = false;
                }
            }
        }
    }
}
