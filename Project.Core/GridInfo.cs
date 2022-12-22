using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project.Core
{
    public class GridInfo
    {
        public (double left, double top) coordinates { get; set; }
        public GraphVertex vertex                    { get; set; }

        public GridInfo((double, double) coordinates, GraphVertex graphVertex)
        {
            this.coordinates =  coordinates;
            this.vertex =       graphVertex;
        }
    }
}
