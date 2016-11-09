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
    public partial class frmCounterValueDetail : Form
    {
        //подключение
        SqlConnection connection = new SqlConnection();
        SqlCommand sc = new SqlCommand();
        SqlDataReader reader;
        //для передачи из вызывающей формы
        int iMode;
        // ID записи для загрузки и управления формой
        string strID;
        //введенное пользователем показание счетчика
        int intValue;

        public frmCounterValueDetail(int Mode, string ID = "1")
        {
            InitializeComponent();
            iMode = Mode;
            strID = ID;
            //создаем строку подключения
            connection.ConnectionString = config.GetConnectionString();
            sc.Connection = connection;
        }
        /// <summary>
        /// При загрузке приводим форму в визуальное соответствие выбранному режиму
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCounterValueDetail_Load(object sender, EventArgs e)
        {
            // Выясняем режим вызова формы
            try
            {
                switch (iMode)
                {
                    case (int)Utilities.status.Add: // ADD
                        {
                            // загрузить знаяения в поля формы
                            dataFill(strID, iMode);
                            this.Text = "Добавить показания";
                            btnSave.Text = "Добавить";
                            break;

                        }
                    case (int)Utilities.status.Edit: // EDIT
                        {
                            // загрузить знаяения в поля формы
                            dataFill(strID, iMode);
                            // запретить поля ДАТА, ТИП СЧЕТЧИКА
                            cbSelectedCounter.Enabled = false;
                            dtSelectedDate.Enabled = false;
                            this.Text = "Изменить показания";
                            btnSave.Text = "Изменить";
                            break;
                        }
                    case (int)Utilities.status.Delete:
                        {
                            // загрузить знаяения в поля формы
                            dataFill(strID, iMode);
                            // запретить поля ДАТА, ТИП СЧЕТЧИКА, значение
                            cbSelectedCounter.Enabled = false;
                            dtSelectedDate.Enabled = false;
                            tbValue.Enabled = false;
                            this.Text = "Удалить показания";
                            btnSave.Text = "Удалить";
                            break;
                        }
                    default:
                        {
                            break;
                        }


                }
            }
            catch (Exception ex)
            {
                Utilities.MessageProvider((int)Utilities.MsgStatus.Error, ex.Message.ToString());
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// заполнение полей данными
        /// получает ID выбранной строки в гриде и режим работы
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="iMode"></param>
        private void dataFill(string strID, int iMode)
        {
            string strQuery = "";
            DataTable dt = new DataTable();
            connection.Open();
            switch (iMode)
            {
                case (int)Utilities.status.Add: // ADD
                    {
                        strQuery = "SELECT Name, Code FROM CounterType";
                        sc.CommandText = strQuery; 
                        reader = sc.ExecuteReader();
                        dt.Columns.Add("Name", typeof(string));
                        dt.Columns.Add("Code", typeof(int));
                        dt.Load(reader);
                        cbSelectedCounter.ValueMember = "Code";
                        cbSelectedCounter.DisplayMember = "Name";
                        cbSelectedCounter.DataSource = dt;
                        connection.Close();
                        break;

                    }
                case (int)Utilities.status.Edit: // EDIT
                    {
                        strQuery = string.Format("SELECT Счетчик, Дата, Показания FROM vwCounterValue WHERE ID = '{0}'", strID);
                        sc.CommandText = strQuery;
                        reader = sc.ExecuteReader();
                        dt.Columns.Add("Счетчик", typeof(string));
                        dt.Columns.Add("Дата", typeof(string));
                        dt.Columns.Add("Показания", typeof(string));
                        dt.Load(reader);
                        cbSelectedCounter.DisplayMember = "Счетчик";
                        cbSelectedCounter.DataSource = dt;
                        //Вытаскиваю значения счетчика и даты в textbox и datepicker
                        dtSelectedDate.Text = dt.Rows[0]["Дата"].ToString();
                        tbValue.Text = dt.Rows[0]["Показания"].ToString();
                        connection.Close();
                        break;
                    }
                case (int)Utilities.status.Delete:
                    {
                        strQuery = string.Format("SELECT Счетчик, Дата, Показания FROM vwCounterValue WHERE ID = '{0}'", strID);
                        sc.CommandText = strQuery;
                            reader = sc.ExecuteReader();
                            dt.Columns.Add("Счетчик", typeof(string));
                            dt.Columns.Add("Дата", typeof(string));
                            dt.Columns.Add("Показания", typeof(string));
                            dt.Load(reader);
                            cbSelectedCounter.DisplayMember = "Счетчик";
                            cbSelectedCounter.DataSource = dt;
                            //Вытаскиваю значения счетчика и даты в textbox и datepicker
                            dtSelectedDate.Text = dt.Rows[0]["Дата"].ToString();
                            tbValue.Text = dt.Rows[0]["Показания"].ToString();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        /// <summary>
        /// Функция заполнения БД введенными данными. 
        /// получает ID, режим вызова, введенное пользователем значение 
        /// формирует строку запроса, и выполняет его
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="iMode"></param>
        /// <param name="intValue"></param>
        private void AddEditDeleteValue(string strID, int iMode, int intValue)
        {

            string strQuery = "";
            connection.Open();

            switch (iMode)
            {
                case (int)Utilities.status.Delete:
                    {
                        strQuery = String.Format("DELETE FROM CounterValue WHERE ID = '{0}'", strID);
                        break;
                    }
                case (int)Utilities.status.Edit:
                    {
                        strQuery = String.Format("UPDATE CounterValue SET value = '{0}' WHERE ID = '{1}'", intValue, strID);
                        break;
                    }
                case (int)Utilities.status.Add:
                    {
                        strQuery = String.Format("INSERT INTO CounterValue (Code,DT,Value) VALUES ('{0}','{1}','{2}')", cbSelectedCounter.GetItemText(cbSelectedCounter.SelectedValue), dtSelectedDate.Value.Date.ToString("yyyy-MM-dd"), intValue);
                        break;
                    }
            }
            try
            {
                sc.CommandText = strQuery;
                sc.ExecuteNonQuery();
                sc.Dispose();
                sc = null;
            }
            catch (Exception ex)
            {
                Utilities.MessageProvider((int)Utilities.MsgStatus.Error, ex.Message.ToString());
            }
            finally
            {
                connection.Close();
            }
        }
        /// <summary>
        /// кнопка сохранения
        /// содержит проверки введенных данных(int && =>0)
        /// вызывает функцию записи в БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.TryParse(tbValue.Text, out intValue))//проверка введенного показания на целое
                {
                    if (intValue >= 0)
                    {
                        switch (iMode)
                        {
                            case (int)Utilities.status.Add:
                                {
                                    //insert
                                    intValue = Convert.ToInt32(tbValue.Text);
                                    AddEditDeleteValue(strID, iMode, intValue);
                                    this.Close();
                                    break;
                                }

                            case (int)Utilities.status.Edit:
                                {
                                    //update
                                    intValue = Convert.ToInt32(tbValue.Text);
                                    AddEditDeleteValue(strID, iMode, intValue);
                                    this.Close();
                                    break;
                                }

                            case (int)Utilities.status.Delete:
                                {
                                    // delete
                                    intValue = Convert.ToInt32(tbValue.Text);
                                    DialogResult dialogResult = MessageBox.Show("Вы точно хотите удалить показания?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        AddEditDeleteValue(strID, iMode, intValue);
                                    }
                                    this.Close();
                                    break;
                                }
                        }
                    }
                    else
                    {
                        Utilities.MessageProvider((int)Utilities.MsgStatus.Warning, "Введены неверные показания");
                    }
                }
                else
                {
                    Utilities.MessageProvider((int)Utilities.MsgStatus.Warning, "Введите неотрицательное число");
                }
            }
            catch (Exception ex)
            {
                Utilities.MessageProvider((int)Utilities.MsgStatus.Error, ex.Message.ToString());
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
