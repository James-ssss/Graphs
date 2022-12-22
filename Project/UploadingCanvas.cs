using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using Project.Core;
using System.Windows.Input;

namespace Project.WPF.Tools
{
    class UploadingCanvas
    {
        ToolArgs toolArgs;

        public UploadingCanvas(ToolArgs toolArgs)
        {
            this.toolArgs = toolArgs;
        }

        public void OpenCanvas()
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.Filter = "Текст формата json (*.json)|*.json";
            if (openDlg.ShowDialog() == true)
            {
                CanvasProperties.Path = openDlg.FileName;
                CanvasProperties.isUploaded = true;
                DrawCanvas(FileReader.ParseGridInfosJson(openDlg.FileName));
            }
        }

        void DrawCanvas(Graph gridInfos)
        {
            DrawGrids(gridInfos);
            DrawConnections();
        }

        void DrawGrids(Graph graph)
        {
            toolArgs.canvas.Children.Clear();
            toolArgs.graphShapeRepo.RemoveData();
            var settings = new SettingsShapes();
            var gridInfos = graph.graph;
            foreach (var grid in gridInfos)
            {
                var newGrid = settings.MakeGrid();

                Canvas.SetLeft(newGrid, grid.coordinates.left);
                Canvas.SetTop(newGrid, grid.coordinates.top);

                var shape = new GraphShape() { Vertex = grid.vertex, GridShape = newGrid };
                toolArgs.graphShapeRepo.AddGraphShape(shape);

                Updates.UpdateTextBoxes(newGrid, grid.vertex);

                toolArgs.canvas.Children.Add(newGrid);
            }
        }

        void DrawConnections()
        {
            var settings = new SettingsShapes();
            var grids = toolArgs.canvas.Children.OfType<Grid>().ToList();
            foreach (var grid in grids)
            {
                var personShape = toolArgs.graphShapeRepo.FindGraphShape(grid);
                foreach (var relativeInfo in personShape.Vertex.RelativesIds)
                {
                    var connection = new ConnectionInfo();
                    connection.BaseShape = personShape;
                    var relatedPerson = toolArgs.graphShapeRepo.GetPersonShapes().Find(sh => sh.Vertex.Id == relativeInfo.Id);
                    connection.DependentShape = relatedPerson;
                    if (!ShapeInfoExtensions.ConnectionIsExists(toolArgs.graphShapeRepo.GetConnectionInfos(), connection))
                    {
                        Polyline polyline = settings.DrawPolyline();
                        polyline.MouseRightButtonDown += PolylineOnMouseBtnDown;
                        TextBox txtBox =    settings.MakeTextBox();

                        var relatedGrid = grids.Find(gr => gr == relatedPerson.GridShape);
                        PointCollection points = settings.GetPoints(grid, relatedGrid);

                        polyline.Points = points;

                        Canvas.SetTop(txtBox, (points[0].Y + points[points.Count - 1].Y) / 2);
                        Canvas.SetLeft(txtBox, (points[0].X + points[points.Count - 1].X) / 2);

                        txtBox.Text = personShape.Vertex.RelativesIds.First(relative => relative.Id == relatedPerson.Vertex.Id).Weight.ToString();

                        connection.Connection = polyline;
                        connection.Weight =     txtBox;

                        toolArgs.graphShapeRepo.AddConnection(connection);
                        toolArgs.canvas.Children.Add(txtBox);
                        toolArgs.canvas.Children.Add(polyline);
                    }
                }
            }
        }

        private void PolylineOnMouseBtnDown(object sender, MouseButtonEventArgs e)
        {
            var polyline = sender as Polyline;

            toolArgs.canvas.Children.Remove(polyline);

            toolArgs.canvas.Children.Remove(toolArgs.graphShapeRepo.FindConnectionInfo(polyline).Weight);
            toolArgs.graphShapeRepo.RemoveConnection(polyline);
        }
    }
}
