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
    public partial class Form3 : Form
    {
        SqlConnection connection = new SqlConnection();
        int flag;
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        public void ComboBoxFill()
        {
            flag = 0;
            SqlCommand sc = new SqlCommand("SELECT DISTINCT Name, Code FROM CounterType", connection);
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
            flag = 1;
        }

        public void ComboBoxFillDate(string SelectedCounterCode)
        {
            flag = 0;
            string sqlCmd = String.Format("SELECT Дата FROM vwCounterValue WHERE Код ='{0}'", SelectedCounterCode);
            SqlCommand sc = new SqlCommand(sqlCmd, connection);
            SqlDataReader reader;
            connection.Open();
            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Дата",typeof(DateTime));
            dt.Load(reader);
            cbSelectDate.ValueMember = "Дата";
            cbSelectDate.DisplayMember = "Дата";
            cbSelectDate.DataSource = dt;
            connection.Close();
            flag = 1;
        }

        public void txtBoxValueFill(string SelectedCounterCode, string SelectedDate)
        {
            flag = 0;
            string sqlCmd = String.Format("SELECT Показания FROM vwCounterValue WHERE Код ='{0}' AND Дата ='{1}'", SelectedCounterCode, SelectedDate);
            SqlCommand sc = new SqlCommand(sqlCmd, connection);
            SqlDataReader reader;
            connection.Open();
            reader = sc.ExecuteReader();
            while (reader.Read())
            {
                tbValue.Text = reader["Показания"].ToString();
            }
            reader.Close();
            connection.Close();
            flag = 1;
        }

        public void UpdateValue(string SelectedCounter, string SelectedDate, string SelectedValue)
        {
            connection.Open();
            string UpdateString = String.Format("UPDATE CounterValue SET value = '{0}' WHERE Code='{1}' AND DT='{2}'", SelectedValue,SelectedCounter, SelectedDate);
            try
            {
                SqlCommand cmdUpdate = new SqlCommand(UpdateString, connection);
                cmdUpdate.ExecuteNonQuery();
                cmdUpdate.Dispose();
                cmdUpdate = null;
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
            string sSelectedCounter = cbSelectCounter.GetItemText(cbSelectCounter.SelectedValue);
            string sSelectedDate = cbSelectDate.GetItemText(cbSelectDate.SelectedValue);
            string sValue = tbValue.Text;
            int userVal;
            if ((int.TryParse(tbValue.Text, out userVal)) && (userVal >= 0))
            {
                UpdateValue(sSelectedCounter, sSelectedDate, sValue);
                MessageBox.Show("Новые показания внесены");
            }
            else
            {
                MessageBox.Show("Введены неверные показания");
            }
                        
         }

        private void Form3_Load(object sender, EventArgs e)
        {
            flag = 0;
            connection.ConnectionString = config.GetConnectionString();
            ComboBoxFill();
        }

        private void cbSelectCounter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelectedCounterCode = cbSelectCounter.GetItemText(cbSelectCounter.SelectedValue);
            if (flag == 1)
            {
                ComboBoxFillDate(SelectedCounterCode);
            }
        }

        private void cbSelectDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            string SelectedCounter = cbSelectCounter.GetItemText(cbSelectCounter.SelectedValue);
            string SelectedDate = cbSelectDate.GetItemText(cbSelectDate.SelectedValue);
            if (flag == 1)
            {
                txtBoxValueFill(SelectedCounter,SelectedDate);
            } 
            
        }
    }
}
