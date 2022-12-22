using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Project.WPF.Tools
{
    internal class ToolArgs
    {
        public MainWindow mainWindow                { get; private set; }
        public Canvas canvas                        { get; private set; }
        public StatusBarUpdater statusBarUpdater    { get; private set; }
        public ShapeRepo graphShapeRepo             { get; private set; }
        public GraphType graphType                  { get; set; }

        public ToolArgs(MainWindow mainWindow, Canvas canvas, StatusBarUpdater statusBarUpdater, ShapeRepo shapeRepo, GraphType graphType)
        {
            this.mainWindow =       mainWindow;
            this.canvas =           canvas;
            this.statusBarUpdater = statusBarUpdater;
            this.graphShapeRepo =   shapeRepo;
            this.graphType =        graphType;
        }
    }
}
