using Aufgabe_1.Datenbankmethoden;
using Aufgabe_1.Interfaces.Datenbankmethoden;
using Aufgabe_1.Model;
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
    public partial class frmEvent : Form
    {
        private bool erfolg;
        public frmEvent()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        public bool Zeige(ref Veranstaltungen veranstaltung)
        {
            frmMain frmMain = new frmMain();
            textBox1.Text = veranstaltung.Name;
            IDBSaal dBSaal = new DBSaal();
            List<Saele> saalliste = dBSaal.LadeSaal();
            comboBox1.DataSource = saalliste;
            comboBox1.DisplayMember = "Saalname";
            ShowDialog();
            frmMain.test = 13;

            if (erfolg == true)
            {
                veranstaltung.Name = textBox1.Text;
                veranstaltung.Saal = comboBox1.Text;
                string datumVon = dateTimePicker1.Value.ToShortDateString();
                veranstaltung.DatumVon = datumVon;
                string datumBis = dateTimePicker2.Value.ToShortDateString();
                veranstaltung.DatumBis = datumBis;
            }
            return erfolg;
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            erfolg = false;
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            erfolg = true;
            this.Close();
        }
    }
}
