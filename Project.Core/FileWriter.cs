using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Newtonsoft.Json;

namespace Project.Core
{
    public class FileWriter : FileWorker
    {
        protected override DataPath Path { get; set; }
        protected override PathExtension PathExtension { get; set; }

        public FileWriter(DataPath? path = DataPath.graphs, PathExtension pathExtension = PathExtension.json)
        {
            if (path == null) return;

            ChangePath((DataPath)path, pathExtension);
        }

        public void WriteFile(string text)
        {
            File.WriteAllText(FullPath, text);
        }

        public static void GenerateJson(string path, object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            File.WriteAllText(path, json);
        }
    }
}