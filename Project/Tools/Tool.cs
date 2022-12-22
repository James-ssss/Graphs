using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Media.Effects;
using System.Windows.Media;
using System.Windows.Controls;

namespace Project.WPF.Tools
{
    internal abstract class Tool
    {
        ToolArgs args;
        List<Shape> shapes;

        public Tool(ToolArgs args)
        {
            this.args = args;
            args.canvas.MouseMove += BaseOnMouseMove;
        }

        protected void BaseOnMouseMove(object sender, MouseEventArgs e)
        {
            shapes = GetHoveredShapes();
            DrawHoverEffect(shapes);
        }

        protected void DrawHoverEffect(List<Shape> shapes)
        {
            ClearHoverEffect();

            foreach (Shape shape in shapes)
            {
                shape.Effect = new DropShadowEffect() { 
                    BlurRadius = 35,
                    ShadowDepth = 0,
                    Color = Color.FromRgb(255, 255, 0)
                };
            }
        }

        protected void ClearHoverEffect()
        {
            var canvas = args.canvas;

            foreach(var component in canvas.Children)
            {
                if (component is Grid grid)
                {
                    var shape = grid.Children.OfType<Shape>().FirstOrDefault();
                    if (shape is not null && shape.Effect != null)
                        shape.Effect = null;
                }

                if (component is Polyline polyline)
                {
                    if (polyline is not null && polyline.Effect != null)
                        polyline.Effect = null;
                }
            }
        }

        protected List<Shape> GetHoveredShapes()
        {
            var canvas = args.canvas;
            List<Shape> shapes = new List<Shape>();

            foreach (var component in canvas.Children)
            {
                if(component is Grid grid)
                {
                    var shape = grid.Children.OfType<Shape>().FirstOrDefault();
                    if (grid.IsMouseOver)
                        shapes.Add(shape);
                }

                if(component is Polyline polyline)
                {
                    if (polyline.IsMouseOver)
                        shapes.Add(polyline);
                }
            }

            return shapes;
        }

        public abstract void Unload();

        public void Dispose()
        {
            args.canvas.MouseMove -= BaseOnMouseMove;

            args.statusBarUpdater.UpdateCurrentState(StateBar.None);
        }
    }
}
