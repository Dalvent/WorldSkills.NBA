using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace NBAManagement
{
    static class FrameManager
    {
        public static Frame TargetFrame { get; set; }
        public static bool CanGoBack => TargetFrame.CanGoBack;
        public static void OpenPage(Page page) 
        {
            TargetFrame.Navigate(page);
        }
        public static void GoBack()
        {
            TargetFrame.GoBack();
        }
    }
}
