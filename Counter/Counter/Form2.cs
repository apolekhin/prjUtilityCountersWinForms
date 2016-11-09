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
namespace Counter
{
    public partial class Form2 : Form
    {
        SqlConnection connection = new SqlConnection();
        
        public Form2()
        {
            InitializeComponent();
            dtPick.Format = DateTimePickerFormat.Custom;
            dtPick.CustomFormat = "yyyy-MM-dd";
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            connection.ConnectionString = config.GetConnectionString();
            ComboBoxFill();
            
        }

        public void ComboBoxFill()
        {
            //проба ридера
            SqlCommand sc = new SqlCommand("SELECT Name, Code FROM CounterType", connection);
            SqlDataReader reader;
            connection.Open();
            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Code", typeof(int));
            dt.Load(reader);
            cbSelectCounter.ValueMember = "Code";
            cbSelectCounter.DisplayMember = "Name";
            cbSelectCounter.DataSource = dt;
            connection.Close();
            
        }

        

        public void InsertValue(string SelectedCounter, string SelectedDate, string SelectedValue)
        {
            connection.Open();
            string InsertString = String.Format("INSERT INTO CounterValue (Code,DT,Value) VALUES ('{0}','{1}','{2}')",SelectedCounter, SelectedDate, SelectedValue);
            try
            {
                SqlCommand cmdInsert = new SqlCommand(InsertString, connection);
                cmdInsert.ExecuteNonQuery();
                cmdInsert.Dispose();
                cmdInsert = null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString(), ex);
            }
            finally
            {
                connection.Close();
            }
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            string SelectedCounter = cbSelectCounter.GetItemText(cbSelectCounter.SelectedValue);
            string Date = dtPick.Value.Date.ToString("yyyy-MM-dd");
            string Value = tbValue.Text;
            int userVal;
            if ((int.TryParse(tbValue.Text, out userVal)) && (userVal>=0))
                    {
                        InsertValue(SelectedCounter, Date, Value);
                        MessageBox.Show("Новые показания внесены");
                    }
                    else
                    { 
                        MessageBox.Show("Введены неверные показания");   } 
                        
                    }
    }
}
