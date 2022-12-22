using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project.Core
{
    public class Graph
    {
        public List<GridInfo> graph = new List<GridInfo>();

        [JsonConstructor]
        public Graph(List<GridInfo> gridInfos)
        {
            this.graph = gridInfos;
        }

        public Graph() { }
    }
}
