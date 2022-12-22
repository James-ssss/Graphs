using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Project.WPF
{
    internal class StatusBarUpdater
    {
        private Label lblCoordinates;
        private Label lblState;

        public StatusBarUpdater(Label lblCoordinates, Label lblState)
        {
            this.lblCoordinates = lblCoordinates;
            this.lblState = lblState;
        }

        public void Update(StateBar state, Point? point)
        {
            UpdateCoordinatesLabel(point);
            UpdateCurrentState(state);
        }

        public void UpdateCoordinatesLabel(Point? point)
        {
            if (point != null)
                lblCoordinates.Content = string.Format("X: {0:f1}, Y: {1:f1}", point.Value.X, point.Value.Y);
        }

        public void UpdateCurrentState(StateBar state)
        {
            lblState.Content = state.ToString();
        }
    }
}
