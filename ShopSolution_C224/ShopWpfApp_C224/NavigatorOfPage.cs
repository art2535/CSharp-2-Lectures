using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ShopWpfApp_C224
{
    internal class NavigatorOfPage
    {
        public static Frame MainFrame { get; set; }

        public NavigatorOfPage()
        {
            MainFrame = new Frame();
        }
    }
}
