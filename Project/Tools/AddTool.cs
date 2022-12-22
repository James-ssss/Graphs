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
    internal class AddTool : Tool
    {
        ToolArgs toolArgs;
        Grid newGrid;
        GraphShape shape;
        GraphVertex vertex;
        bool gridIsSaved;

        public AddTool(ToolArgs toolArgs) : base(toolArgs)
        {
            toolArgs.statusBarUpdater.UpdateCurrentState(StateBar.AddVertex);

            shape = new GraphShape();
            vertex = new GraphVertex();
            gridIsSaved = false;

            this.toolArgs = toolArgs;
            newGrid = SetGrid();
            newGrid.CaptureMouse();

            newGrid.MouseMove           += OnMouseMove;
            newGrid.MouseLeftButtonDown += OnMouseLeftButtonDown;
        }

        private void OnMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid;

            if (!gridIsSaved)
            {
                toolArgs.graphShapeRepo.AddGraphShape(shape);
            }

            gridIsSaved = true;
            grid.ReleaseMouseCapture();
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            Grid grid = sender as Grid;
            if (!grid.IsMouseCaptured) return;
            Canvas.SetLeft(grid, Mouse.GetPosition(toolArgs.canvas).X);
            Canvas.SetTop(grid, Mouse.GetPosition(toolArgs.canvas).Y);
        }

        private Grid SetGrid()
        {
            var settings = new SettingsShapes();
            var newGrid = settings.MakeGrid();

            Canvas.SetLeft(newGrid, Mouse.GetPosition(toolArgs.canvas).X);
            Canvas.SetTop(newGrid, Mouse.GetPosition(toolArgs.canvas).Y);

            toolArgs.canvas.Children.Add(newGrid);

            vertex.Id = toolArgs.graphShapeRepo.MaxIndex;
            vertex.Name = vertex.Id.ToString();

            shape.GridShape =   newGrid;
            shape.Vertex =      vertex;

            var txtBlock = newGrid.Children.OfType<StackPanel>().FirstOrDefault().Children.OfType<TextBlock>().FirstOrDefault(txtBlock => txtBlock.Name == "Name");
            if (txtBlock is not null)
                txtBlock.Text = shape.Vertex.Name;

            return newGrid;
        }

        public override void Unload()
        {
            newGrid.MouseMove               -= OnMouseMove;
            newGrid.MouseLeftButtonDown     -= OnMouseLeftButtonDown;
            Dispose();
        }
    }
}
