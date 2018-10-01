using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace SQLExample
{
    public partial class CustomersForm : Form
    {
        private List<Customers> CustomerList = new List<Customers>();

        public CustomersForm()
        { 
            InitializeComponent();
            LoadCustomersFromTable();
            PopulateCustomerLstBox();
        }

        private SqlConnection OpenDatabase()
        {
            SqlConnection connection = new SqlConnection(); //Create a new SQL connection object
            connection.ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Arbitrary; Integrated Security=True; MultipleActiveResultSets=True; AttachDbFilename=|DataDirectory|\SQLExample.mdf;";
            return connection;
        }

        private void LoadCustomersFromTable()
        {         
            try
            {               
                SqlConnection con = OpenDatabase();
                con.Open();
                SqlCommand cmd = new SqlCommand("select * from Customers", con);
                SqlDataReader readCustomerFile = cmd.ExecuteReader();
                CustomerList.Clear();
                while (readCustomerFile.Read())
                {
                    Customers customer = new Customers(Convert.ToString(readCustomerFile["Forename"]), Convert.ToString(readCustomerFile["Surname"]));
                    CustomerList.Add(customer);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }
        }

        private void SaveCustomerToTable()
        {
            try
            {
                SqlConnection con = OpenDatabase();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "INSERT Customers (Forename, Surname) VALUES (@forename, @surname)";
                cmd.Parameters.AddWithValue("@forename", forenameTxt.Text);
                cmd.Parameters.AddWithValue("@surname", surnameTxt.Text);
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                LoadCustomersFromTable();
                PopulateCustomerLstBox();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Can not open connection ! " + ex);
            }
        }

        private void PopulateCustomerLstBox()
        {
            customersLstBox.Items.Clear();
            for (int i = 0; i < CustomerList.Count; i++)
            {
                customersLstBox.Items.Add(CustomerList[i].Forename + " " + CustomerList[i].Surname);
            }
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            SaveCustomerToTable();
        }
    }
}
