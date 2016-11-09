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
    public partial class frmCounterEdit : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand comand = new SqlCommand();
        DataTable dTab = new DataTable();
        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter();
      
  
        public frmCounterEdit()
        {
            InitializeComponent();
            //получаем строку подключения
            connection.ConnectionString = config.GetConnectionString();
            comand.Connection = connection;
        }

        private void frmCounterEdit_Load(object sender, EventArgs e)
        {
            try
            {
                FillGrid();
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
/// Получает данные из таблицы счетчиков для отображения в grid
/// </summary>
        private void FillGrid()
        {
            try
            {
                dTab.Clear();
                dgvCounters.DataSource = null;
                dgvCounters.Rows.Clear();
                dgvCounters.Refresh();
                connection.Open();
                comand.CommandText = "SELECT Code AS 'Код', Name AS 'Наименование', ID FROM counterType";
                sqlDataAdapter.SelectCommand = comand;
                sqlDataAdapter.Fill(dTab);
                this.dgvCounters.DataSource = dTab;
                connection.Close();
                //выравнивание ID и кода счетчика по правому краю в dataGridView
                dgvCounters.Columns[0].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                this.dgvCounters.Columns["ID"].Visible = false;
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
/// Кнопка Обновить в форме типов счетчиков
/// Вызывает функцию запроса в БД и заполнения грида
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                FillGrid();
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
/// Кнопка добавить в форме типов счетчиков
/// Меняет режим работы формы редактирования и создает ее экземпляр
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmCounterEditAlter frmCounterEditAlter = new frmCounterEditAlter((int)Utilities.status.Add);
            frmCounterEditAlter.ShowDialog();
            btnRefresh.PerformClick();
        }

        /// <summary>
        ///Кнопка редактировать в форме типов счетчиков
        ///Передает положение курсора в гриде 
        ///Меняет режим работы формы редактирования и создает ее экземпляр
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            //создание экземпляр формы
            frmCounterEditAlter frmCounterEditAlter = new frmCounterEditAlter((int)Utilities.status.Edit, dgvCounters[2, dgvCounters.CurrentRow.Index].Value.ToString());
            frmCounterEditAlter.ShowDialog();
            btnRefresh.PerformClick();
            }

        /// <summary>
        ///Кнопка удалить в форме типов счетчиков
        ///Передает положение курсора в гриде 
        ///Меняет режим работы формы редактирования и создает ее экземпляр
/// </summary>
/// <param name="sender"></param>
/// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            frmCounterEditAlter frmCounterEditAlter = new frmCounterEditAlter((int)Utilities.status.Delete, dgvCounters[2, dgvCounters.CurrentRow.Index].Value.ToString());
            frmCounterEditAlter.ShowDialog();
            btnRefresh.PerformClick();
        }
    }
}

