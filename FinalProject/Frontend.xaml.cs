using FinalProject.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
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
using static System.Net.Mime.MediaTypeNames;

namespace FinalProject
{
    /// <summary>
    /// Interaction logic for Frontend.xaml
    /// </summary>
    public partial class Frontend : Window
    {
        FRONTEND_RESERVATIONContext context;
        FoodMenu foodMenu = new FoodMenu();
        FinalizePayment finalizePayment = new FinalizePayment();
        public Frontend()
        {
            InitializeComponent();
           
            Load();
            context = new FRONTEND_RESERVATIONContext();
           
            this.Closed += (sender, e) => context?.Dispose(); // because of can't use using before context object in this to dispose we calling the form event

        }


        #region First part "resevation"



        #region buttons

        private void btnfoodmenu(object sender, RoutedEventArgs e)
        {
            foodMenu.Show();
            foodMenu.specialned.Visibility = Visibility.Visible;
        }

        private void brfinalizebill(object sender, RoutedEventArgs e)
        {
            finalizePayment.Show();
        }


        private void Button_GotFocus(object sender, RoutedEventArgs e)
        {
            btndelete.Visibility = Visibility.Visible;
            deletborder.Visibility = Visibility.Visible;

            updatebtn.Visibility = Visibility.Visible;
            //updatebtn.IsEnabled = false;

            combobox.Visibility = Visibility.Visible;

            var result = context.reservation.Select(r => new { reservation = r.Id + "|" + r.first_name + " " + r.last_name + "|" + r.phone_number }).ToList();
            
            combobox.ItemsSource = result;

            combobox.DisplayMemberPath= "reservation";

        }

        #endregion

        #region Making placeHolder
        public void Load()
        {
            fnametxt.Text = "First";
            lnametxt.Text = "Last";
            Citytxt.Text = "City";
            phonnum.Text = "(999) 999-999";
            txtemail.Text = "Enter Your Email Address";
            apttxt.Text = "APT./Suite";
            addtxt.Text = "Street Address";
            ziptxt.Text = "Zip Code";


        }

        private void lnametxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (lnametxt.Text == "Last")
            {
                lnametxt.Text = "";
            }
        }
        private void phonyxy_GotFocus(object sender, RoutedEventArgs e)
        {
            if (phonnum.Text == "(999) 999-999")
            {
                phonnum.Text = "";
            }
        }

        private void fnametxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (fnametxt.Text == "First")
            {
                fnametxt.Text = "";
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtemail.Text == "Enter Your Email Address")
            {
                txtemail.Text = "";
            }
        }

        private void Citytxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (Citytxt.Text == "City")
            {
                Citytxt.Text = "";
            }
        }

        private void apttxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (apttxt.Text == "APT./Suite")
            {
                apttxt.Text = "";
            }
        }

        private void addtxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (addtxt.Text == "Street Address")
            {
                addtxt.Text = "";
            }

        }

        private void ziptxt_GotFocus(object sender, RoutedEventArgs e)
        {
            if (ziptxt.Text == "Zip Code")
            {
                ziptxt.Text = "";
            }

        }
        #endregion


        #endregion


        #region seconed part Search

        private void Button_GotFocus_1(object sender, RoutedEventArgs e)
        {
            context.reservation.Load();

            var result = context.reservation.Where(r => r.first_name.Contains(searchtxt.Text) 
            || r.last_name.Contains(searchtxt.Text) || r.email_address.Contains(searchtxt.Text)
            || r.city.Contains(searchtxt.Text) || r.state.Contains(searchtxt.Text) 
            || r.payment_type.Contains(searchtxt.Text) || r.card_exp.Contains(searchtxt.Text)
            || r.Id.ToString().Contains(searchtxt.Text) || r.arrival_time.ToString().Contains(searchtxt.Text)
            ||r.gender.Contains(searchtxt.Text) || r.number_guest.ToString().Contains(searchtxt.Text)
            ||r.room_type.Contains(searchtxt.Text) || r.room_floor.Contains(searchtxt.Text)).ToList();

            if (searchtxt.Text!="" && result.Count > 0)
            {
                datagrid1.ItemsSource= result;
                datagrid1.Visibility = Visibility.Visible;
                errorimg.Visibility = Visibility.Hidden;

            }
            else 
            {
                datagrid1.Visibility = Visibility.Hidden;
                errorimg.Visibility = Visibility.Visible;
            }



        }


        #endregion


        #region Third part "data grid"
        private void TabItem_GotFocus(object sender, RoutedEventArgs e)
        {
            context.reservation.Load();

            datagrid.ItemsSource = context.reservation.Local.ToBindingList();

            context.SaveChanges();
        }

        #endregion


        #region fourth part

        private void TabControl_GotFocus(object sender, RoutedEventArgs e)
        {
            context.reservation.Load();

            reservedlst.ItemsSource = context.reservation.Where(ch => ch.check_in == false).
                Select(r => new
                {
                    reservation = r.room_number + "|" + r.room_type
                           + "|" + r.first_name + r.last_name + "|" + r.phone_number + "|" + r.arrival_time
                }).ToList();

            reservedlst.DisplayMemberPath = "reservation";

            context.reservation.Load();

            occuplst.ItemsSource = context.reservation.Where(ch => ch.check_in == true)
                .Select(r => new
                {
                    reservation = r.room_number + "|" + r.room_type
                           + "|" + r.first_name + r.last_name + "|" + r.phone_number
                }).ToList();

            occuplst.DisplayMemberPath = "reservation";


        }


        #endregion


        #region Back Button

        private void btn_back(object sender, RoutedEventArgs e)
        {
            Login lg = new Login();
            lg.Show();
            Close();
        }
        #endregion


        #region Submit Button

        private void btn_submit(object sender, RoutedEventArgs e)
        {

            try
            {

                context.reservation.Add(new reservation
                {
                    first_name = fnametxt.Text,
                    last_name = lnametxt.Text,
                    birth_day = DOB.Text,
                    gender = gen.Text,
                    phone_number = phonnum.Text,
                    email_address = txtemail.Text,
                    street_address = addtxt.Text,
                    apt_suite = apttxt.Text,
                    city = Citytxt.Text,
                    zip_code = ziptxt.Text,
                    state = statecombobox.Text,
                    number_guest = int.Parse(combonumb.Text),
                    room_number = roomnum.Text,
                    room_type = roomtype.Text,
                    room_floor = floornum.Text,
                    arrival_time = DateTime.Now,
                    leaving_time = (DateTime)leavdate.SelectedDate,
                    payment_type = finalizePayment.comboBoxPaymentMethod.Text,
                    card_number = finalizePayment.CardNumber.Text,
                    card_type = finalizePayment.CardType.Text,
                    card_cvc = finalizePayment.TboxCVV.Text,
                    card_exp = $"{finalizePayment.comboBoxMM.Text}/{finalizePayment.comboBoxYY.Text}",
                    supply_status = supply.IsChecked.Value,
                    check_in = check.IsChecked.Value,
                    towel = (bool)foodMenu.Towels.IsChecked.Value,
                    cleaning = (bool)foodMenu.clean.IsChecked.Value,
                    s_surprise = (bool)foodMenu.sweet.IsChecked.Value,
                    break_fast = int.Parse(foodMenu.txtbreakfast.Text),
                    lunch = int.Parse(foodMenu.txtlunch.Text),
                    dinner = int.Parse(foodMenu.txtdinner.Text),

                }
                    );

                context.SaveChanges();

                MessageBox.Show("Data Saved Successfully", "Add Data Info");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Some Error Occured", "Add Data Info");

            }
        }

        #endregion


        #region Submit Button Visibility

        //finalize button gotfocus
        private void visible_btnsubmit(object sender, RoutedEventArgs e)
        {
            btnSub.Visibility = Visibility.Visible;
        }

        #endregion


        #region Delete button

        private void del_btn(object sender, RoutedEventArgs e)
        {
            var data = combobox.SelectedValue.ToString();
            var spldata = data.Split("|");
            var splid = spldata[0].Split(" = ");
            int id = int.Parse(splid[1]);
            var res = MessageBox.Show("Are You Sure To Delete Data", "Confirm Delete"); 

            if (res == MessageBoxResult.OK)
            {
                try
                {

                var del_data = context.reservation.FirstOrDefault(r => r.Id == id);
                context.reservation.Remove(del_data);
                context.SaveChanges();

                combobox.ItemsSource = null;

                context.reservation.Load();

                var result1 = context.reservation.Select(r => new { reservation = r.Id + "|" + r.first_name + " " + r.last_name + "|" + r.phone_number }).ToList();

                combobox.ItemsSource = result1;

                combobox.DisplayMemberPath = "reservation";
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Delete Error");
                }
            }
            else
            {
                MessageBox.Show("Data Still in DB", "Canceled Delete");
            }
        }

        #endregion

        #region Update Button

        private void update_btn(object sender, RoutedEventArgs e)
        {
            var data = combobox.SelectedValue.ToString();
            var spldata = data.Split("|");
            var splid = spldata[0].Split(" = ");
            int id = int.Parse(splid[1]);
            var del_data = context.reservation.FirstOrDefault(r => r.Id == id);
            try
            {

            del_data.first_name = fnametxt.Text;
            del_data.last_name = lnametxt.Text;
            del_data.birth_day = DOB.Text;
            del_data.gender = gen.Text;
            del_data.phone_number = phonnum.Text;
            del_data.email_address = txtemail.Text;
            del_data.street_address = addtxt.Text;
            del_data.apt_suite = apttxt.Text;
            del_data.city = Citytxt.Text;
            del_data.zip_code = ziptxt.Text;
            del_data.state = statecombobox.Text;
            del_data.number_guest = int.Parse(combonumb.Text);
            del_data.room_number = roomnum.Text;
            del_data.room_type = roomtype.Text;
            del_data.room_floor = floornum.Text;
            del_data.arrival_time = DateTime.Now;
            del_data.leaving_time = (DateTime)leavdate.SelectedDate;
            del_data.payment_type = finalizePayment.comboBoxPaymentMethod.Text;
            del_data.card_number = finalizePayment.CardNumber.Text;
            del_data.card_type = finalizePayment.CardType.Text;
            del_data.card_cvc = finalizePayment.TboxCVV.Text;
            del_data.card_exp = $"{finalizePayment.comboBoxMM.Text}/{finalizePayment.comboBoxYY.Text}";
            del_data.supply_status = supply.IsChecked.Value;
            del_data.check_in = check.IsChecked.Value;
            del_data.towel = (bool)foodMenu.Towels.IsChecked.Value;
            del_data.cleaning = (bool)foodMenu.clean.IsChecked.Value;
            del_data.s_surprise = (bool)foodMenu.sweet.IsChecked.Value;

            if (foodMenu.chkboxbreakfast.IsChecked.Value)
            {
                del_data.break_fast = int.Parse(foodMenu.txtbreakfast.Text);

            }

            if (foodMenu.chkboxlunch.IsChecked.Value)

            {
                del_data.lunch = int.Parse(foodMenu.txtlunch.Text);
            }

            if (foodMenu.chkboxdinner.IsChecked.Value)
            {
                del_data.dinner = int.Parse(foodMenu.txtdinner.Text);
            }

            
            context.reservation.Update(del_data);
            context.SaveChanges();
            MessageBox.Show("Date Updated Successfully", "Update Info");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Update Error");
            }


        }

        #endregion

        #region New Reservation button
        private void cleardatabtn(object sender, RoutedEventArgs e)
        {
            fnametxt.Text = "First";
            lnametxt.Text = "Last";
            phonnum.Text = "(999) 999-999";
            Citytxt.Text = "City";
            txtemail.Text = "Enter Your Email Address";
            addtxt.Text = "Street Address";
            apttxt.Text = "APT./Suite";
            ziptxt.Text = "Zip Code";
            gen.SelectedItem = "";
            DOB.SelectedDate = DateTime.Now;
            statecombobox.SelectedItem = null;
            combonumb.SelectedItem = null;
            roomtype.SelectedItem = null;
            roomnum.SelectedItem = null;
            floornum.SelectedItem = null;
            entrydate.SelectedDate = DateTime.Now;
            leavdate.SelectedDate = DateTime.Now;
            check.IsChecked = false;
            sms.IsChecked = false;
            supply.IsChecked = false;
        }


        #endregion

     
    }
}
