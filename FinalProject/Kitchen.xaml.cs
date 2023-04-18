using Dapper;
using FinalProject.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for Kitchen.xaml
    /// </summary>
    public partial class Kitchen : Window
    {
        FoodMenu fd = new FoodMenu();

        //IDbConnection connection;

        FRONTEND_RESERVATIONContext context;
        public Kitchen()
        {
            InitializeComponent();

            //connection = new SqlConnection("Data Source=DESKTOP-UUIBUUP\\SQLEXPRESS;Initial Catalog=FRONTEND_RESERVATION;Integrated Security=True; Encrypt=false");

            context = new FRONTEND_RESERVATIONContext();

            this.Closed += (sender, e) => context?.Dispose(); // because of can't use using before context object in this to dispose we calling the form event

            LoadData();
        }

        private void btnnext_Click(object sender, RoutedEventArgs e)
        {
            fd.Show();
        }

        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            context.reservation.Load();

            var result = context.reservation.Select(r => new { r.Id , r.first_name , r.last_name , r.phone_number , 
                                                     r.room_type , r.room_floor , r.room_number , r.break_fast
                                                     , r.lunch , r.dinner , r.cleaning , r.towel , r.s_surprise
                                                     , r.supply_status , r.food_bill}).ToList();

            Kitchendatagrid.ItemsSource = result;

        }

        private void LoadData()
        {
            context.reservation.Load();

            ontmhelinelst.ItemsSource = context.reservation.Where(r => r.check_in == true).Select(r => new { reservation = r.Id + " | " + r.first_name + " " + r.last_name + " | " + r.phone_number }).ToList();

            ontmhelinelst.DisplayMemberPath = "reservation";

        }

        private void btn_back(object sender, RoutedEventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            Close();
        }

        private void btn_update(object sender, RoutedEventArgs e)
        {

            if (ontmhelinelst.SelectedValue == null)
            {
                MessageBox.Show("Select Data To update", "Warning");
            }
            var data = ontmhelinelst.SelectedValue.ToString();
            var spldata = data.Split("|");
            var splid = spldata[0].Split(" = ");
            int id = int.Parse(splid[1]);
            var upd_data = context.reservation.FirstOrDefault(r => r.Id == id);
          
            upd_data.supply_status = foodchkbox.IsChecked.Value;

            if (fd.chkboxbreakfast.IsChecked.Value)
            {
                upd_data.break_fast = int.Parse(fd.txtbreakfast.Text);
            }

            if(fd.chkboxlunch.IsChecked.Value)
            {

                upd_data.lunch = int.Parse(fd.txtlunch.Text);
           
            }

            if(fd.chkboxdinner.IsChecked.Value)
            {

                upd_data.dinner = int.Parse(fd.txtdinner.Text);

            }

            if (foodchkbox.IsChecked.Value==true)
            {
                towelchkbox.IsChecked = false;
                sweetchkbox.IsChecked = false;
                Cleaning.IsChecked = false;
            }
            upd_data.towel = (bool)fd.Towels.IsChecked.Value;
            upd_data.cleaning = (bool)fd.clean.IsChecked.Value;
            upd_data.s_surprise = (bool)fd.sweet.IsChecked.Value;

            context.reservation.Update(upd_data);
            context.SaveChanges();
            MessageBox.Show("Date Updated Successfully", "Update Info");

        }
    }
}
