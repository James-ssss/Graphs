using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project.Core
{
    public class FileReader : FileWorker
    {
        protected override DataPath Path { get; set; }
        protected override PathExtension PathExtension { get; set; }

        public FileReader(DataPath? path = DataPath.graphs, PathExtension pathExtension = PathExtension.json)
        {
            if (path == null) return;

            ChangePath((DataPath)path, pathExtension);
        }

        public static Graph ParseGridInfosJson(string path)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Graph>(json);
        }
    }
}
