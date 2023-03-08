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

namespace Aufgabe_1
{
    public partial class frmMain : Form
    {
        const string connectionString = "Data Source = Datenbank.sqlite;";
        private SQLiteConnection db_Connection = null;
        public CellStyle WhiteCellStyle;
        public int test;
        int reihen;
        int sitzplaetze;
        const int ANZAHL_REIHEN = 11;
        const int ANZAHL_SITZPLAETZE = 10;
        private string EventName;
        private List<Sitzplatz> _sitzplatzliste;
        public frmMain()
        {
            InitializeComponent();
            gridSaal.AllowEditing = false;
            gridSaal.Rows.Fixed = 0;
            gridSaal.Cols.Fixed = 0;
            db_Connection = new SQLiteConnection();
            db_Connection.ConnectionString = connectionString;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void LadeSaele()
        {
            IDBSaal dBSaal = new DBSaal();
            List<Saele> saalliste = dBSaal.LadeSaal();
            gridSaal.DataSource = saalliste;
            gridSaal.Cols[0].Visible = false;
            gridSaal.Cols[2].Visible = false;
            gridSaal.Cols[3].Visible = false;
            gridSaal.Cols[1].Width = 160;
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
                veranstaltung.DatumVon = reader.GetString(3);
                veranstaltungsliste.Add(veranstaltung);
            }
            sql_Command.Dispose();
            db_Connection.Close();
            gridVeranstaltungen.DataSource = veranstaltungsliste;
            gridVeranstaltungen.Cols[0].Visible = false;
            gridVeranstaltungen.Cols[1].Visible = false;
            gridVeranstaltungen.Cols[3].Width = 40;
        }

        public void LadeDatenbankeintraege()
        {
            IDBVeranstaltung dBVeranstaltung = new DBVeranstaltung();
            Veranstaltungen veranstaltung = new Veranstaltungen();
            int rowsel = gridVeranstaltungen.RowSel;
            EventName = (string)gridVeranstaltungen.GetData(rowsel, 2);
            veranstaltung.Name = EventName;
            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();
            sql_Command.CommandText = $"SELECT * FROM {EventName}";
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

        private void CreateVeranstaltung()
        {
            try
            {
                frmEvent frmEvent = new frmEvent();
                IDBVeranstaltung dBVeranstaltung = new DBVeranstaltung();
                Veranstaltungen veranstaltung = new Veranstaltungen();
                dBVeranstaltung.AddVeranstaltung(veranstaltung);
                db_Connection.Open();
                SQLiteCommand sql_Command = new SQLiteCommand();
                sql_Command = db_Connection.CreateCommand();
                sql_Command.CommandText = $"CREATE TABLE {veranstaltung.Name}(Id INTEGER PRIMARY KEY AUTOINCREMENT, Reihe INTEGER, Sitzplatz INTEGER, Zustand TEXT);";
                sql_Command.ExecuteNonQuery();
                db_Connection.Close();
                db_Connection.Open();
                sql_Command = db_Connection.CreateCommand();
                sql_Command.CommandText = $"SELECT COUNT(*) FROM {veranstaltung.Name};";
                long count = (long)sql_Command.ExecuteScalar();
                sql_Command.ExecuteNonQuery();
                db_Connection.Close();

                db_Connection.Open();

                if (count < 90)
                {
                    for (int rows = 1; rows < ANZAHL_REIHEN; rows++)
                    {
                        for (int cols = 1; cols < ANZAHL_SITZPLAETZE; cols++)
                        {
                            sql_Command.CommandText = $"INSERT INTO {veranstaltung.Name} (Reihe, Sitzplatz, Zustand) VALUES ('{rows} ', '{cols}', '');";
                            sql_Command.ExecuteNonQuery();
                        }
                    }
                }
                sql_Command.Dispose();
                db_Connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Data.ToString());
            }
        }

        private void CreateSaal()
        {
            try
            {
                frmSaal frmSaal = new frmSaal();
                IDBSaal dBSaal = new DBSaal();
                Saele saal = new Saele();
                frmSaal.Zeige(ref saal);
                dBSaal.AddSaal(saal);
                SQLiteCommand sql_Command = new SQLiteCommand();
                db_Connection.Open();
                sql_Command = db_Connection.CreateCommand();
                sql_Command.CommandText = $"INSERT INTO Saele (Saalname, Reihen, Sitzplaetze) VALUES ('{saal.Saalname}','rows ', 'cols')";
                sql_Command.ExecuteNonQuery();
                sql_Command.Dispose();
                db_Connection.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Data.ToString());
            }
        }

        private void SetWhite()
        {
            WhiteCellStyle = c1FlexGrid1.Styles.Add("Default");
            WhiteCellStyle.BackColor = Color.White;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            CreateDataBase();
            //CreateTables();
            LadeSaele();
            SetWhite();
            c1FlexGrid1.BackColor = WhiteCellStyle.BackColor;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            gridVeranstaltungen.Cols[0].Visible = false;
            gridVeranstaltungen.Cols[1].Visible = false;
            gridVeranstaltungen.Cols[2].Visible = false;
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
                sql_Command.CommandText = $"UPDATE {eventname} SET Zustand = 'Reserviert' WHERE Reihe = '{row}' AND Sitzplatz = '{col}'";
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
                sql_Command.CommandText = $"UPDATE {eventname} SET Zustand = 'Freier Platz' WHERE Reihe = '{row}' AND Sitzplatz = '{col}'";
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
                sql_Command.CommandText = $"UPDATE {eventname} SET Zustand = 'Platzhalter' WHERE Reihe = '{row}' AND Sitzplatz = '{col}'";
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
                sql_Command.CommandText = $"UPDATE {eventname} SET Zustand = '' WHERE Reihe = '{row}' AND Sitzplatz = '{col}'";
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
            frmSaal.Zeige(ref saal);
            dBSaal.AddSaal(saal);
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
            frmSaal.Zeige(ref saal);
            dBSaal.EditSaal(saal);
            LadeSaele();
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
            LadeVeranstaltungen();
        }

        private void btnVeranstaltungAdd_Click(object sender, EventArgs e)
        {
            frmEvent frmEvent = new frmEvent();
            IDBVeranstaltung dBVeranstaltung = new DBVeranstaltung();
            Veranstaltungen veranstaltung = new Veranstaltungen();
            frmEvent.Zeige(ref veranstaltung);
            dBVeranstaltung.AddVeranstaltung(veranstaltung);
            CreateVeranstaltung();
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
            veranstaltung.DatumVon = (string)gridVeranstaltungen.GetData(rowsel, 4);
            veranstaltung.DatumBis = (string)gridVeranstaltungen.GetData(rowsel, 5);
            frmEvent.Zeige(ref veranstaltung);
            dBVeranstaltung.EditVeranstaltung(veranstaltung);
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
            LadeFlexgrid();
            LadeDatenbankeintraege();
        }
    }
}
