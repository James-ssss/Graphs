using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.WPF
{
    internal class ShapeInfoExtensions
    {
        public static bool ConnectionIsExists(List<ConnectionInfo> shapeInfos, ConnectionInfo shape)
        {
            foreach (ConnectionInfo shapeInfo in shapeInfos)
            {
                if ((shapeInfo.BaseShape == shape.BaseShape && shapeInfo.DependentShape == shape.DependentShape) ||
                    (shapeInfo.DependentShape == shape.BaseShape && shapeInfo.BaseShape == shape.DependentShape))
                    return true;
            }
            return false;
        }
    }
}
