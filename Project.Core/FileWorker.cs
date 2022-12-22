using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core
{
    public abstract class FileWorker
    {
        protected abstract DataPath Path { get; set; }
        protected abstract PathExtension PathExtension { get; set; }
        protected string FullPath { get; set; }

        public void ChangePath(DataPath path, PathExtension pathExtension)
        {
            Path = path;
            PathExtension = pathExtension;
            FullPath = Path + '.' + PathExtension.ToString();

            if (!File.Exists(FullPath))
                File.Create(FullPath);
        }

        public string GetPath()
        {
            return FullPath;
        }
    }
}
