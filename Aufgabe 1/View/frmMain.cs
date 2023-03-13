using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using Aufgabe_1.Datenbankmethoden;
using Aufgabe_1.Interfaces.Datenbankmethoden;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Web.UI.Design;
using Aufgabe_1.Model;
using System.Data.SqlClient;
using C1.Framework;
using Aufgabe_1.View;
using System.Drawing.Text;

namespace Aufgabe_1
{
    public partial class frmMain : Form
    {
        const string connectionString = "Data Source = Datenbank.sqlite;";
        private SQLiteConnection db_Connection = null;
        public CellStyle WhiteCellStyle;
        int Reihen;
        int Sitzplaetze;
        int reihen;
        int sitzplaetze;
        public string EventName { get; set; }
        private List<Sitzplatz> _sitzplatzliste;
        public frmMain()
        {
            InitializeComponent();
            btnVeranstaltungEdit.Enabled = false;
            btnVeranstaltundDelete.Enabled = false;
            btnSaalDelete.Enabled = false;
            btnSaalEdit.Enabled = false;
            button1.Enabled = false;
            gridSaal.AllowEditing = false;
            gridSaal.Rows.Fixed = 0;
            gridSaal.Cols.Fixed = 0;
            for (int i = 0; i < c1FlexGrid1.Cols.Count; i++)
            {
                c1FlexGrid1.Cols[i].Visible = false;
            }
            db_Connection = new SQLiteConnection();
            db_Connection.ConnectionString = connectionString;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void LadeSaele()
        {
            IDBSaal dBSaal = new DBSaal();
            List<Saele> saalliste = dBSaal.LadeSaal();
            saalliste = saalliste.OrderBy(x => x.Saalname).ToList();
            gridSaal.DataSource = saalliste;
            gridSaal.Cols[0].Visible = false;
            gridSaal.Cols[2].Visible = false;
            gridSaal.Cols[3].Visible = false;
            gridSaal.Cols[1].Width = 191;
        }

        private void LadeFlexgrid()
        {
            IDBVeranstaltung dBVeranstaltung = new DBVeranstaltung();
            Veranstaltungen veranstaltung = new Veranstaltungen();
            IDBSaal dBSaal = new DBSaal();
            Saele saal = new Saele();
            string saalname = (string)gridSaal.GetData(gridSaal.RowSel, 1);
            SQLiteCommand sql_Command = new SQLiteCommand();
            db_Connection.Open();

            sql_Command = db_Connection.CreateCommand();
            sql_Command.CommandText = $"SELECT Reihen, Sitzplaetze FROM Saele WHERE Saalname = '{saalname}'";
            SQLiteDataReader reader;
            reader = sql_Command.ExecuteReader();
            while (reader.Read())
            {
                reihen = (int)reader.GetInt32(0);
                sitzplaetze = (int)reader.GetInt32(1);
            }
            reader.Close();
            sql_Command.Dispose();
            db_Connection.Close();
            c1FlexGrid1.Cols.Count = sitzplaetze;
            c1FlexGrid1.Rows.Count = reihen;

            for (int i = 1; i < sitzplaetze; i++)
            {
                c1FlexGrid1.Cols[i].Width = 40;
                c1FlexGrid1.Cols[i].Caption = i.ToString();
            }
        }

        private void LadeVeranstaltungen()
        {
            IDBSaal dBSaal = new DBSaal();
            Saele saal = new Saele();
            int rowsel = gridSaal.RowSel;
            saal.Id = (int)gridSaal.GetData(rowsel, 0);
            saal.Saalname = (string)gridSaal.GetData(rowsel, 1);
            db_Connection = new SQLiteConnection();
            db_Connection.ConnectionString = connectionString;
            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();
            sql_Command.CommandText = $"SELECT * FROM Veranstaltungen WHERE Saal = '{saal.Saalname}'";
            SQLiteDataReader reader = sql_Command.ExecuteReader();
            List<Veranstaltungen> veranstaltungsliste = new List<Veranstaltungen>();
            while (reader.Read())
            {
                Veranstaltungen veranstaltung = new Veranstaltungen();
                veranstaltung.Id = reader.GetInt32(0);
                veranstaltung.Name = reader.GetString(1);
                veranstaltung.Saal = reader.GetString(2);
                veranstaltung.von = Convert.ToDateTime(reader.GetString(3));
                veranstaltung.bis = Convert.ToDateTime(reader.GetString(4));
                veranstaltungsliste.Add(veranstaltung);
            }
            sql_Command.Dispose();
            db_Connection.Close();
            gridVeranstaltungen.DataSource = veranstaltungsliste;
            gridVeranstaltungen.Cols[4].Format = "dd/MM/yyyy HH:mm";
            gridVeranstaltungen.Cols[5].Format = "dd/MM/yyyy HH:mm";
            gridVeranstaltungen.Cols[0].Visible = false;
            gridVeranstaltungen.Cols[1].Visible = false;
            gridVeranstaltungen.Cols[3].Visible = false;
            gridVeranstaltungen.Cols[2].Width = 150;
            gridVeranstaltungen.Cols[4].Width = 150;
            gridVeranstaltungen.Cols[5].Width = 150;

        }

        public void LadeDatenbankeintraege()
        {
            for (int i = 0; i < c1FlexGrid1.Cols.Count; i++)
            {
                c1FlexGrid1.Cols[i].Visible = true;
            }
            IDBVeranstaltung dBVeranstaltung = new DBVeranstaltung();
            Veranstaltungen veranstaltung = new Veranstaltungen();
            int rowsel = gridVeranstaltungen.RowSel;
            EventName = (string)gridVeranstaltungen.GetData(rowsel, 2);
            veranstaltung.Name = EventName;
            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();
            sql_Command.CommandText = $"SELECT * FROM [{EventName}]";
            SQLiteDataReader reader = sql_Command.ExecuteReader();
            List<Sitzplatz> sitzplatzliste = new List<Sitzplatz>();
            while (reader.Read())
            {
                Sitzplatz Sitzplatz = new Sitzplatz();
                Sitzplatz.Id = reader.GetInt32(0);
                Sitzplatz.Reihe = reader.GetInt32(1);
                Sitzplatz.Spalte = reader.GetInt32(2);
                Sitzplatz.Zustand = reader.GetString(3);
                sitzplatzliste.Add(Sitzplatz);
            }
            _sitzplatzliste = sitzplatzliste;
            sql_Command.Dispose();
            db_Connection.Close();

            for (int rows = 0; rows < c1FlexGrid1.Rows.Count; rows++)
            {
                for (int cols = 0; cols < c1FlexGrid1.Cols.Count; cols++)
                {
                    c1FlexGrid1.SetCellStyle(rows, cols, WhiteCellStyle);
                }
            }

            for (int i = c1FlexGrid1.Rows.Fixed; i < c1FlexGrid1.Rows.Count; i++)
            {
                c1FlexGrid1[i, 0] = i.ToString();
            }

            db_Connection.Open();
            CellStyle c1 = c1FlexGrid1.Styles.Add("Reserviert");
            CellStyle c2 = c1FlexGrid1.Styles.Add("Freier Platz");
            CellStyle c3 = c1FlexGrid1.Styles.Add("Platzhalter");
            c1.BackColor = Color.Red;
            c2.BackColor = Color.Green;
            c3.BackColor = Color.Gray;

            for (int rows = 1; rows < c1FlexGrid1.Rows.Count; rows++)
            {
                for (int cols = 1; cols < c1FlexGrid1.Cols.Count; cols++)
                {
                    Sitzplatz sitzplatz = _sitzplatzliste.Where(x => x.Reihe == rows && x.Spalte == cols).FirstOrDefault();
                    string Zustand = sitzplatz.Zustand;

                    if (Zustand == "Reserviert")
                    {
                        c1FlexGrid1.SetCellStyle(rows, cols, c1);
                    }
                    else if (Zustand == "Freier Platz")
                    {
                        c1FlexGrid1.SetCellStyle(rows, cols, c2);
                    }
                    else if (Zustand == "Platzhalter")
                    {
                        c1FlexGrid1.SetCellStyle(rows, cols, c3);
                    }
                }
            }
            db_Connection.Close();
        }
        private void CreateDataBase()
        {
            db_Connection.Open();
            db_Connection.Close();
        }

        private void SetWhite()
        {
            WhiteCellStyle = c1FlexGrid1.Styles.Add("Default");
            WhiteCellStyle.BackColor = Color.White;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateDataBase();
            LadeSaele();
            SetWhite();
            c1FlexGrid1.BackColor = WhiteCellStyle.BackColor;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            gridVeranstaltungen.Cols[0].Visible = false;
            gridVeranstaltungen.Cols[1].Visible = false;
            gridVeranstaltungen.Cols[2].Visible = false;

            IDBVeranstaltung dBVeranstaltung = new DBVeranstaltung();
            dBVeranstaltung.CreateVeranstaltungen();
        }

        private void c1FlexGrid1_Click(object sender, EventArgs e)
        {
            int row = c1FlexGrid1.Row;
            int col = c1FlexGrid1.Col;
            CellStyle c1 = c1FlexGrid1.Styles.Add("Reserviert");
            CellStyle c2 = c1FlexGrid1.Styles.Add("Freier Platz");
            CellStyle c3 = c1FlexGrid1.Styles.Add("Platzhalter");
            c1.BackColor = Color.Red;
            c2.BackColor = Color.Green;
            c3.BackColor = Color.Gray;
            string eventname = (string)gridVeranstaltungen.GetData(gridVeranstaltungen.RowSel, 2);

            if (c1FlexGrid1.GetCellStyle(row, col).Name == WhiteCellStyle.Name)
            {
                c1FlexGrid1.SetCellStyle(row, col, c1);
                db_Connection.Open();
                SQLiteCommand sql_Command = new SQLiteCommand();
                sql_Command = db_Connection.CreateCommand();
                sql_Command.CommandText = $"UPDATE [{eventname}] SET Zustand = 'Reserviert' WHERE Reihe = '{row}' AND Sitzplatz = '{col}'";
                sql_Command.ExecuteNonQuery();
                sql_Command.Dispose();
                db_Connection.Close();
                return;
            }
            if (c1FlexGrid1.GetCellStyle(row, col).Name == c1.Name)
            {
                c1FlexGrid1.SetCellStyle(row, col, c2);
                db_Connection.Open();
                SQLiteCommand sql_Command = new SQLiteCommand();
                sql_Command = db_Connection.CreateCommand();
                sql_Command.CommandText = $"UPDATE [{eventname}] SET Zustand = 'Freier Platz' WHERE Reihe = '{row}' AND Sitzplatz = '{col}'";
                sql_Command.ExecuteNonQuery();
                sql_Command.Dispose();
                db_Connection.Close();
                return;
            }
            if (c1FlexGrid1.GetCellStyle(row, col).Name == c2.Name)
            {
                c1FlexGrid1.SetCellStyle(row, col, c3);
                db_Connection.Open();
                SQLiteCommand sql_Command = new SQLiteCommand();
                sql_Command = db_Connection.CreateCommand();
                sql_Command.CommandText = $"UPDATE [{eventname}] SET Zustand = 'Platzhalter' WHERE Reihe = '{row}' AND Sitzplatz = '{col}'";
                sql_Command.ExecuteNonQuery();
                sql_Command.Dispose();
                db_Connection.Close();
                return;
            }
            if (c1FlexGrid1.GetCellStyle(row, col).Name == c3.Name)
            {
                c1FlexGrid1.SetCellStyle(row, col, c3);
            }
            if (c1FlexGrid1.GetCellStyle(row, col).Name == c3.Name)
            {
                c1FlexGrid1.SetCellStyle(row, col, WhiteCellStyle.Name);
                db_Connection.Open();
                SQLiteCommand sql_Command = new SQLiteCommand();
                sql_Command = db_Connection.CreateCommand();
                sql_Command.CommandText = $"UPDATE [{eventname}] SET Zustand = '' WHERE Reihe = '{row}' AND Sitzplatz = '{col}'";
                sql_Command.ExecuteNonQuery();
                sql_Command.Dispose();
                db_Connection.Close();
            }
        }

        private void btnSaalAdd_Click(object sender, EventArgs e)
        {
            frmSaal frmSaal = new frmSaal();
            IDBSaal dBSaal = new DBSaal();
            Saele saal = new Saele();
            bool isClicked = frmSaal.Zeige(ref saal);
            if (isClicked == true)
            {
                dBSaal.AddSaal(saal);
            }
            LadeSaele();
        }

        private void btnSaalEdit_Click(object sender, EventArgs e)
        {
            frmSaal frmSaal = new frmSaal();
            IDBSaal dBSaal = new DBSaal();
            Saele saal = new Saele();
            int rowsel = gridSaal.RowSel;
            saal.Id = (int)gridSaal.GetData(rowsel, 0);
            saal.Saalname = (string)gridSaal.GetData(rowsel, 1);
            saal.Reihen = (int)gridSaal.GetData(rowsel, 2);
            saal.Sitzplaetze = (int)gridSaal.GetData(rowsel, 3);
            bool isClicked = frmSaal.Zeige(ref saal);
            if (isClicked == true)
            {
                dBSaal.EditSaal(saal);
                db_Connection.Open();
                SQLiteCommand sql_Command = new SQLiteCommand();
                sql_Command = db_Connection.CreateCommand();
                sql_Command.CommandText = $"SELECT * FROM Veranstaltungen WHERE Saal = '{saal.Saalname}'";
                SQLiteDataReader reader = sql_Command.ExecuteReader();
                List<string> list = new List<string>();
                while (reader.Read())
                {
                    string Saal = reader.GetString(1);
                    list.Add(Saal);
                }
                reader.Close();
                sql_Command.CommandText = $"SELECT Reihen, Sitzplaetze FROM Saele WHERE Saalname = '{saal.Saalname}'";
                SQLiteDataReader reader2 = sql_Command.ExecuteReader();
                while (reader2.Read())
                {
                    Reihen = (int)reader2.GetInt32(0);
                    Sitzplaetze = (int)reader2.GetInt32(1);
                }
                reader2.Close();
                foreach (object obj in list)
                {
                    sql_Command.CommandText = $"DELETE FROM '{obj}'";
                    sql_Command.ExecuteNonQuery();
                    for (int rows = 1; rows < Reihen; rows++)
                    {
                        for (int cols = 1; cols < Sitzplaetze; cols++)
                        {
                            sql_Command.CommandText = $"INSERT INTO [{obj}] (Reihe, Sitzplatz, Zustand) VALUES ('{rows} ', '{cols}', '')";
                            sql_Command.ExecuteNonQuery();
                        }
                    }
                }
                sql_Command.Dispose();
                db_Connection.Close();
                LadeFlexgrid();
                LadeDatenbankeintraege();
                LadeSaele();
            }
        }

        private void btnSaalDelete_Click(object sender, EventArgs e)
        {
            IDBSaal dBSaal = new DBSaal();
            Saele saal = new Saele();
            int saalId;
            int rowsel = gridSaal.RowSel;
            saalId = (int)gridSaal.GetData(rowsel, 0);
            saal.Id = saalId;
            dBSaal.DeleteSaal(saal);
            LadeSaele();
        }

        private void gridSaal_Click(object sender, EventArgs e)
        {
            btnSaalDelete.Enabled = true;
            btnSaalEdit.Enabled = true;
            LadeVeranstaltungen();
        }

        private void btnVeranstaltungAdd_Click(object sender, EventArgs e)
        {
            frmEvent frmEvent = new frmEvent();
            IDBVeranstaltung dBVeranstaltung = new DBVeranstaltung();
            Veranstaltungen veranstaltung = new Veranstaltungen();
            bool isClicked = frmEvent.Zeige(ref veranstaltung);
            if (isClicked == true)
            {
                dBVeranstaltung.AddVeranstaltung(veranstaltung);
            }
            LadeVeranstaltungen();
        }

        private void btnVeranstaltungEdit_Click(object sender, EventArgs e)
        {
            frmEvent frmEvent = new frmEvent();
            IDBVeranstaltung dBVeranstaltung = new DBVeranstaltung();
            Veranstaltungen veranstaltung = new Veranstaltungen();
            int rowsel = gridVeranstaltungen.RowSel;
            veranstaltung.Id = (int)gridVeranstaltungen.GetData(rowsel, 1);
            veranstaltung.Name = (string)gridVeranstaltungen.GetData(rowsel, 2);
            veranstaltung.Saal = (string)gridVeranstaltungen.GetData(rowsel, 3);
            veranstaltung.von = (DateTime)gridVeranstaltungen.GetData(rowsel, 4);
            veranstaltung.bis = (DateTime)gridVeranstaltungen.GetData(rowsel, 5);
            bool isClicked = frmEvent.Zeige(ref veranstaltung);
            if (isClicked == true)
            {
                dBVeranstaltung.EditVeranstaltung(veranstaltung);
            }

            LadeVeranstaltungen();
        }

        private void btnVeranstaltundDelete_Click(object sender, EventArgs e)
        {
            IDBVeranstaltung dBVeranstaltung = new DBVeranstaltung();
            Veranstaltungen veranstaltung = new Veranstaltungen();
            int eventId;
            int rowsel = gridVeranstaltungen.RowSel;
            eventId = (int)gridVeranstaltungen.GetData(rowsel, 1);
            veranstaltung.Id = eventId;
            dBVeranstaltung.DeleteVeranstaltung(veranstaltung);
            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();
            string EventName = (string)gridVeranstaltungen.GetData(gridVeranstaltungen.RowSel, 2);
            sql_Command.CommandText = $"DROP TABLE '{EventName}'";
            sql_Command.ExecuteNonQuery();
            sql_Command.Dispose();
            db_Connection.Close();
            LadeVeranstaltungen();
        }

        private void gridVeranstaltungen_Click(object sender, EventArgs e)
        {
            btnVeranstaltungEdit.Enabled = true;
            btnVeranstaltundDelete.Enabled = true;
            button1.Enabled = true;
            LadeFlexgrid();
            LadeDatenbankeintraege();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmPlatzsuche frmPlatzsuche = new frmPlatzsuche(EventName, reihen , sitzplaetze);
            frmPlatzsuche.ShowDialog();
        }
    }
}
