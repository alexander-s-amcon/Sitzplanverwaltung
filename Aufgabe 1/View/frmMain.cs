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

namespace Aufgabe_1
{
    public partial class frmMain : Form
    {
        const string connectionString = "Data Source = Datenbank.sqlite;";
        private SQLiteConnection db_Connection = null;
        public CellStyle WhiteCellStyle;
        const int ANZAHL_REIHEN = 11;
        const int ANZAHL_SITZPLAETZE = 10;
        private string SaalName;
        private List<Sitzplatz> _sitzplatzliste;
        string hallo;

        public frmMain()
        {
            InitializeComponent();
            dataGridView1.ReadOnly = true;

            db_Connection = new SQLiteConnection();
            db_Connection.ConnectionString = connectionString;
        }

        private void LadeSaele()
        {
            IDBSaal dBSaal = new DBSaal();
            List <Saele> saalliste = dBSaal.LadeSaal();
            dataGridView1.DataSource = saalliste;
            DataGridViewColumn column = dataGridView1.Columns[0];
            column.Width = 20;
            DataGridViewColumn column2 = dataGridView1.Columns[1];
            column2.Width = 167;
            this.dataGridView1.ClearSelection();
        }

        public void LadeDatenbankeintraege()
        {
            SaalName = "Sitzplatz";
            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();
            sql_Command.CommandText = "SELECT * FROM " + SaalName +"";
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

        private void CreateTables()
        {
            try
            {
                db_Connection.Open();
                SQLiteCommand sql_Command = new SQLiteCommand();
                sql_Command = db_Connection.CreateCommand();
                sql_Command.CommandText = "CREATE TABLE IF NOT EXISTS Sitzplatz(Id INTEGER PRIMARY KEY AUTOINCREMENT, Reihe INTEGER, Sitzplatz INTEGER, Zustand TEXT);";
                db_Connection.Close();

                db_Connection.Open();
                sql_Command = db_Connection.CreateCommand();
                sql_Command.CommandText = "SELECT COUNT(*) FROM Sitzplatz;";
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
                            sql_Command.CommandText = "INSERT INTO Sitzplatz (Reihe, Sitzplatz, Zustand) VALUES ('" + rows + "', '" + cols + "', '');";
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

        private void CreateDropDown()
        {
            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();
            sql_Command.CommandText = "SELECT name FROM sqlite_master WHERE type = 'table' ORDER BY 1";
            SQLiteDataReader reader = sql_Command.ExecuteReader();
            List<Tables> tablelist = new List<Tables>();

            while (reader.Read())
            {
                Tables tables = new Tables();
                tables.Table = reader.ToString();
                tablelist.Add(tables);
            }
            sql_Command.Dispose();
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
            CreateTables();
            CreateDropDown();
            LadeDatenbankeintraege();
            LadeSaele();
            SetWhite();
            c1FlexGrid1.BackColor = WhiteCellStyle.BackColor;
            FormBorderStyle = FormBorderStyle.FixedSingle;


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

            if (c1FlexGrid1.GetCellStyle(row, col).Name == WhiteCellStyle.Name)
            {
                c1FlexGrid1.SetCellStyle(row, col, c1);
                db_Connection.Open();
                SQLiteCommand sql_Command = new SQLiteCommand();
                sql_Command = db_Connection.CreateCommand();
                sql_Command.CommandText = "UPDATE Sitzplatz SET Zustand = 'Reserviert' WHERE Reihe = '" + row + "' AND Sitzplatz = '" + col + "'";
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
                sql_Command.CommandText = "UPDATE Sitzplatz SET Zustand = 'Freier Platz' WHERE Reihe = '" + row + "' AND Sitzplatz = '" + col + "'";
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
                sql_Command.CommandText = "UPDATE Sitzplatz SET Zustand = 'Platzhalter' WHERE Reihe = '" + row + "' AND Sitzplatz = '" + col + "'";
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
                sql_Command.CommandText = "UPDATE Sitzplatz SET Zustand = '' WHERE Reihe = '" + row + "' AND Sitzplatz = '" + col + "'";
                sql_Command.ExecuteNonQuery();
                sql_Command.Dispose();
                db_Connection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmEvent fEvent = new frmEvent();
            fEvent.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmSaal fSaal = new frmSaal();
            fSaal.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            LadeDatenbankeintraege();
        }
    }
}
