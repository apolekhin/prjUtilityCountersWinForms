using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Counter
{
    public partial class Form1 : Form
    {

        private frmCounterValueList frmCounterValueList = new frmCounterValueList();
        private frmCounterEdit frmCounterEdit = new frmCounterEdit();
        
        public Form1()
        {
            InitializeComponent();
            AddOwnedForm(frmCounterValueList);
            AddOwnedForm(frmCounterEdit);

        }


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void выходToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void mnuCounterValueList_Click(object sender, EventArgs e)
        {
            frmCounterValueList.ShowDialog();
        }

        private void счетчикиToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmCounterEdit.ShowDialog();
        }
    }
}
