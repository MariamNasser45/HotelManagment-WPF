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
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        Login_ManagerContext LoginContext;
        public Login()
        {
            InitializeComponent();

            LoginContext = new Login_ManagerContext();

            this.Closed += (sender, e) => LoginContext?.Dispose();
        }



        private void btnlicen_Click(object sender, RoutedEventArgs e)
        {
            License ls = new License();
            ls.Show();
        }

        private void btnsignin_Click(object sender, RoutedEventArgs e)
        {
            if (txtusrname.Text == "UserName" || passbox.Password == "Password")
            {
                MessageBox.Show("Both UserName and Password are required, please Enter Them","Error Massege");
            }

            var usname=LoginContext.frontend.Select(a => a.user_name).ToList();
            var uspass = LoginContext.frontend.Select(a => a.pass_word).ToList();
            var Kitname = LoginContext.kitchen.Select(a => a.user_name).ToList();
            var Kitpass = LoginContext.kitchen.Select(a => a.pass_word).ToList();


            if ((txtusrname.Text == usname[0] && passbox.Password == uspass[0]) ||
                (txtusrname.Text == usname[1] && passbox.Password == uspass[1])
                ||(txtusrname.Text == usname[2] && passbox.Password == uspass[2]))
            {

                Frontend fr = new Frontend();
                fr.Show();
                Close();

            }


           else if ((txtusrname.Text == Kitname[0] && passbox.Password == Kitpass[0])
                || (txtusrname.Text == Kitname[1] && passbox.Password == Kitpass[1]))
            {

                Kitchen kitchen = new Kitchen();
                kitchen.Show();
                Close();
            }

            else
            {
                MessageBox.Show("UserName Or Password is wrong ");
            }
        }



        private void txtusrname_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtusrname.Text == "UserName")
            {
                txtusrname.Text = "";
                lblusrname.Content = "UserName";
            }
        }

        private void passbox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (passbox.Password == "Password")
            {
                passbox.Password = "";
                lblpass.Content = "Password";
            }
        }
    }
}
