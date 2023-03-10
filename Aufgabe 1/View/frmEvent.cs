using Aufgabe_1.Datenbankmethoden;
using Aufgabe_1.Interfaces.Datenbankmethoden;
using Aufgabe_1.Model;
using C1.Win.C1TrueDBGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace Aufgabe_1
{
    public partial class frmEvent : Form
    {
        const string connectionString = "Data Source = Datenbank.sqlite;";
        private SQLiteConnection db_Connection = null;
        private bool erfolg;
        DateTime datum;
        DateTime bis;
        public frmEvent()
        {
            InitializeComponent();
            frmMain frmMain = new frmMain();
            db_Connection = new SQLiteConnection();
            db_Connection.ConnectionString = connectionString;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dateTimePicker1.MinDate = DateTime.Now;
            dateTimePicker2.MinDate = DateTime.Now;
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.CustomFormat = "dd/MM/yyyy HH:mm";
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.CustomFormat = "dd/MM/yyyy HH:mm";
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
                datum = Convert.ToDateTime(dateTimePicker1.Value);
                veranstaltung.von = datum;
                bis = Convert.ToDateTime(dateTimePicker2.Value);
                veranstaltung.bis = bis;
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

            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();
            sql_Command.CommandText = $"SELECT Datum, Datum FROM Veranstaltungen WHERE Saal = '{comboBox1.Text}'";
            SQLiteDataReader reader = sql_Command.ExecuteReader();
            List<DateTime> dtms = new List<DateTime>();
            List<DateTime> biss = new List<DateTime>();
            DateTime dtm1 = Convert.ToDateTime(dateTimePicker1.Value);
            DateTime dtm2 = Convert.ToDateTime(dateTimePicker2.Value);
            while (reader.Read())
            {
                DateTime dtm = Convert.ToDateTime(reader.GetString(0));
                DateTime bis = Convert.ToDateTime(reader.GetString(1));
                dtms.Add(dtm);
                biss.Add(bis);
            }
            reader.Close();
            if (dtms.Count > 0)
            {
                foreach (DateTime dtm in dtms)
                {
                    foreach (DateTime bis in biss)
                    {
                        if (dtm < dtm1 && dtm2 < bis)
                        {
                            MessageBox.Show("Es ist bereits eine Veranstaltung für diesen Zeitpunkt geplant.");
                            erfolg = false;
                            break;
                        }
                        else
                        {
                            erfolg = true;
                        }
                    }
                }
            }
            else { erfolg = true; }

            db_Connection.Close();

            if (textBox1.Text == "")
            {
                erfolg = false;
            }
            this.Close();


        }
    }
}
