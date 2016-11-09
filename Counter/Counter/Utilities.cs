using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Counter
{
    public class Utilities
    {
        //допустимые режимы работы формы редактирования
        public enum status : int
        {
            Add = 0,
            Edit = 1,
            Delete = 2

        }
        ///управляет режимом вызова информационного окна
        public enum MsgStatus : int
        {
            Info = 0,
            Warning = 1,
            Error = 2,
            Question = 3
        }
        /// <summary>
        /// Конвертер даты в строковом формате в формат yyyy-MM-dd
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string DateConvert(string input)
        {
            //Полученную строку переводим в DateTime, затем форматируем при обратном переводе в string
            DateTime dtTest;
            dtTest = Convert.ToDateTime(input);
            input = String.Format("{0:yyyy-MM-dd}", dtTest);
            return input;
        }
        //сообщения об ошибках, предупреждения, инфо
        public static void MessageProvider(int status, string msg)
        {
            switch (status)
            {
                case (int)MsgStatus.Info:
                    {
                        MessageBox.Show(msg, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
                    }
                case (int)MsgStatus.Warning:
                    {
                        MessageBox.Show(msg, "Предупреждение", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;
                    }
                case (int)MsgStatus.Error:
                    {
                        MessageBox.Show("Ошибка!\nТекст ошибки:\n" + msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    }
                case (int)MsgStatus.Question:
                    {
                        MessageBox.Show(msg, "Вы уверены?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        break;
                    }
                default:
                    {
                        MessageBox.Show(msg, "Сообщение", MessageBoxButtons.OK, MessageBoxIcon.None);
                        break;
                    }
            }
        } 
    }
}
