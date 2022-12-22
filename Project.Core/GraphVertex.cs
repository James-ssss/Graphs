using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project.Core
{
    public class Relative
    {
        public int Id { get; set; }
        public int Weight { get; set; }
    }

    public class GraphVertex
    {
        public string Name              { get; set; }
        public int Id                   { get; set; }
        public List<Relative> RelativesIds = new List<Relative>();

        [JsonConstructor]
        public GraphVertex(string name, int id, List<Relative> relativesIds)
        {
            Name =              name;
            Id =                id;
            this.RelativesIds = relativesIds;
        }

        public GraphVertex() { }
    }
}
