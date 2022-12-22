using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Project.WPF
{
    interface IShapeRepo
    {
        void RemoveShape(GraphShape personShape);
        void RemoveShape(Grid grid);
        void RemoveData();
        void AddGraphShape(GraphShape personShape);
        void AddConnection(GraphShape personShape, Polyline connection, GraphShape relatedPerson);
        void AddConnection(ConnectionInfo connection);
        void AddGridConnectionInfo(ConnectionInfo shapeInfo);
        void RemoveConnection(GraphShape personShape, GraphShape removeShape);
        GraphShape                      FindGraphShape(Grid grid);
        ConnectionInfo                  FindConnectionInfo(GraphShape person, GraphShape relatedPerson);
        List<ConnectionInfo>            GetConnectionInfos();
        List<GraphShape>                GetPersonShapes();
        List<Polyline>                  GetPolylines(Grid grid);
    }
}
