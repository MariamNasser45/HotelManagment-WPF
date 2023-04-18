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
    /// Interaction logic for FinalizePayment.xaml
    /// </summary>
    public partial class FinalizePayment : Window
    {
        FRONTEND_RESERVATIONContext context;
        public FinalizePayment()
        {
            InitializeComponent();

            context = new FRONTEND_RESERVATIONContext();

            this.Closed += (sender, e) => context?.Dispose();
        }

        private void btn_next(object sender, RoutedEventArgs e)
        {
     
            Close();
        }

        private void phonyxy_GotFocus(object sender, RoutedEventArgs e)
        {
            if (CardNumber.Text == "(999) 999-999")
            {
                CardNumber.Text = "";
            }
        }

        private void Cvcplaceholder(object sender, RoutedEventArgs e)
        {
            if (TboxCVV.Text == "CVC")
            {
                TboxCVV.Text = "";
            }
        }

      
    }
}
