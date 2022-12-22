using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Project.WPF.Tools;

namespace Project.WPF
{
    public partial class MainWindow : Window
    {
        Tool tool = null;
        ToolArgs toolArgs;
        //PersonArgs personArgs;
        StatusBarUpdater statusUpdater;

        public MainWindow()
        {
            InitializeComponent();

            statusUpdater = new StatusBarUpdater(lblCoordinateInfo, lblStateInfo);
            toolArgs = new ToolArgs(this, mainCanvas, statusUpdater, new ShapeRepo(), GraphType.WeightedGraph);
            //personArgs = new PersonArgs(btnSaveData, txtBoxPersonName, txtBoxPersonBirthDate, txtBoxPersonDeathDate);
            this.DataContext = toolArgs;
            lblGraphType.Content = toolArgs.graphType;

            tool = new ArrowTool(toolArgs);
        }

        private void btnAddPerson_Click(object sender, RoutedEventArgs e)
        {
            if (tool != null) tool.Unload();
            tool = new AddTool(toolArgs);
        }

        private void btnConnection_Click(object sender, RoutedEventArgs e)
        {
            if (tool != null) tool.Unload();
            tool = new ConnectionTool(toolArgs);
        }

        private void btnRemovePerson_Click(object sender, RoutedEventArgs e)
        {
            if (tool != null) tool.Unload();
            tool = new RemoveTool(toolArgs);
        }

        private void btnArrow_Click(object sender, RoutedEventArgs e)
        {
            if (tool != null) tool.Unload();
            tool = new ArrowTool(toolArgs);
        }

        private void btnUploadCanvas_Click(object sender, RoutedEventArgs e)
        {
            var uploadingCanvas = new UploadingCanvas(toolArgs);
            uploadingCanvas.OpenCanvas();
            btnArrow_Click(sender, e);
        }

        private void btnSaveCanvas_Click(object sender, RoutedEventArgs e)
        {
            var saveCanvas = new SaverCanvas(toolArgs);
            saveCanvas.SaveCanvas();
        }

        private void mainCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            statusUpdater.UpdateCoordinatesLabel(Mouse.GetPosition(this));
        }

        private void BaseGraphBtn_Click(object sender, RoutedEventArgs e)
        {
            toolArgs.graphType = GraphType.WeightedGraph;
            lblGraphType.Content = toolArgs.graphType;
        }

        private void NetworkGraphBtn_Click(object sender, RoutedEventArgs e)
        {
            toolArgs.graphType = GraphType.TransportNetwork;
            lblGraphType.Content = toolArgs.graphType;
        }
    }
}
