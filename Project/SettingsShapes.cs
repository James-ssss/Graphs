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

namespace Project.WPF
{
    internal class SettingsShapes
    {
        const int rectHeight = 80;
        const int rectWidth =  rectHeight;

        public Polyline DrawPolyline()
        {
            var stroke =            Brushes.RosyBrown;
            double thickness =      4;
            var strokeDashArray =   new DoubleCollection();

            var polyline = new Polyline()
            {
                Stroke =            stroke,
                StrokeThickness =   thickness,
                StrokeDashArray =   strokeDashArray,
                StrokeEndLineCap =  PenLineCap.Round,
                StrokeLineJoin =    PenLineJoin.Round,
                StrokeDashCap =     PenLineCap.Round,
            };

            Panel.SetZIndex(polyline, -2);
            return polyline;
        }

        public TextBox MakeTextBox()
        {
            var txtBox = new TextBox()
            {
                Name = "WeightTxtBox",
                FontSize = 15,
                BorderThickness = new Thickness(0),
                TextWrapping = TextWrapping.Wrap,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Background = null,
                BorderBrush = null,
                CaretBrush = null,
                SelectionBrush = Brushes.RosyBrown,
                Text = "0",
            };

            Panel.SetZIndex(txtBox, -1);

            return txtBox;
        }

        public Point GetBasePoint((double left, double top) coordinates, (double width, double height) rect, bool isRelative)
        {
            Point point = new Point();
            
            point = new Point(coordinates.left + rect.width / 2, coordinates.top + rect.height / 2);

            return point;
        }   

        public PointCollection GetPoints(Grid grid, Grid relatedGrid)
        {
            PointCollection points = new PointCollection();
            var relatedCircle = relatedGrid.Children.OfType<Ellipse>().FirstOrDefault();
            var circle = grid.Children.OfType<Ellipse>().FirstOrDefault();
            points.Add(GetBasePoint((Canvas.GetLeft(grid), Canvas.GetTop(grid)), (circle.Width, circle.Height), false));
            points.Add(GetBasePoint((Canvas.GetLeft(relatedGrid), Canvas.GetTop(relatedGrid)),
                (relatedCircle.Width, relatedCircle.Height), true));
            //Updates.UpdatePoints(points, type);

            return points;
        }

        public Grid MakeGrid()
        {
            var newGrid = new Grid();
            var circle = MakeCircle();

            newGrid.Children.Add(circle);

            var stackPanel = MakeStackPanel(circle);
            newGrid.Children.Add(stackPanel);

            return newGrid;
        }

        private Ellipse MakeCircle()
        {
            var circle = new Ellipse
            {
                Width =                 rectWidth,
                Height =                rectHeight,
                VerticalAlignment =     VerticalAlignment.Top,
                HorizontalAlignment =   HorizontalAlignment.Left,
                Fill =                  Brushes.AntiqueWhite,
                Stroke =                Brushes.Black,
                StrokeThickness =       0.5,
            };

            return circle;
        }

        private StackPanel MakeStackPanel(Ellipse circle)
        {
            var stackPanel = new StackPanel() { Margin = new Thickness(10, 10, 10, 10), HorizontalAlignment = HorizontalAlignment.Center, VerticalAlignment = VerticalAlignment.Center };
            List<TextBlock> textBlocks = new List<TextBlock>() {
                new TextBlock {
                    Name = "Name",
                    FontSize = 20,
                    TextTrimming = TextTrimming.WordEllipsis,
                    TextWrapping = TextWrapping.Wrap,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                }
            };

            foreach (var txtBlock in textBlocks)
            {
                stackPanel.Children.Add(txtBlock);
            }

            return stackPanel;
        }
    }
}
