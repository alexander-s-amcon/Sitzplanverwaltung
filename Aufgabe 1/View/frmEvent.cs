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
            saalliste = saalliste.OrderBy(x => x.Saalname).ToList();
            comboBox1.DataSource = saalliste;
            comboBox1.DisplayMember = "Saalname";
            ShowDialog();

            if (erfolg == true)
            {
                veranstaltung.Name = textBox1.Text;
                veranstaltung.Saal = comboBox1.Text;
                string datum = dateTimePicker1.Value.ToShortDateString();
                veranstaltung.Datum = datum;
                string bis = dateTimePicker2.Value.ToShortDateString();
                veranstaltung.Bis = bis;
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

            if (textBox1.Text == "") 
            {
                erfolg = false;
            }
            this.Close();
        }
    }
}
