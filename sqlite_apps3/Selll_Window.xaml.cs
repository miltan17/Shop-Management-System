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
    /// Interaction logic for Selll_Window.xaml
    /// </summary>
    public partial class Selll_Window : Window
    {
        string dbConnectionStrig = @"Data Source= test.sqlite;Version = 3;";
        public Selll_Window()
        {
            InitializeComponent();

            //Load Brand to the Brand Combobox
            Load_Brand();
        }

        //select and get all catagory to the brand combobox
        private void Load_Brand()
        {
            this.Combobox_Brand.Items.Clear();
            //connect sqlite
            SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionStrig);

            try
            {
                //do the following to select your desired item

                //open connection
                sqliteCon.Open();

                //sql query to select model, description ... to show them in the updatable textbox
                string qry = "select distinct catagory from Products";
                SQLiteCommand cmnd = new SQLiteCommand(qry, sqliteCon);
                cmnd.ExecuteNonQuery();

                //create datareader
                SQLiteDataReader dreader = cmnd.ExecuteReader();

                //move selected item to the text box from the datareader
                while (dreader.Read())
                {
                      Combobox_Brand.Items.Add( dreader["catagory"].ToString());      
                }

                //close connection
                sqliteCon.Close();

                //visible and hide elements
                VisibleAndHide("Initial");

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        //dropdown closed event for brand combobox
        private void Combobox_Brand_DropDownClosed_1(object sender, EventArgs e)
        {
            //if no brand has selected 
            if (Combobox_Brand.SelectedValue == null)
            {
                Label_brandError.Content = "Please select any Brand";
            }
            // if brand is selected
            else
            {
                //set selected brand name to the brandName string variable
                string brandName = Combobox_Brand.SelectedValue.ToString();

                //make brand error label visibility off
                Label_brandError.Content = null;

                //connect sqlite
                SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionStrig);

                try
                {
                    //do the following to select your desired item

                    //make model combobox empty
                    this.Combobox_model.Items.Clear();
                    this.Combobox_model.SelectedValue = null;
                    //make amount combobox empty
                    this.Combobox_amount.Items.Clear();
                    this.Combobox_amount.SelectedValue = null;

                    //open connection
                    sqliteCon.Open();

                    //sql query to select model, description ... to show them in the updatable textbox
                    string qry = "select model from Products where catagory == '" + brandName + "'";
                    SQLiteCommand cmnd = new SQLiteCommand(qry, sqliteCon);
                    cmnd.ExecuteNonQuery();

                    //create datareader
                    SQLiteDataReader dreader = cmnd.ExecuteReader();

                    //move selected item to the text box from the datareader
                    while (dreader.Read())
                    {
                        //insert model to model combobox
                        Combobox_model.Items.Add(dreader["model"].ToString());
                    }
                    //close connection
                    sqliteCon.Close();

                    //visible and hide element
                    VisibleAndHide("Brand");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //dropdown closed event for model combobox
        private void Combobox_model_DropDownClosed_1(object sender, EventArgs e)
        {

            //if no model is selected
            if (Combobox_model.SelectedValue == null)
            {
                Label_modelError.Content = "Please select any Model";
            }
            //if any model is selected then do this
            else
            {
                Label_modelError.Content = null;
                //set selected model name to the brandName string variable
                string modelName = Combobox_model.SelectedValue.ToString();

                //connect sqlite
                SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionStrig);

                try
                {
                    //do the following to select your desired item

                    //open connection
                    sqliteCon.Open();

                    //sql query to select model, description ... to show them in the updatable textbox
                    string qry = "select amount from Products where model == '" + modelName + "'";
                    SQLiteCommand cmnd = new SQLiteCommand(qry, sqliteCon);
                    cmnd.ExecuteNonQuery();

                    //create datareader
                    SQLiteDataReader dreader = cmnd.ExecuteReader();

                    //move selected item to the text box from the datareader
                    while (dreader.Read())
                    {
                        //insert model to model combobox

                        int amount = Convert.ToInt32(dreader["amount"]);

                        //add 1 to amount in the amount combobox
                        for (int i = 1; i <= amount; i++)
                        {
                            Combobox_amount.Items.Add(i);
                        }
                    }

                    //close connection
                    sqliteCon.Close();

                    //visible and hide elements
                    VisibleAndHide("Model");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //dropdown closed event for amount combobox
        private void Combobox_amount_DropDownClosed_1(object sender, EventArgs e)
        {
            
            //if amount of product is selected
            if (Combobox_amount.SelectedValue == null)
            {
                Label_amountError.Content = "Please select amount of products";
            }
            else
            {
                Label_amountError.Content = null;
                //set selected brand name and model name to the brandName and modelName string variable
                string brandName = Combobox_Brand.SelectedValue.ToString();
                string modelName = Combobox_model.SelectedValue.ToString();


                //connect sqlite
                SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionStrig);

                try
                {
                    //do the following to select your desired item

                    //open connection
                    sqliteCon.Open();

                    //sql query to select model, description ... to show them in the updatable textbox
                    string qry = "select price from Products where model == '" + modelName + "' and catagory == '" + brandName + "'";
                    SQLiteCommand cmnd = new SQLiteCommand(qry, sqliteCon);
                    cmnd.ExecuteNonQuery();

                    //create datareader
                    SQLiteDataReader dreader = cmnd.ExecuteReader();

                    //move selected item to the text box from the datareader
                    while (dreader.Read())
                    {
                        //insert price to the label_price
                        Label_price.Content = "Total Price: " + (Convert.ToInt32(dreader["price"])*Convert.ToInt32(Combobox_amount.SelectedValue)).ToString() + "/-";

                    }
                    //close connection
                    sqliteCon.Close();

                    //visible and hide elements
                    VisibleAndHide("Amount");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //method to visible and hide elemets
        private void VisibleAndHide(string p)
        {
            switch (p)
            {
                case "Initial":
                    //make all elements hide without combobox for brand

                    Label_SelectModel.Visibility = System.Windows.Visibility.Hidden;
                    Combobox_model.Visibility = System.Windows.Visibility.Hidden;

                    Label_SelectAmount.Visibility = System.Windows.Visibility.Hidden;
                    Combobox_amount.Visibility = System.Windows.Visibility.Hidden;

                    Label_price.Visibility = System.Windows.Visibility.Hidden;

                    Button_sell.Visibility = System.Windows.Visibility.Hidden;
                    break;

                case "Brand":
                    Label_SelectModel.Visibility = System.Windows.Visibility.Visible;
                    Combobox_model.Visibility = System.Windows.Visibility.Visible;

                    Label_SelectAmount.Visibility = System.Windows.Visibility.Hidden;
                    Combobox_amount.Visibility = System.Windows.Visibility.Hidden;

                    Label_price.Visibility = System.Windows.Visibility.Hidden;

                    Button_sell.Visibility = System.Windows.Visibility.Hidden;
                    break;

                case "Model":
                    Label_SelectAmount.Visibility = System.Windows.Visibility.Visible;
                    Combobox_amount.Visibility = System.Windows.Visibility.Visible;

                    Label_price.Visibility = System.Windows.Visibility.Hidden;

                    Button_sell.Visibility = System.Windows.Visibility.Hidden;
                    break;

                case "Amount":
                    Label_price.Visibility = System.Windows.Visibility.Visible;

                    Button_sell.Visibility = System.Windows.Visibility.Visible;
                    break;

            }
        }

        //final ok button to sell item
        private void Button_Sell_Click_1(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
               "Do you really want to sell this item?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
               //connect sqlite
                SQLiteConnection sqliteCon = new SQLiteConnection(dbConnectionStrig);

                try
                {
                    
                    string brandName = Combobox_Brand.SelectedValue.ToString();
                    string modelName = Combobox_model.SelectedValue.ToString();
                    //open connection
                    sqliteCon.Open();

                    //sql query to select model, description ... to show them in the updatable textbox
                    string qry = "select amount from Products where catagory == '" + brandName + "' and model == '" + modelName + "'";
                    SQLiteCommand cmnd = new SQLiteCommand(qry, sqliteCon);
                    cmnd.ExecuteNonQuery();

                    //create datareader
                    SQLiteDataReader dreader = cmnd.ExecuteReader();

                    int amount = 0;
                    //move selected item to the text box from the datareader
                    while (dreader.Read())
                    {
                        //insert model to model combobox
                        amount = Convert.ToInt32(dreader["amount"]);
                    }

                    int After_Sellint_Amount = Convert.ToInt32(amount - Convert.ToInt32(Combobox_amount.SelectedValue));

                    //if amount of this product after selling is zero then delete this item form the database
                    if (After_Sellint_Amount == 0)
                    {
                        qry = "delete  from Products where catagory == '" + brandName + "' and model == '" + modelName + "'";
                        cmnd = new SQLiteCommand(qry, sqliteCon);
                        cmnd.ExecuteNonQuery();
                    }
                    //if amount of this product after selling is not zero then update this item in the database
                    else
                    {
                        qry = "update products set amount = '" + After_Sellint_Amount + "' where catagory == '" + brandName + "' and model == '" + modelName + "'";
                        cmnd = new SQLiteCommand(qry, sqliteCon);
                        cmnd.ExecuteNonQuery();
                    }

                    //close connection
                    sqliteCon.Close();

                    Load_Brand();

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

    }
}
