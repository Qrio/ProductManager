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
using System.Windows.Controls.Primitives;
using System.Windows.Shapes;

namespace ProjectX_Proto2
{
    /// <summary>
    /// Interaction logic for ProductMan.xaml
    /// </summary>
    /// 
    
    
    public partial class ProductMan : Window
    {
        private Popup _MyPopup;

        public ProductMan()
        {
            InitializeComponent();
            _MyPopup = Resources["myPopup"] as Popup;
        }

        private void pnlProfile_Click(object sender, MouseButtonEventArgs e)
        {

            if (canvasProfile.Visibility == System.Windows.Visibility.Collapsed)
            {
                canvasProfile.Visibility = System.Windows.Visibility.Visible;
                pnlProfile.Background = Application.Current.FindResource("HoverAccentBrush") as SolidColorBrush;
            }
            else 
            {
                canvasProfile.Visibility = System.Windows.Visibility.Collapsed;
                pnlProfile.Background = Application.Current.FindResource("WindowAccentBrush") as SolidColorBrush;
            }
            //if (_MyPopup.IsOpen == true)
            //{
            //    _MyPopup.IsOpen = false;
            //}
            //else
            //{
            //    _MyPopup.IsOpen = true;
            //}
            
        }

        private void tglProfile_Click(object sender, RoutedEventArgs e)
        {
            if (canvasProfile.Visibility == System.Windows.Visibility.Collapsed)
            {
                canvasProfile.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                canvasProfile.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

    }
}
