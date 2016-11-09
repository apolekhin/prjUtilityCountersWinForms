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
    public partial class Form4 : Form
    {
            SqlConnection connection = new SqlConnection();
            SqlCommand comand = new SqlCommand();
            DataTable dTab1 = new DataTable();
            SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter();
            int flag;//флаг на первоначальное заполнение отчета
            string CommandText1;// Содержит текст запроса. Модифицируется условиями фильтра
            const string sQueryTemplate = "SELECT Дата, Счетчик, Показания FROM vwCounterValue WHERE {0} AND {1} AND {2} ORDER BY Дата DESC";


        public void ComboBoxFill()
        {
            //проба ридера
                SqlCommand sc = new SqlCommand("SELECT DISTINCT Счетчик, Код FROM vwCounterValue", connection);
                SqlDataReader reader;
                connection.Open();
                reader = sc.ExecuteReader();
                
            DataTable dt = new DataTable();
            dt.Columns.Add("Счетчик", typeof(string));
            dt.Columns.Add("Код", typeof(int));
            //добавление кастомного счетчика ВСЕ
            DataRow _All = dt.NewRow();
            _All["Счетчик"] = "Все";
            _All["Код"] = "0";
            dt.Rows.Add(_All);//Конец добавления кастомного
            dt.Load(reader);
            cbSelectCounter.ValueMember = "Код";
            cbSelectCounter.DisplayMember = "Счетчик";
            cbSelectCounter.DataSource = dt;
            
            /*
            DataTable dTab = new DataTable();
            SqlDataAdapter sqlDTAdapter = new SqlDataAdapter(); 
            sqlDTAdapter.SelectCommand = new SqlCommand("SELECT Name FROM CounterType", connection);
            sqlDTAdapter.Fill(dTab);
            cbSelectCounter.DataSource = dTab;
            cbSelectCounter.DisplayMember = dTab.Columns["Name"].ColumnName;
            cbSelectCounter.ValueMember = dTab.Columns["Name"].ColumnName;*/
            connection.Close();
            //cbSelectCounter.Items.Add("Все");
        }//заполнение комбобокса типами счетчиков

        public void Query(string cmdText)
            {
                dTab1.Clear();
                dataGridView1.DataSource = null;
                dataGridView1.Rows.Clear();
                dataGridView1.Refresh();
                
                connection.Open();
                if (flag == 0 && chkBoxFilter.Checked == true)
                {
                    string dateFrom = dtFrom.Value.Date.ToString("yyyy-MM-dd");
                    string dateTo = dtTo.Value.Date.ToString("yyyy-MM-dd");
                    string SelectedCounter;
                    string AllCounters = "Код='";
                    //проверка счетчика ВСЕ
                    SelectedCounter = cbSelectCounter.GetItemText(cbSelectCounter.SelectedValue);
                    if (SelectedCounter.CompareTo("0") == 0) 
                    {
                        AllCounters = "'1'=";
                        SelectedCounter = "'1";
                    }
                    //Конец проверки
                    cmdText = String.Format(sQueryTemplate, AllCounters + SelectedCounter + "'", "Дата >= '" + dateFrom + "'", "Дата <='" + dateTo +"'");
                
                }
                else if (flag == 1 && chkBoxFilter.Checked == false)
                {
                    cmdText = String.Format(sQueryTemplate, "1=1", "1=1", "1=1");
                }
                sqlDataAdapter1.SelectCommand = new SqlCommand(cmdText, connection);
                sqlDataAdapter1.Fill(dTab1);
                this.dataGridView1.DataSource = dTab1;
                connection.Close();
            }
        
        public Form4()
        {
            InitializeComponent();
            connection.ConnectionString = config.GetConnectionString(); 
        }

        private void Form4_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Hide();
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            
            flag = 1;
            CommandText1 = String.Format(sQueryTemplate, "1=1", "1=1", "1=1");
            Query(CommandText1);
            ComboBoxFill(); 
            flag = 0;
            
        }

        

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Query(CommandText1);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxFilter.Checked == true)
            {
                
                label4.Text = "Фильтр Вкл";
                Query(CommandText1);
            }
            else if (chkBoxFilter.Checked == false)
            {
                label4.Text = "Фильтр Выкл";
                Query(CommandText1);
            }
                    
        }

        private void cbSelectCounter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                Query(CommandText1);
                chkBoxFilter.Checked = true;
            }
            
        }

        private void dtTo_ValueChanged(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                Query(CommandText1);
                chkBoxFilter.Checked = true;
            }
        }

        private void dtFrom_ValueChanged(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                Query(CommandText1);
                chkBoxFilter.Checked = true;
            }
        }

        
    }
}
