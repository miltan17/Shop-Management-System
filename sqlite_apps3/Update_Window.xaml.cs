using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace sqlite_apps3
{
    /// <summary>
    /// Interaction logic for Update_Window.xaml
    /// </summary>
    public partial class Update_Window : Window
    {
        //declaring variables
        string selecte_Model_From_The_Gridview;
        string dbConnectionStrig = @"Data Source= test.sqlite;Version = 3;";

        //open update window from here
        public Update_Window()
        {
            
            InitializeComponent();
            //load data to the datagrid
            loadData();
            //Hide all labels and textbox of this form
            Hide_All();

        }

        //method for hiding all labels and textbox.
        private void Hide_All()
        {
            //make label hidden
            Label_Amount.Visibility = System.Windows.Visibility.Hidden;
            Label_Catagory.Visibility = System.Windows.Visibility.Hidden;
            Label_Description.Visibility = System.Windows.Visibility.Hidden;
            Label_Model.Visibility = System.Windows.Visibility.Hidden;
            Label_Price.Visibility = System.Windows.Visibility.Hidden;

            //make textbox hidden
            txt_catagory.Visibility = System.Windows.Visibility.Hidden;
            txt_description.Visibility = System.Windows.Visibility.Hidden;
            txt_model.Visibility = System.Windows.Visibility.Hidden;
            txt_pieces.Visibility = System.Windows.Visibility.Hidden;
            txt_prices.Visibility = System.Windows.Visibility.Hidden;

            //make button hidden
            Btn_Update.Visibility = System.Windows.Visibility.Hidden;
        }

        //Load data in the datagrid from the database
        private void loadData()
        {
            //connect sqlite
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionStrig);
            try
            {
                //open connection 
                sqliteCon.Open();

                //do operation in here
                string qry = "select model, price, arrival_date from Products";
                SQLiteCommand cmnd = new SQLiteCommand(qry, sqliteCon);
                cmnd.ExecuteNonQuery();

                //SQLiteDataReader dreader = cmnd.ExecuteReader();
                SQLiteDataAdapter sqliteda = new SQLiteDataAdapter(cmnd);
                DataTable dt = new DataTable("Products");
                sqliteda.Fill(dt);
                Item_grid.ItemsSource = dt.DefaultView;
                sqliteda.Update(dt);

                sqliteCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //do this operatin when double click on a row of the datagrid
        private void Item_grid_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            //connect sqlite
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionStrig);

            try
            {
                //make all the labels and textbox and button visible
                show_All();
                // return if there's no row selected
                if (Item_grid.SelectedItem == null)
                    return;
                //else do the following 

                //create datarowview for the selected row
                DataRowView dr = Item_grid.SelectedItem as DataRowView;
                //create datarow to find the value of all position from the selected row.
                DataRow dr1 = dr.Row;

                //model name of the selected row
                selecte_Model_From_The_Gridview = Convert.ToString(dr1.ItemArray[0]);
                //open connection
                sqliteCon.Open();

                //sql query to select model, description ... to show them in the updatable textbox
                string qry = "select model,description , catagory, amount, price from Products where model = '"+Convert.ToString(dr1.ItemArray[0])+"' ";
                SQLiteCommand cmnd = new SQLiteCommand(qry, sqliteCon);
                cmnd.ExecuteNonQuery();

                //create datareader
                SQLiteDataReader dreader = cmnd.ExecuteReader();

                //move selected item to the text box from the datareader
                while (dreader.Read())
                {
                    txt_model.Text = dreader["model"].ToString() ;
                    txt_description.Text = dreader["description"].ToString();
                    txt_catagory.Text = dreader["catagory"].ToString();
                    txt_pieces.Text = dreader["amount"].ToString();
                    txt_prices.Text = dreader["price"].ToString();
                }

                //close connection
                sqliteCon.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //maske all the labels, textbox and button visible
        private void show_All()
        {
            //make labels visible
            Label_Amount.Visibility = System.Windows.Visibility.Visible;
            Label_Catagory.Visibility = System.Windows.Visibility.Visible;
            Label_Description.Visibility = System.Windows.Visibility.Visible;
            Label_Model.Visibility = System.Windows.Visibility.Visible;
            Label_Price.Visibility = System.Windows.Visibility.Visible;

            //make textbox visible
            txt_catagory.Visibility = System.Windows.Visibility.Visible;
            txt_description.Visibility = System.Windows.Visibility.Visible;
            txt_model.Visibility = System.Windows.Visibility.Visible;
            txt_pieces.Visibility = System.Windows.Visibility.Visible;
            txt_prices.Visibility = System.Windows.Visibility.Visible;

            //make button visible
            Btn_Update.Visibility = System.Windows.Visibility.Visible;
        }

        //work for update button
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //connect sqlite
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionStrig);
            try
            {
                //open connection
                sqliteCon.Open();

                string description = txt_description.Text;
                //do update operetion
                string query = "update products set model = '" + txt_model.Text + "' ,description ='" + description + "', catagory ='" + txt_catagory.Text + "', amount='" + Convert.ToInt32(txt_pieces.Text) + "', price = '" +Convert.ToInt32(txt_prices.Text) + "' where model = '" + selecte_Model_From_The_Gridview + "'";

                //create command according to this query
                SQLiteCommand cmnd = new SQLiteCommand(query, sqliteCon);
                //execute this command
                cmnd.ExecuteNonQuery();

                MessageBox.Show("Successfully Updated.");

                //Close connection
                sqliteCon.Close();
                //load data again in the datagrid
                loadData();
                //hide labels, textbox and button again
                Hide_All();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}
