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
        public string Saalname;

        public frmSaal()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            frmMain f1 = new frmMain();
            Saalname = textBox1.Text;
            IDBSaal dBSaal = new DBSaal();
            Saele saal = new Saele(); 
            saal.Saalname = Saalname;
            dBSaal.AddSaal(saal);
            this.Close();
        }
    }
}
