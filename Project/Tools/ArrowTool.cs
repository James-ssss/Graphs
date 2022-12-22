using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Project.WPF.Tools
{
    internal class ArrowTool : Tool
    {
        ToolArgs toolArgs;
        SettingsShapes settings;
        Grid grid;

        public ArrowTool(ToolArgs toolArgs) : base(toolArgs)
        {
            toolArgs.statusBarUpdater.UpdateCurrentState(StateBar.None);

            this.toolArgs = toolArgs;

            settings = new SettingsShapes();

            AddRectangleEvents();
        }

        private void ShapeDragOnMouseMove(object sender, MouseEventArgs e)
        {
            var grid = sender as Grid;
            var circle = grid.Children.OfType<Ellipse>().FirstOrDefault();
            if (!grid.IsMouseCaptured) return;
            Canvas.SetLeft(grid, Mouse.GetPosition(toolArgs.canvas).X - circle.Width / 2);
            Canvas.SetTop(grid, Mouse.GetPosition(toolArgs.canvas).Y - circle.Height / 2);

            foreach (var shapeInfo in toolArgs.graphShapeRepo.GetConnectionInfos())
            {
                if (shapeInfo.BaseShape.GridShape == grid)
                {
                    shapeInfo.Connection.Points[0] =
                        settings.GetBasePoint((Mouse.GetPosition(toolArgs.canvas).X - circle.Width / 2,
                        Mouse.GetPosition(toolArgs.canvas).Y - circle.Height / 2),
                        (circle.Width, circle.Height), false);
                    //Updates.UpdatePoints(shapeInfo.Connection.Points, shapeInfo.RelativeType);
                }
                else if (shapeInfo.DependentShape.GridShape == grid)
                {
                    shapeInfo.Connection.Points[shapeInfo.Connection.Points.Count - 1] =
                        settings.GetBasePoint((Mouse.GetPosition(toolArgs.canvas).X - circle.Width / 2,
                        Mouse.GetPosition(toolArgs.canvas).Y - circle.Height / 2),
                        (circle.Width, circle.Height), true);
                    //Updates.UpdatePoints(shapeInfo.Connection.Points, shapeInfo.RelativeType);
                }

                Canvas.SetTop(shapeInfo.Weight, (shapeInfo.Connection.Points[0].Y + shapeInfo.Connection.Points[shapeInfo.Connection.Points.Count - 1].Y) / 2);
                Canvas.SetLeft(shapeInfo.Weight, (shapeInfo.Connection.Points[0].X + shapeInfo.Connection.Points[shapeInfo.Connection.Points.Count - 1].X) / 2);
            }
        }

        private void ShapeDragOnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            this.grid = grid;
            var rect = grid.Children.OfType<Rectangle>().FirstOrDefault();

            grid.CaptureMouse();
        }

        private void ShapeDragOnMouseUp(object sender, MouseButtonEventArgs e)
        {
            var grid = sender as Grid;
            grid.ReleaseMouseCapture();
        }

        private void AddRectangleEvents()
        {
            var canvas = toolArgs.canvas;

            foreach (var component in canvas.Children)
            {
                if (component is Grid grid)
                {
                    grid.MouseLeftButtonDown += ShapeDragOnMouseDown;
                    grid.MouseLeftButtonUp +=   ShapeDragOnMouseUp;
                    grid.MouseMove +=           ShapeDragOnMouseMove;
                }
            }
        }

        private void RemoveRectangleEvents()
        {
            var canvas = toolArgs.canvas;

            foreach (var component in canvas.Children)
            {
                if (component is Grid grid)
                {
                    grid.MouseLeftButtonDown -= ShapeDragOnMouseDown;
                    grid.MouseLeftButtonUp -=   ShapeDragOnMouseUp;
                    grid.MouseMove -=           ShapeDragOnMouseMove;
                }
            }
        }

        public override void Unload()
        {
            RemoveRectangleEvents();
            Dispose();
        }
    }
}
