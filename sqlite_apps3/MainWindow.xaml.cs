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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SQLite;
using System.Data;

namespace sqlite_apps3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string dbConnectionStrig = @"Data Source= test.sqlite;Version = 3;";
        public MainWindow()
        {
            InitializeComponent();
            Loading_Data();
        }

        private void Loading_Data()
        {
            //connect sqlite
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionStrig);
            try
            {
                //open connection 
                sqliteCon.Open();

                //do operation in here
                string qry = "select catagory as 'Brand', model, price, arrival_date as 'Arrived On'  from Products";
                SQLiteCommand cmnd = new SQLiteCommand(qry, sqliteCon);
                cmnd.ExecuteNonQuery();

                //SQLiteDataReader dreader = cmnd.ExecuteReader();
                SQLiteDataAdapter sqliteda = new SQLiteDataAdapter(cmnd);
                DataTable dt = new DataTable("Products");
                sqliteda.Fill(dt);
                Product_Grid.ItemsSource = dt.DefaultView;
                sqliteda.Update(dt);

                //while (dreader.Read())
                //{

                //}
                //close connection
                sqliteCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //open a new window for adding an item
            Add_Window add = new Add_Window();
            add.Show();
        }

        private void MenuItem_Update(object sender, RoutedEventArgs e)
        {
            //opening update window
            Update_Window update = new Update_Window();
            update.Show();
        }

        private void MenuItem_Exit(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing_1(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
               "Are you sure you want to quit?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No)
            {
                e.Cancel = true;
            }
        }

        private void MenuItem_Sell(object sender, RoutedEventArgs e)
        {
            Selll_Window sale = new Selll_Window();
            sale.Show();
        }


        private void MenuItem_Load(object sender, RoutedEventArgs e)
        {
            //make search tolbox hidden
            Textbox_Search.Visibility = System.Windows.Visibility.Hidden;
            Label_searchError.Visibility = System.Windows.Visibility.Hidden;
            Label_Search.Visibility = System.Windows.Visibility.Hidden;
            Radiobutton_all.Visibility = System.Windows.Visibility.Hidden;
            Radiobutton_brand.Visibility = System.Windows.Visibility.Hidden;
            Radiobutton_model.Visibility = System.Windows.Visibility.Hidden;
            Label_Noitem.Visibility = System.Windows.Visibility.Hidden;
            //and load data
            Loading_Data();
        }
        //button for searching
        private void MenuItem_Search(object sender, RoutedEventArgs e)
        {
            //make search toolbox visible and then do work
            Textbox_Search.Visibility = System.Windows.Visibility.Visible;
            Label_searchError.Visibility = System.Windows.Visibility.Visible;
            Label_Search.Visibility = System.Windows.Visibility.Visible;
            Radiobutton_all.Visibility = System.Windows.Visibility.Visible;
            Radiobutton_brand.Visibility = System.Windows.Visibility.Visible;
            Radiobutton_model.Visibility = System.Windows.Visibility.Visible;
            Label_Noitem.Visibility = System.Windows.Visibility.Visible;
        }

        //event to search in both catagory or model
        private void RadioButton_All(object sender, RoutedEventArgs e)
        {
            string str = Textbox_Search.Text.ToString();
            if (str == "")
            {
                Label_searchError.Content = "Please give any input";
                
            }
            else
            {
                Label_searchError.Content = "";
                //connect sqlite
                SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionStrig);
                try
                {
                    string qry;
                    //open connection 
                    sqliteCon.Open();

                    //do operation in here
                    if (Radiobutton_all.IsChecked == true)
                    {
                        qry = "select catagory as 'Brand', model, price, arrival_date as 'Arrived On'  from Products where catagory = '" + Textbox_Search.Text.ToString() + "' or model ='" + Textbox_Search.Text + "' ";
                    }
                    else if(Radiobutton_brand.IsChecked == true)
                    {
                        qry = "select catagory as 'Brand', model, price, arrival_date as 'Arrived On'  from Products where catagory = '" + Textbox_Search.Text.ToString() + "' ";
                    
                    }
                    else
                        qry = "select catagory as 'Brand', model, price, arrival_date as 'Arrived On'  from Products where model ='" + Textbox_Search.Text + "' ";
                    
                    SQLiteCommand cmnd = new SQLiteCommand(qry, sqliteCon);
                    cmnd.ExecuteNonQuery();

                    //SQLiteDataReader dreader = cmnd.ExecuteReader();
                    SQLiteDataAdapter sqliteda = new SQLiteDataAdapter(cmnd);
                    DataTable dt = new DataTable("Products");
                    sqliteda.Fill(dt);
                    Product_Grid.ItemsSource = dt.DefaultView;
                    sqliteda.Update(dt);
                    //close connection
                    sqliteCon.Close();

                    //if no product is found then do this
                    if (Product_Grid.Items.Count == 1)
                    {
                        Product_Grid.Visibility = System.Windows.Visibility.Hidden;
                        Label_Noitem.Content = "Sorry, No product found.";
                    }
                    //else do this
                    else
                    {
                        Product_Grid.Visibility = System.Windows.Visibility.Visible;
                        Label_Noitem.Content = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void MenuItem_About(object sender, RoutedEventArgs e)
        {
            //System.Diagnostics.Process.Start("notepad.exe","about.txt");
            var process = new System.Diagnostics.Process();
            process.StartInfo = new System.Diagnostics.ProcessStartInfo()
            {
                UseShellExecute = true,
                FileName = "about.txt"
            };

            process.Start();
            process.WaitForExit();
        }

        private void MenuItem_Help(object sender, RoutedEventArgs e)
        {
            Help hlp = new Help();
            hlp.Show();
            
        }

    }
}
