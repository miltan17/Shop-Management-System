using System;
using System.Collections.Generic;
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
    /// Interaction logic for Add_Window.xaml
    /// </summary>
    public partial class Add_Window : Window
    {

        string dbConnectionStrig = @"Data Source= test.sqlite;Version = 3;";
        public Add_Window()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //connect sqlite
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionStrig);
            try
            {
                //open connection 
                sqliteCon.Open();

                //do operation in here

                //query to insert data
                string qry = "insert into products (model,description,catagory,amount,price) values ('"+this.txt_model.Text+"','"+this.txt_description.Text+"','"+this.txt_brand.Text+"','"+Convert.ToInt32(this.txt_pieces.Text)+"','"+Convert.ToInt32(this.txt_price.Text)+"')";
                SQLiteCommand cmnd = new SQLiteCommand(qry, sqliteCon);
                cmnd.ExecuteNonQuery();

                MessageBox.Show("Successfully saved.");
                
                //close connection
                sqliteCon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            this.Close();
        }

        
    }
}
