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
using System.Windows.Media.Animation;

namespace ProjectX_Proto2
{
    /// <summary>
    /// Interaction logic for ProductMan.xaml
    /// </summary>
    /// 


    public partial class ProductMan : Window
    {

        public ProductMan()
        {
            InitializeComponent();
            this.canvasSettings.Visibility = System.Windows.Visibility.Collapsed;
            this.canvasProfile.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void tglProfile_Click(object sender, RoutedEventArgs e)
        {
            if (canvasSettings.Visibility == System.Windows.Visibility.Visible)
            {
                tglSettings.IsChecked = false;
                tglSettings.RaiseEvent(new RoutedEventArgs(ToggleButton.ClickEvent));
            }
            canvasProfile.Visibility = canvasProfile.Visibility == System.Windows.Visibility.Collapsed ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;            
        }

        private void btnPurchaseHistory_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Purchase History");
        }

        private void btnVendors_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Vendors");
        }

        private void btnItemsPrices_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Item & Prices");
        }


        private void tglSettings_Click(object sender, RoutedEventArgs e)
        {
            if (canvasProfile.Visibility == System.Windows.Visibility.Visible)
            {
                tglProfile.IsChecked = false;
                tglProfile.RaiseEvent(new RoutedEventArgs(ToggleButton.ClickEvent));
            }
            canvasSettings.Visibility = canvasSettings.Visibility == System.Windows.Visibility.Collapsed ? System.Windows.Visibility.Visible : System.Windows.Visibility.Collapsed;
        }

        private void tglNewItems_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tglMoreProducts_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tglNew_Click(object sender, RoutedEventArgs e)
        {

        }



    }
}
