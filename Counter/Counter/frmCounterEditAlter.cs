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
    public partial class frmCounterEditAlter : Form
    {
        ///подключение
        SqlConnection connection = new SqlConnection();
        SqlCommand sc = new SqlCommand();
        // хранит режим работы формы, переданный как параметр при создании экземпляра формы
        int iMode;
        // хранит ID выбранной в гриде строки, переданный как параметр при создании экземпляра формы
        string strID;
        // введенное пользователем имя счетчика при редактировании
        string strName;

        public frmCounterEditAlter(int Mode, string ID = "1")
        {
            InitializeComponent();
            //создаем строку подключения
            connection.ConnectionString = config.GetConnectionString();
            sc.Connection = connection;
            iMode = Mode;
            strID = ID;
        }
        
        /// <summary>
        /// При загрузке формы редактирования проверяется режим работы 
        /// и формы приводится в визуальное соответствие выбранному режиму
        /// Поля заполняются данными
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCounterEditAlter_Load(object sender, EventArgs e)
        {
            
            // Выясняем режим вызова формы
            try
            {
                switch (iMode)
                {
                    case (int)Utilities.status.Add: // ADD
                        {
                            btnSave.Text = "Сохранить";
                            this.Text = "Добавить счетчик";
                            break;

                        }
                    case (int)Utilities.status.Edit: // EDIT
                        {
                            // загрузить знаяения в поля формы
                            dataFill(strID, iMode);
                            //приведение формы в визуальное соответствие выбранного режима работы 
                            tbCode.Enabled = true;
                            this.Text = "Изменить счетчик";
                            btnSave.Text = "Сохранить";
                            break;
                        }
                    case (int)Utilities.status.Delete:
                        {
                            // загрузить знаяения в поля формы
                            dataFill(strID, iMode);
                            //приведение формы в визуальное соответствие выбранного режима работы 
                            tbCode.Enabled = false;
                            tbName.Enabled = false;
                            this.Text = "Удалить счетчик";
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
/// Функция заполнения полей в режимах изменения и удаления.
/// Делает запрос в БД типов счетчиков, получает код и название
/// Передает в соответствующие поля. 
/// В качестве параметра получает ID счетчика, выбранного в гриде и режим работы формы
/// </summary>
/// <param name="strID"></param>
/// <param name="iMode"></param>
        private void dataFill(string ID, int Mode)
        {
            connection.Open();
            SqlDataReader reader;
            DataTable dt = new DataTable();
            // В режиме добавления действия не выполняются
            if (Mode != (int)Utilities.status.Add) 
            { 
                        string strQuery = string.Format("SELECT Name, Code FROM CounterType WHERE ID = '{0}'", ID);
                        sc.CommandText = strQuery;
                        reader = sc.ExecuteReader();
                        dt.Columns.Add("Name", typeof(string));
                        dt.Columns.Add("Code", typeof(string));
                        dt.Load(reader);
                        //Вытаскиваю значения счетчика и даты в textbox и datepicker
                        tbCode.Text = dt.Rows[0]["Code"].ToString().TrimEnd();
                        tbName.Text = dt.Rows[0]["Name"].ToString().TrimEnd();
             }
            connection.Close();
        }//заполнение полей в вызванной форме
        
        /// <summary>
        /// Функция, вносящая изменения в БД, создает строку запроса в связи с выбранным режимом
        /// В качестве параметров получает ID выбранной в гриде записи, режим работы, и название, введенное пользователем
        /// Вызывается по нажатию кнопки сохранить
        /// </summary>
        /// <param name="strID"></param>
        /// <param name="iMode"></param>
        /// <param name="strName"></param>
        private void AddEditDeleteValue(string ID, int Mode, string strName)
        {
            string strQuery = "";
            strName = strName.TrimEnd();
            int intCode = Convert.ToInt32(tbCode.Text);
            connection.Open();
            switch (Mode)
            {
                case (int)Utilities.status.Delete:
                    {
                        strQuery = String.Format("DELETE FROM CounterType WHERE ID = '{0}'", ID);
                        break;
                    }
                case (int)Utilities.status.Edit:
                    {
                        strQuery = String.Format("UPDATE CounterType SET Name = '{0}', Code = '{1}' WHERE ID = '{2}'", strName, intCode, ID);
                        break;
                    }
                case (int)Utilities.status.Add:
                    {
                        strQuery = String.Format("INSERT INTO CounterType (Code, Name) VALUES ('{0}','{1}')", intCode, strName);
                        break;
                    }
            }
                sc.CommandText = strQuery;
                sc.ExecuteNonQuery();
                sc.Dispose();
                sc = null;
            connection.Close();
        }//изменение таблицы счетчиков
       
        /// <summary>
        /// Кнопка сохранения
        /// Содержит проверку введенных данных(код=int&&>0, имя не пустое), подтверждение удаления
        /// Вызывает функцию редактирования БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)//вызываем метод внесения показаний в базу
        {
            int intCode;
            if (int.TryParse(tbCode.Text, out intCode))
            {
                if (intCode > 0)//проверка введенного кода на > 0
                {

                    try
                    {
                        switch (iMode)
                        {
                            case (int)Utilities.status.Add:
                                {
                                    //добавить
                                    strName = tbName.Text.ToString();
                                    if (strName != "")
                                    {
                                        AddEditDeleteValue(strID, iMode, strName);
                                        this.Close();
                                    }
                                    else
                                    {
                                        Utilities.MessageProvider((int)Utilities.MsgStatus.Warning, "Введите название счетчика");
                                    }
                                    break;
                                }

                            case (int)Utilities.status.Edit:
                                {
                                    //изменить
                                    strName = tbName.Text.ToString();
                                    if (strName != "")
                                    {
                                        AddEditDeleteValue(strID, iMode, strName);
                                        this.Close();
                                    }
                                    else
                                    {
                                        Utilities.MessageProvider((int)Utilities.MsgStatus.Warning, "Введите название счетчика");
                                    }
                                    break;
                                }

                            case (int)Utilities.status.Delete:
                                {
                                    //удалить
                                    DialogResult dialogResult = MessageBox.Show("Вы точно хотите удалить счетчик?", "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (dialogResult == DialogResult.Yes)
                                    {
                                        strName = tbName.Text.ToString();
                                        AddEditDeleteValue(strID, iMode, strName);
                                    }
                                    this.Close();
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
                else
                {
                    Utilities.MessageProvider((int)Utilities.MsgStatus.Warning, "Введите неотрицательное число");
                }
            }
            else
            {
                Utilities.MessageProvider((int)Utilities.MsgStatus.Warning, "Введите число больше нуля");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }//закрытие окна
        /// <summary>
        /// Функция сообщений 
        /// Принимает режим и сообщение как параметры
        /// возвращает сообщение об ошибке, предупреждение, информацию, вопрос
        /// </summary>
        /// <param name="status"></param>
        /// <param name="msg"></param>
        
    }
}
