using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using Project.Core;

namespace Project.WPF.Tools
{
    internal class ConnectionTool : Tool
    {
        ToolArgs        args;
        Polyline        polyline;
        PointCollection points;
        SettingsShapes  settings;
        TextBox         text;
        //RelativeType relativeType;

        ConnectionInfo shapeInfo;

        public ConnectionTool(ToolArgs args) : base(args)
        {
            args.statusBarUpdater.UpdateCurrentState(StateBar.Connection);

            shapeInfo = new ConnectionInfo();
            points =    new PointCollection();
            settings =  new SettingsShapes();

            this.args = args;
            AddGridEvent(SetLineOnShape);
        }

        private void SetLineOnShape(object sender, MouseEventArgs e)
        {
            //relativeType = args.correctRelative;
            var grid = sender as Grid;
            var circle = grid.Children.OfType<Ellipse>().FirstOrDefault();
            shapeInfo.BaseShape = args.graphShapeRepo.FindGraphShape(grid);

            polyline = settings.DrawPolyline();

            polyline.MouseRightButtonDown += PolylineOnMouseBtnDown;

            points.Add(settings.GetBasePoint((Canvas.GetLeft(grid), Canvas.GetTop(grid)), (circle.Width, circle.Height), false));
            points.Add(points[0]);
            polyline.Points = points;

            shapeInfo.Connection = polyline;

            args.canvas.Children.Add(polyline);

            RemoveGridEvent(SetLineOnShape);
            AddGridEvent(SetPolylineOnShapeDown);

            args.canvas.MouseMove += CanvasOnMouseMove;
        }

        private void PolylineOnMouseBtnDown(object sender, MouseButtonEventArgs e)
        {
            var polyline = sender as Polyline;

            args.canvas.Children.Remove(polyline);

            if (shapeInfo.BaseShape is not null)
            {
                args.graphShapeRepo.RemoveConnection(polyline);

                shapeInfo = new ConnectionInfo();
                points = new PointCollection();

                RemoveGridEvent(SetPolylineOnShapeDown);
                RemoveGridEvent(SetLineOnShape);
                args.canvas.MouseMove -= CanvasOnMouseMove;

                AddGridEvent(SetLineOnShape);

                return;
            }

            args.canvas.Children.Remove(args.graphShapeRepo.FindConnectionInfo(polyline).Weight);
            args.graphShapeRepo.RemoveConnection(polyline);
        }


        private void SetPolylineOnShapeDown(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            var circle = grid.Children.OfType<Ellipse>().FirstOrDefault();
            shapeInfo.DependentShape = args.graphShapeRepo.FindGraphShape(grid);

            args.canvas.MouseMove -= CanvasOnMouseMove;

            if (shapeInfo.BaseShape != shapeInfo.DependentShape
                && !ShapeInfoExtensions.ConnectionIsExists(args.graphShapeRepo.GetConnectionInfos(), shapeInfo))
            {
                points[points.Count - 1] = settings.GetBasePoint((Canvas.GetLeft(grid), Canvas.GetTop(grid)), (circle.Width, circle.Height), true);

                text = settings.MakeTextBox();

                Canvas.SetTop(text, (points[0].Y + points[points.Count - 1].Y) / 2);
                Canvas.SetLeft(text, (points[0].X + points[points.Count - 1].X) / 2);

                text.TextChanged += WeightTextChanged;
                args.canvas.Children.Add(text);

                shapeInfo.Weight = text;

                args.graphShapeRepo.AddGridConnectionInfo(shapeInfo);
                shapeInfo.BaseShape.Vertex.RelativesIds.Add(new Relative { Id = shapeInfo.DependentShape.Vertex.Id, Weight = int.Parse(text.Text) });
            }
            else
                args.canvas.Children.Remove(polyline);

            shapeInfo = new ConnectionInfo();
            points =    new PointCollection();
            polyline =  null;
            text =      null;

            RemoveGridEvent(SetPolylineOnShapeDown);
            AddGridEvent(SetLineOnShape);
        }

        private void WeightTextChanged(object sender, TextChangedEventArgs e)
        {
            var txtBox = (TextBox)sender;

            if (txtBox.Text == "" || txtBox.Text == null || !int.TryParse(txtBox.Text, out int weight) || weight < 0)
                txtBox.Text = "0";

            var connection = args.graphShapeRepo.FindConnectionInfo(txtBox);
            connection.BaseShape.Vertex.RelativesIds.First(relative => relative.Id == connection.DependentShape.Vertex.Id).Weight = int.Parse(txtBox.Text);
        }

        private void CanvasOnMouseMove(object sender, MouseEventArgs e)
        {
            var canvas = sender as Canvas;

            //Updates.UpdatePoints(points, relativeType);
            points[points.Count-1] = new Point(Mouse.GetPosition(canvas).X, Mouse.GetPosition(canvas).Y);
        }

        private void AddGridEvent(params MouseButtonEventHandler[] eventHandlers)
        {
            var canvas = args.canvas;

            foreach (var component in canvas.Children)
            {
                if (component is Grid grid)
                {
                    foreach(var handler in eventHandlers)
                        grid.MouseLeftButtonDown += handler;
                }
            }
        }

        private void RemoveGridEvent(params MouseButtonEventHandler[] eventHandler)
        {
            var canvas = args.canvas;

            foreach (var component in canvas.Children)
            {
                if (component is Grid grid)
                {
                    foreach (var handler in eventHandler)
                        grid.MouseLeftButtonDown -= handler;
                }
            }
        }

        public override void Unload()
        {
            RemoveGridEvent(SetPolylineOnShapeDown, SetLineOnShape);
            args.canvas.MouseMove -= CanvasOnMouseMove;
            if (shapeInfo is not null)
                args.canvas.Children.Remove(shapeInfo.Connection);
            Dispose();
        }
    }
}
