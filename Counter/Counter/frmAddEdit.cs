﻿using System;
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
    public partial class frmAddEdit : Form
    {
        SqlConnection connection = new SqlConnection();
        int flag;

        public int iMode;

        public frmAddEdit()
        {
            InitializeComponent();
            dtSelectedDate.Format = DateTimePickerFormat.Custom;
            dtSelectedDate.CustomFormat = "yyyy-MM-dd";
            
        }

        private void frmAddEdit_Load(object sender, EventArgs e)
        {
            connection.ConnectionString = config.GetConnectionString();
            ComboBoxFill();
            dtSelectedDate.Hide();
            cbSelectDate.Hide();
            switch (frmCounterValueList.status)
            {
                case "Add":
                    {
                        btnOK.Text = "Add";
                        this.Text = "Add New Value";
                        dtSelectedDate.Show();
                        break;
                    }
                case "Edit":
                    {
                        btnOK.Text = "Edit";
                        this.Text = "Edit Value";
                        cbSelectDate.Show();
                        break;
                    }
                case "Delete":
                    {
                        tbValue.ReadOnly = true;
                        btnOK.Text = "Delete";
                        this.Text = "Delete Value";
                        cbSelectDate.Show();
                        break;
                    }
                default:
                    MessageBox.Show("Error on case");
                    break;
            }
        }

        public void ComboBoxFill()
        {
            flag = 0;
            SqlCommand sc = new SqlCommand("SELECT Name, Code FROM CounterType", connection);
            SqlDataReader reader;
            connection.Open();
            reader = sc.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Code", typeof(int));
            dt.Load(reader);
            cbSelectedCounter.ValueMember = "Code";
            cbSelectedCounter.DisplayMember = "Name";
            cbSelectedCounter.DataSource = dt;
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
            dt.Columns.Add("Дата", typeof(DateTime));
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

        public void InsertValue(string SelectedCounter, string SelectedDate, string SelectedValue)
        {
            connection.Open();
            string InsertString = String.Format("INSERT INTO CounterValue (Code,DT,Value) VALUES ('{0}','{1}','{2}')", SelectedCounter, SelectedDate, SelectedValue);
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

        public void UpdateValue(string SelectedCounter, string SelectedDate, string SelectedValue)
        {
            connection.Open();
            string UpdateString = String.Format("UPDATE CounterValue SET value = '{0}' WHERE Code='{1}' AND DT='{2}'", SelectedValue, SelectedCounter, SelectedDate);
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

        public void DeleteValue(string SelectedCounter, string SelectedDate, string SelectedValue)
        {
            connection.Open();

            SelectedCounter = SelectedCounter.TrimEnd();

            string DeleteString = String.Format("DELETE FROM CounterValue WHERE Value = '{0}' AND Code='{1}' AND DT='{2}'", SelectedValue, SelectedCounter, SelectedDate);
            try
            {
                SqlCommand cmdUpdate = new SqlCommand(DeleteString, connection);
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

        private void btnOK_Click(object sender, EventArgs e)
        {
            switch (frmCounterValueList.status)
            {
                case "Add":
                    {
                        string sSelectedCounter = cbSelectedCounter.GetItemText(cbSelectedCounter.SelectedValue);
                        string sSelectedDate = dtSelectedDate.Value.Date.ToString("yyyy-MM-dd");
                        string sValue = tbValue.Text;
                        int userVal;
                        if ((int.TryParse(tbValue.Text, out userVal)) && (userVal >= 0))
                        {
                            InsertValue(sSelectedCounter, sSelectedDate, sValue);
                            MessageBox.Show("Новые показания внесены");
                            this.Close();                       
                        }
                        else
                        {
                            MessageBox.Show("Введены неверные показания");
                        }
                        break;
                    }
                case "Edit":
                    {
                        string sSelectedCounter = cbSelectedCounter.GetItemText(cbSelectedCounter.SelectedValue);
                        string sSelectedDate = cbSelectDate.GetItemText(cbSelectDate.SelectedValue);
                        string sValue = tbValue.Text;
                        int userVal;
                        if ((int.TryParse(tbValue.Text, out userVal)) && (userVal >= 0))
                        {
                            UpdateValue(sSelectedCounter, sSelectedDate, sValue);
                            MessageBox.Show("Новые показания внесены");
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Введены неверные показания");
                        }
                        
                        break;
                    }
                case "Delete":
                    {
                        string sSelectedCounter = cbSelectedCounter.GetItemText(cbSelectedCounter.SelectedValue);
                        string sSelectedDate = cbSelectDate.GetItemText(cbSelectDate.SelectedValue);
                        string sValue = tbValue.Text;
                        {
                            DeleteValue(sSelectedCounter, sSelectedDate, sValue);
                            MessageBox.Show("Value deleted");
                            this.Close();
                        }
                        tbValue.ReadOnly = true;
                        break;
                    }
                default:
                    MessageBox.Show("Error on case");
                    break;
            }
        }

        private void cbSelectedCounter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((frmCounterValueList.status == "Edit")||frmCounterValueList.status == "Delete")
            {
                string SelectedCounterCode = cbSelectedCounter.GetItemText(cbSelectedCounter.SelectedValue);
                if (flag == 1)
                {
                    ComboBoxFillDate(SelectedCounterCode);
                }
            }
        }

        private void cbSelectDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbValue.Text = "";
            if ((frmCounterValueList.status == "Edit") || (frmCounterValueList.status == "Delete"))
            {
                string SelectedCounter = cbSelectedCounter.GetItemText(cbSelectedCounter.SelectedValue);
                string SelectedDate = cbSelectDate.GetItemText(cbSelectDate.SelectedValue);
                if (flag == 1)
                {
                    txtBoxValueFill(SelectedCounter, SelectedDate);
                }
            }
        }
        
    }
}
