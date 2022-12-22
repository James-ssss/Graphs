using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using Project.Core;

namespace Project.WPF.Tools
{
    static class CanvasProperties
    {
        public static string  Path { get; set; }
        public static bool isUploaded = false;
    }

    internal class SaverCanvas
    {
        ToolArgs args;

        public SaverCanvas(ToolArgs args)
        {
            this.args = args;
        }

        public void SaveCanvas()
        {
            if (!CanvasProperties.isUploaded)
            {
                SaveFileDialog saveDlg = new SaveFileDialog();
                saveDlg.Filter = "Текст формата json (*.json)|*.json";
                if (saveDlg.ShowDialog() == true)
                {
                    CanvasProperties.Path = saveDlg.FileName;
                    CanvasProperties.isUploaded = true;
                    FileWriter.GenerateJson(saveDlg.FileName, GetAllGrids(args.canvas, args.graphShapeRepo));
                }
            }
            else
            {
                FileWriter.GenerateJson(CanvasProperties.Path, GetAllGrids(args.canvas, args.graphShapeRepo));
            }
        }

        public Graph GetAllGrids(Canvas canvas, ShapeRepo shapeRepo)
        {
            Graph graph = new Graph();

            var grids = canvas.Children.OfType<Grid>().ToList();
            foreach (var grid in grids)
            {
                (double left, double top) coordinates = (Canvas.GetLeft(grid), Canvas.GetTop(grid));
                var vertex = shapeRepo.FindGraphShape(grid).Vertex;
                graph.graph.Add(new GridInfo(coordinates, vertex));
            }

            return graph;
        }
    }
}
