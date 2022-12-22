using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Project.Core;

namespace Project.WPF
{
    static class Updates
    {
        //    public static void UpdatePoints(PointCollection points, RelativeType type)
        //    {
        //        switch (type)
        //        {
        //            case RelativeType.Parent:
        //                ExpandPointCollection(points, 2);
        //                points[1] = new Point(points[0].X, points[0].Y + Math.Abs(points[0].Y - points[3].Y) / 4);
        //                points[2] = new Point(points[3].X, points[1].Y);
        //                break;

        //            case RelativeType.Child:
        //                ExpandPointCollection(points, 2);
        //                points[1] = new Point(points[0].X, points[3].Y + Math.Abs(points[0].Y - points[3].Y) / 4);
        //                points[2] = new Point(points[3].X, points[1].Y);
        //                break;

        //            case RelativeType.Spouses:
        //            case RelativeType.FormerSpouses:
        //                ExpandPointCollection(points, 2);
        //                points[1] = new Point(points[0].X + (points[3].X - points[0].X) / 2, points[0].Y);
        //                points[2] = new Point(points[1].X, points[3].Y);
        //                break;
        //        }
        //    }

        //    static void ExpandPointCollection(PointCollection points, int count)
        //    {
        //        if (points.Count <= 2)
        //        {
        //            for (int i = 0; i < count; i++)
        //            {
        //                points.Add(points[points.Count - 1]);
        //            }
        //        }
        //    }

        public static void UpdateTextBoxes(Grid newGrid, GraphVertex vertex)
        {
            StackPanel stackPanel = newGrid.Children.OfType<StackPanel>().FirstOrDefault();
            var txtBlocks = stackPanel.Children.OfType<TextBlock>().ToList();
            txtBlocks.First(txtBlock => txtBlock.Name == "Name").Text = vertex.Name;
        }
    }
}
