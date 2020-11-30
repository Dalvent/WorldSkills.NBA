using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace NBAManagement
{
    class RadioBackgroundControlHighlighter
    {
        public Control Highlighted { get; private set; } = null;
        private readonly Brush highlighteBrush;
        private readonly Brush standartBrush;

        public RadioBackgroundControlHighlighter(Brush highlighteBrush, Brush standartBrush)
        {
            this.highlighteBrush = highlighteBrush;
            this.standartBrush = standartBrush;
        }

        public void HighlightControl(Control control)
        {
            if(Highlighted != null)
                Highlighted.Background = highlighteBrush;

            control.Background = standartBrush;
            Highlighted = control;
        }
    }
}
