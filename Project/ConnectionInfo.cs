using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using Project.Core;

namespace Project.WPF
{
    /// <summary>
    /// Информация о связи двух людей
    /// </summary>
    internal class ConnectionInfo
    {
        public GraphShape           BaseShape;
        //public RelativeType         RelativeType;
        public Polyline             Connection;
        public GraphShape           DependentShape;
        public TextBox              Weight;
    }

    /// <summary>
    /// Связь человека и прямоугольника
    /// </summary>
    class GraphShape
    {
        public Grid         GridShape;
        public GraphVertex  Vertex;
        //public Person   Info;
    }
}
