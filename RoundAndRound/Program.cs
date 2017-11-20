using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace RoundAndRound
{
    class Program
    {
        static void Main(string[] args)
        {
            PublicTransportGraph graph = new PublicTransportGraph();

            FileParser parser = new FileParser();
            parser.Load("buses.txt");

            //File.WriteAllText("lines.txt", parser.lineStore.GetAll());
            //File.WriteAllText("stops.txt", parser.stopStore.GetAll());

            int nVert = parser.stopStore.Count;
            for (int i = 0; i < nVert; i++) graph.AddVertex(new Vertex());

            int nEdges = 0;

            for (int line = 0; line < parser.lineStops.Count; line++)
            {
                List<int> current = parser.lineStops[line];
                for (int i = 0; i < current.Count; i++)
                {
                    for (int j = i+1; j < current.Count; j++)
                    {
                        if (current[i] == current[j]) continue;
                        Edge edge = new Edge();
                        edge.line = line;
                        edge.vertexId1 = current[i];
                        edge.vertexId2 = current[j];
                        graph.AddEdge(edge);
                        nEdges++;
                    }
                }
            }

            Console.WriteLine("Enter start: ");
            int start = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter length:");
            int length = int.Parse(Console.ReadLine());

            File.WriteAllText("routes.txt", string.Format("Routes with length {0} from {1} to {1}\n", length, start, start));

            while (true)
            {
                List<Edge> route = graph.GetRandomRoute(start, length);

                int prev = start;
                for (int i = 0; i < route.Count; i++)
                {
                    int other = route[i].GetOther(prev);
                    Console.WriteLine("{3}. {0} -> {1}\t({2})", prev, other, route[i].line, i+1);
                    File.AppendAllText("routes.txt", string.Format("{0}. {1} -> {2}\t({3})\n", i + 1, parser.stopStore.GetString(prev),
                                    parser.stopStore.GetString(other), parser.lineStore.GetString(route[i].line)));
                    prev = other;
                }
                File.AppendAllText("routes.txt", "\n");
                Console.ReadLine();
            }
    
            Console.ReadLine();
        }
    }
}
