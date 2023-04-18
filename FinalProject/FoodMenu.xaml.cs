using FinalProject.Models;
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
using System.Windows.Shapes;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for FoodMenu.xaml
    /// </summary>
    public partial class FoodMenu : Window
    {
        FRONTEND_RESERVATIONContext context;
        public FoodMenu()
        {
            InitializeComponent();

            context = new FRONTEND_RESERVATIONContext();

            this.Closed += (sender, e) => context?.Dispose();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if(chkboxbreakfast.IsChecked == true) 
            {
                txtbreakfast.IsEnabled = true;
            }
        }

        private void chkboxlunch_Checked(object sender, RoutedEventArgs e)
        {
            if (chkboxlunch.IsChecked == true)
            {
                txtlunch.IsEnabled = true;
            }
            else 
            {
                txtlunch.IsEnabled = false; 
            }
        }

        private void chkboxdinner_Checked(object sender, RoutedEventArgs e)
        {
            if (chkboxdinner.IsChecked == true)
            {
                txtdinner.IsEnabled = true;
            }
        }

        private void txtlunch_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtlunch.Text == "Quantity?")
            {
                txtlunch.Text = "";
            }
        }

        private void txtdinner_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtdinner.Text == "Quantity?")
            {
                txtdinner.Text = "";
            }
        }

        private void txtbreakfast_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtbreakfast.Text == "Quantity?")
            {
                txtbreakfast.Text = "";
            }
        }

        private void btn_next(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
