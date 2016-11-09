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
//для экспорта в csv
using System.IO;
//для экспорта в эксель
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
namespace Counter
{
    public partial class frmCounterValueList : Form
    {
        SqlConnection connection = new SqlConnection();
        SqlCommand sc = new SqlCommand();
        DataTable dTab = new DataTable();
        SqlDataAdapter sqlDataAdapter1 = new SqlDataAdapter();
        SqlDataReader reader;
        string strQuery;
        //флаг на первоначальное заполнение отчета
        int flag;
        //шаблон текста запроса в SQL
        const string sQueryTemplate = "SELECT Дата, Счетчик, Показания, Код, ID FROM vwCounterValue WHERE {0} AND {1} AND {2} ORDER BY Дата DESC"; 
        
        /// <summary>
        /// Заполняет комбобокс фильтра типами счетчиков, в Value записывает код счетчика
        /// </summary>
        private void FillComboBox()
        {
            DataTable dTabCB = new DataTable();
            strQuery = "SELECT DISTINCT Счетчик, Код FROM vwCounterValue";
            sc.CommandText = strQuery;
                connection.Open();
                reader = sc.ExecuteReader();
                dTabCB.Columns.Add("Счетчик", typeof(string));
                dTabCB.Columns.Add("Код", typeof(int));
                //добавление кастомного счетчика ВСЕ
                DataRow _All = dTabCB.NewRow();
                _All["Счетчик"] = "Все";
                _All["Код"] = "0";
                dTabCB.Rows.Add(_All);
                //Конец добавления кастомного
                dTabCB.Load(reader);
                cbSelectCounter.ValueMember = "Код";
                cbSelectCounter.DisplayMember = "Счетчик";
                cbSelectCounter.DataSource = dTabCB;
                connection.Close();
        }//заполнение комбобокса выбора счетчика
        
        /// <summary>
        /// Формирует строку запроса с учетом фильтра или без него
        /// Выполняет запрос в БД и заполняет грид
        /// </summary>
        /// <param name="cmdText"></param>
        private void FillGrid()
        {
            string cmdText = String.Format(sQueryTemplate, "1=1", "1=1", "1=1"); 
            dTab.Clear();
            if (flag == 0)
            {
                dgvView.DataSource = null;
                dgvView.Rows.Clear();
                dgvView.Refresh();
            }
            connection.Open();
                //если флаг позволяет и фильтр включен
                if (flag == 0 && chbToggleFilter.Checked == true)
                {
                    string dateFrom = dtDateFrom.Value.Date.ToString("yyyy-MM-dd");
                    string dateTo = dtDateTo.Value.Date.ToString("yyyy-MM-dd");
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
                    cmdText = String.Format(sQueryTemplate, AllCounters + SelectedCounter + "'", "Дата >= '" + dateFrom + "'", "Дата <='" + dateTo + "'");

                }
                //Если флаг позволяет и фильтр выключен
                else if (flag == 1 && chbToggleFilter.Checked == false)
                {
                    cmdText = String.Format(sQueryTemplate, "1=1", "1=1", "1=1");
                }
                sc.CommandText = cmdText;
                sqlDataAdapter1.SelectCommand = sc;
                sqlDataAdapter1.Fill(dTab);
                connection.Close();
                this.dgvView.DataSource = dTab;
            //выравнивание показаний счетчика по правому краю в dataGridView
            dgvView.Columns[2].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            //прячу колонку код и ID
            this.dgvView.Columns["Код"].Visible = false;
            this.dgvView.Columns["ID"].Visible = false;
        }//Заполнение грида с фильтром или без 

        public frmCounterValueList()
        {
            InitializeComponent();
            connection.ConnectionString = config.GetConnectionString();
            sc.Connection = connection;
        }
        
        /// <summary>
        /// при загрузке формы формируем первоначальную строку запроса без фильтра
        /// очищаем дататэйбл
        /// заполняем комбобокс фильтра типами счетчиков
        /// заполняем грид
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmCounterValueList_Load(object sender, EventArgs e)
        {
            //очистка datatable и dataGridView
            dgvView.DataSource = null;
            try
            {
                flag = 1;
                FillGrid();
                FillComboBox();
                flag = 0;
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

        private void frmCounterValueList_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                //очистка datatable и dataGridView
                dTab.Clear();
                dgvView.DataSource = null;
                Hide();
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                flag = 1;
                FillGrid();
                FillComboBox();
                flag = 0;
            }
            catch (Exception ex)
            {
                Utilities.MessageProvider((int)Utilities.MsgStatus.Error, ex.Message.ToString());
            }
            finally
            {
                connection.Close();
            }
        }//Кнопка ОБНОВИТЬ
        
        /// <summary>
        /// Реагирует на чекбокс фильтра
        /// вызывает запрос в БД
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbToggleFilter_CheckedChanged(object sender, EventArgs e)//Переключение фильтра
        {
            try
            {
                if (chbToggleFilter.Checked == true)
                {
                    lblFilterStatus.Text = "Фильтр включен";
                    FillGrid();
                }
                else if (chbToggleFilter.Checked == false)
                {
                    lblFilterStatus.Text = "Фильтр выключен";
                    FillGrid();
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

        private void cbSelectCounter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (flag == 0)
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
                chbToggleFilter.Checked = true;
            }
        }//Включение фильтра при изменении одного из его параметров

        private void dtDateFrom_ValueChanged(object sender, EventArgs e)
        {
            if (flag == 0)
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
                chbToggleFilter.Checked = true;
            }
        }//Включение фильтра при изменении одного из его параметров

        private void dtDateTo_ValueChanged(object sender, EventArgs e)
        {
            if (flag == 0)
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
                chbToggleFilter.Checked = true;
            }
        }//Включение фильтра при изменении одного из его параметров
        
        /// <summary>
        /// Кнопка добавить
        /// переключает режим работы
        /// создает экземпляр формы редактирования
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd1_Click(object sender, EventArgs e)
        {
            frmCounterValueDetail frmCounterValueDetail = new frmCounterValueDetail((int)Utilities.status.Add);
            frmCounterValueDetail.ShowDialog();
            btnRefresh.PerformClick();
        }//Кнопка ДОБАВИТЬ
        
        /// <summary>
        /// Кнопка редактировать
        /// переключает режим работы 
        /// передает положение курсора в гриде
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit1_Click(object sender, EventArgs e)
        {
            //создание формы
            frmCounterValueDetail frmCounterValueDetail = new frmCounterValueDetail((int)Utilities.status.Edit, dgvView[4, dgvView.CurrentRow.Index].Value.ToString());
            frmCounterValueDetail.ShowDialog();
            btnRefresh.PerformClick();
        }//Кнопка ИЗМЕНИТЬ
        
        /// <summary>
        /// Кнопка удалить
        /// переключает режим работы 
        /// передает положение курсора в гриде
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete1_Click(object sender, EventArgs e)
        {
            frmCounterValueDetail frmCounterValueDetail = new frmCounterValueDetail((int)Utilities.status.Delete, dgvView[4, dgvView.CurrentRow.Index].Value.ToString());
            frmCounterValueDetail.ShowDialog();
            btnRefresh.PerformClick();
        }//Кнопка УДАЛИТЬ
        
        /// <summary>
        /// Кнопка экспорта
        /// вызывает диалог выбора пути и расширения файла для экспорта
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            exportFileDialog.Filter = "CSV file|*.csv|" + "Excel file|*.xlsx";
            exportFileDialog.Title = "Export to File";
            exportFileDialog.FileName = String.Format("Показания счетчиков {0}",DateTime.Now.ToString("yyyy-MM-dd"));
            exportFileDialog.ShowDialog();
            
        }//Кнопка ЭКСПОРТ

        private void exportFileDialog_FileOk(object sender, CancelEventArgs e)//Получение пути для экспорта в xlsx или csv 
        {
                string outputFile = exportFileDialog.FileName;
                var extension = Path.GetExtension(exportFileDialog.FileName);
                FillGrid();
                switch (extension.ToLower())
                {
                    case ".csv":
                        exportCSV(outputFile);
                        break;
                    case ".xlsx":
                        exportXLSX(outputFile);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(extension);
                }
        }

        private void exportCSV(string outputFile)
        {
            if (dgvView.RowCount > 0)
            {
                string value = "";
                DataGridViewRow dr = new DataGridViewRow();
                StreamWriter swOut = new StreamWriter(outputFile, true, System.Text.Encoding.GetEncoding(1251));

                //запись названий колонок в csv
                for (int i = 0; i <= dgvView.Columns.Count - 3; i++)
                {
                    if (i > 0)
                    {
                        swOut.Write(";");
                    }
                    swOut.Write(dgvView.Columns[i].HeaderText);
                }

                swOut.WriteLine();

                //запись тела таблицы в csv
                for (int j = 0; j <= dgvView.Rows.Count - 1; j++)
                {
                    if (j > 0)
                    {
                        swOut.WriteLine();
                    }

                    dr = dgvView.Rows[j];

                    for (int i = 0; i <= dgvView.Columns.Count - 3; i++)
                    {
                        if (i > 0)
                        {
                            swOut.Write(";");
                        }
                        value = dr.Cells[i].Value.ToString();
                        value = value.TrimEnd();
                        //проверка на работу с колонкой ДАТА
                        //если дата, то конвертируем ее в нужный формат
                        if (i == 0)
                        {
                            value = Utilities.DateConvert(value);
                        }
                        //обрезаем строку от лишних пробелов
                        //замена запятых на пробелы
                        value = value.Replace(',', ' ');
                        //замена переносов строк на пробелы
                        value = value.Replace(Environment.NewLine, " ");

                        swOut.Write(value);
                    }
                }
                swOut.Close();
            }
            Utilities.MessageProvider((int)Utilities.MsgStatus.Info, string.Format("Файл сохранен в:\n{0} ", outputFile));
        }//экспорт в CSV

        private void exportXLSX(string outputFile)
        {
            ExportDataSet(dTab, outputFile);
            Utilities.MessageProvider((int)Utilities.MsgStatus.Info, string.Format("Файл сохранен в:\n{0} ", outputFile));
        
        }//экспорт в XLSX

        private void ExportDataSet(DataTable table, string destination)
        {
            using (var workbook = SpreadsheetDocument.Create(destination, DocumentFormat.OpenXml.SpreadsheetDocumentType.Workbook))
            {
                //режем тэйбл от мусора
                table.Columns.Remove("Код");
                table.Columns.Remove("ID");
                
                //создание workbook+sheets 
                var workbookPart = workbook.AddWorkbookPart();
                workbook.WorkbookPart.Workbook = new DocumentFormat.OpenXml.Spreadsheet.Workbook();
                workbook.WorkbookPart.Workbook.Sheets = new DocumentFormat.OpenXml.Spreadsheet.Sheets();
                
                //собственный формат для даты
                //styles
                WorkbookStylesPart workbookStylesPart = workbookPart.AddNewPart<WorkbookStylesPart>("rId3");
                Stylesheet stylesheet = new Stylesheet();
                //  Date Time Display Format when s="1" is applied to cell
                NumberingFormats numberingFormats = new NumberingFormats() { Count = (UInt32Value)1U };
                NumberingFormat numberingFormat = new NumberingFormat() { NumberFormatId = (UInt32Value)164U, FormatCode = "dd.MM.yyyy" };
                numberingFormats.Append(numberingFormat);
                // Cell font
                Fonts fonts = new Fonts() { Count = (UInt32Value)1U };
                DocumentFormat.OpenXml.Spreadsheet.Font font = new DocumentFormat.OpenXml.Spreadsheet.Font();
                FontSize fontSize = new FontSize() { Val = 11D };
                FontName fontName = new FontName() { Val = "Calibri" };
                font.Append(fontSize);
                font.Append(fontName);
                fonts.Append(font);
                // empty, but mandatory
                Fills fills = new Fills() { Count = (UInt32Value)1U };
                Fill fill = new Fill();
                fills.Append(fill);
                Borders borders = new Borders() { Count = (UInt32Value)1U };
                Border border = new Border();
                borders.Append(border);
                // cellFormat1 for text cell cellFormat2 for Datetime cell 
                CellFormats cellFormats = new CellFormats() { Count = (UInt32Value)2U };
                CellFormat cellFormat1 = new CellFormat() { FontId = (UInt32Value)0U };
                CellFormat cellFormat2 = new CellFormat() { NumberFormatId = (UInt32Value)164U, FontId = (UInt32Value)0U, ApplyNumberFormat = true };
                cellFormats.Append(cellFormat1);
                cellFormats.Append(cellFormat2);
                // Save as styles
                stylesheet.Append(numberingFormats);
                stylesheet.Append(fonts);
                stylesheet.Append(fills);
                stylesheet.Append(borders);
                stylesheet.Append(cellFormats);
                workbookStylesPart.Stylesheet = stylesheet;
                
                {
                    var sheetPart = workbook.WorkbookPart.AddNewPart<WorksheetPart>();
                    var sheetData = new DocumentFormat.OpenXml.Spreadsheet.SheetData();
                    sheetPart.Worksheet = new DocumentFormat.OpenXml.Spreadsheet.Worksheet(sheetData);

                    DocumentFormat.OpenXml.Spreadsheet.Sheets sheets = workbook.WorkbookPart.Workbook.GetFirstChild<DocumentFormat.OpenXml.Spreadsheet.Sheets>();
                    string relationshipId = workbook.WorkbookPart.GetIdOfPart(sheetPart);

                    uint sheetId = 1;
                    if (sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Count() > 0)
                    {
                        sheetId =
                            sheets.Elements<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Select(s => s.SheetId.Value).Max() + 1;
                    }

                    DocumentFormat.OpenXml.Spreadsheet.Sheet sheet = new DocumentFormat.OpenXml.Spreadsheet.Sheet() { Id = relationshipId, SheetId = sheetId, Name = "Отчет" };
                    sheets.Append(sheet);

                    DocumentFormat.OpenXml.Spreadsheet.Row headerRow = new DocumentFormat.OpenXml.Spreadsheet.Row();

                    List<String> columns = new List<string>();
                    
                    //создаем header в xlsx 
                    foreach (System.Data.DataColumn column in table.Columns)
                    {
                        columns.Add(column.ColumnName);
                        DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                        cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                        cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(column.ColumnName);
                        headerRow.AppendChild(cell);
                    }
                    sheetData.AppendChild(headerRow);
                    
                    //Перенос из datatable
                    foreach (System.Data.DataRow dsrow in table.Rows)
                    {
                        DocumentFormat.OpenXml.Spreadsheet.Row newRow = new DocumentFormat.OpenXml.Spreadsheet.Row();
                        foreach (String col in columns)
                        {
                            DocumentFormat.OpenXml.Spreadsheet.Cell cell = new DocumentFormat.OpenXml.Spreadsheet.Cell();
                            //проверка типа входных данных
                            Type dtype = dsrow[col].GetType();
                            switch (dtype.Name.ToString())
                            {
                                case "DateTime":
                                    DateTime dt = Convert.ToDateTime(dsrow[col].ToString());
                                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(dt.ToOADate().ToString()); 
                                    cell.StyleIndex = 1; 
                                    break;
                                
                                case "String":
                                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.String;
                                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(dsrow[col].ToString());
                                    break;
                                
                                case "Int32":
                                    cell.DataType = DocumentFormat.OpenXml.Spreadsheet.CellValues.Number;
                                    cell.CellValue = new DocumentFormat.OpenXml.Spreadsheet.CellValue(dsrow[col].ToString());
                                    break;
                                
                                default:
                                    break;
                            }
                            newRow.AppendChild(cell);
                        }

                        sheetData.AppendChild(newRow);
                    }

                }
            }
        }//вывод dataTable в xlsx с учетом типа данных

    }
}
    