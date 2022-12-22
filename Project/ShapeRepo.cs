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
    internal class ShapeRepo : IShapeRepo
    {
        private List<ConnectionInfo>    connectionInfos = new List<ConnectionInfo>();
        private List<GraphShape>        personShapes = new List<GraphShape>();

        private int _MaxIndex;
        public int MaxIndex { 
            get 
            {
                if (personShapes.Count == 0) return 0;

                int max = personShapes.Max(graph => graph.Vertex.Id);
                int min = personShapes.Min(graph => graph.Vertex.Id);

                if (max <= _MaxIndex || min >= _MaxIndex || (_MaxIndex < max && _MaxIndex > min)) {
                    _MaxIndex = max;
                }

                return ++_MaxIndex;
            } 
            private set { }
        }

        public void AddConnection(GraphShape personShape, Polyline connection, GraphShape relatedPerson)
        {
            var newPersonShape = new ConnectionInfo() 
            { 
                BaseShape =         personShape,
                Connection =        connection,
                DependentShape =    relatedPerson
            };

            connectionInfos.Add(newPersonShape);
        }

        public void AddConnection(ConnectionInfo connection)
        {
            connectionInfos.Add(connection);
        }

        public void AddGraphShape(GraphShape shape)
        {
            personShapes.Add(shape);
        }

        public void AddGridConnectionInfo(ConnectionInfo shapeInfo)
        {
            connectionInfos.Add(shapeInfo);
        }

        public GraphShape FindGraphShape(Grid grid)
        {
            foreach (var personShape in personShapes)
                if(personShape.GridShape.Equals(grid))
                    return personShape;
            return null;
        }

        public List<Polyline> GetPolylines(Grid grid)
        {
            List<Polyline> connections = new List<Polyline>();

            foreach (var shapeInfo in connectionInfos)
            {
                if(shapeInfo.BaseShape.GridShape == grid || shapeInfo.DependentShape.GridShape == grid)
                    connections.Add(shapeInfo.Connection);
            }

            return connections;
        }

        public List<ConnectionInfo> GetConnectionInfos()
        {
            return connectionInfos.ToList();
        }

        public List<GraphShape> GetPersonShapes()
        {
            return personShapes.ToList();
        }

        public void RemoveData()
        {
            connectionInfos.Clear();
            personShapes.Clear();
        }

        public void RemoveConnection(GraphShape personShape, GraphShape removeShape)
        {
            var itemToRemove = connectionInfos.SingleOrDefault(
                shapeInfo => shapeInfo.BaseShape == personShape && shapeInfo.DependentShape == removeShape
                );

            if (itemToRemove != null)
                connectionInfos.Remove(itemToRemove);
        }

        public void RemoveConnection(Polyline polyline)
        {
            var itemToRemove = connectionInfos.FirstOrDefault(shapeInfo => shapeInfo.Connection.Equals(polyline));

            if(itemToRemove != null) {
                itemToRemove.BaseShape.Vertex.RelativesIds.Remove(itemToRemove.BaseShape.Vertex.RelativesIds.First(relative => relative.Id == itemToRemove.DependentShape.Vertex.Id));
                connectionInfos.Remove(itemToRemove);
            }
        }

        public void RemoveShape(GraphShape personShape)
        {
            List<ConnectionInfo> itemsToRemove = connectionInfos.FindAll(
                shapeInfo => shapeInfo.BaseShape == personShape || shapeInfo.DependentShape == personShape
                );

            foreach (var shapeInfo in itemsToRemove)
            {
                if(connectionInfos.Contains(shapeInfo))
                {
                    connectionInfos.Remove(shapeInfo);
                }
            }

            personShapes.Remove(personShape);
        }

        public void RemoveShape(Grid grid)
        {
            RemoveShape(FindGraphShape(grid));
        }

        public ConnectionInfo FindConnectionInfo(GraphShape person, GraphShape relatedPerson)
        {
            foreach (var connection in connectionInfos)
            {
                if(connection.BaseShape == person && connection.DependentShape == relatedPerson ||
                    connection.DependentShape == person && connection.BaseShape == relatedPerson)
                    return connection;
            }
            return null;
        }

        public ConnectionInfo FindConnectionInfo(Polyline connection)
        {
            foreach (var connectionInfo in connectionInfos)
            {
                if (connectionInfo.Connection.Equals(connection))
                    return connectionInfo;
            }
            return null;
        }

        public ConnectionInfo FindConnectionInfo(TextBox textBox)
        {
            foreach (var connectionInfo in connectionInfos)
            {
                if (connectionInfo.Weight.Equals(textBox))
                    return connectionInfo;
            }
            return null;
        }
    }
}
