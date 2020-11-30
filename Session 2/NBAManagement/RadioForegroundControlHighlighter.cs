using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace NBAManagement
{
    class RadioForegroundControlHighlighter
    {
        private Control highlighted = null;
        private readonly Brush highlighteBrush;
        private readonly Brush standartBrush;

        public RadioForegroundControlHighlighter(Brush highlighteBrush, Brush standartBrush)
        {
            this.highlighteBrush = highlighteBrush;
            this.standartBrush = standartBrush;
        }

        public void HighlightControl(Control control)
        {
            if(highlighted != null)
                highlighted.Foreground = standartBrush;

            control.Foreground = highlighteBrush;
            highlighted = control;
        }
    }
}
