using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoundAndRound
{
    public class Edge
    {
        public int vertexId1, vertexId2;
        public int line;
        
        public int GetOther(int vertexId)
        {
            if (vertexId == vertexId1) return vertexId2;
            else return vertexId1;
        }
    }
}
