using Aufgabe_1.Datenbankmethoden;
using Aufgabe_1.Interfaces.Datenbankmethoden;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aufgabe_1
{
    public partial class frmSaal : Form
    {

        private bool erfolg;

        public frmSaal()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public bool Zeige(ref Saele saal)
        {
            textBox1.Text = saal.Saalname;

            numericUpDown1.Value = saal.Reihen;
            numericUpDown2.Value = saal.Sitzplaetze;
            ShowDialog();

            if (erfolg == true)
            {
                saal.Saalname = textBox1.Text;
                saal.Reihen = Convert.ToInt32(numericUpDown1.Value+1);
                saal.Sitzplaetze = Convert.ToInt32(numericUpDown2.Value+1);
            }
            return erfolg;
        }
        private void btnOK_Click(object sender, EventArgs e)
        {
            erfolg = true;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            erfolg = false;
            this.Close();
        }
    }
}
