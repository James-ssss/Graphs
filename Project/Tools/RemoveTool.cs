using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Project.WPF.Tools
{
    internal class RemoveTool : Tool
    {
        ToolArgs args;

        public RemoveTool(ToolArgs args) : base(args)
        {
            args.statusBarUpdater.UpdateCurrentState(StateBar.RemoveVertex);

            this.args = args;

            AddEvent();
        }

        private void AddEvent()
        {
            var canvas = args.canvas;

            foreach (var component in canvas.Children)
            {
                if (component is Grid grid)
                {
                    grid.MouseLeftButtonDown += OnMouseLeftButtonDown;
                }
            }
        }

        private void RemoveEvent()
        {
            var canvas = args.canvas;

            foreach (var component in canvas.Children)
            {
                if (component is Grid grid)
                {
                    grid.MouseLeftButtonDown -= OnMouseLeftButtonDown;
                }
            }
        }

        private void OnMouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            var grid = sender as Grid;

            foreach (var shapeInfo in args.graphShapeRepo.GetConnectionInfos())
            {
                if(shapeInfo.BaseShape.GridShape == grid || shapeInfo.DependentShape.GridShape == grid)
                {
                    args.canvas.Children.Remove(shapeInfo.Connection);
                    args.canvas.Children.Remove(shapeInfo.Weight);
                    if (shapeInfo.DependentShape.GridShape == grid) shapeInfo.BaseShape.Vertex.RelativesIds.Remove(shapeInfo.BaseShape.Vertex.RelativesIds.First(relative => relative.Id == shapeInfo.DependentShape.Vertex.Id));
                }
            }

            args.graphShapeRepo.RemoveShape(grid);

            args.canvas.Children.Remove(grid);
        }

        public override void Unload()
        {
            RemoveEvent();
            Dispose();
        }
    }
}
