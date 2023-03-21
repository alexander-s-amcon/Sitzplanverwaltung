using Aufgabe_1.Datenbankmethoden;
using Aufgabe_1.Interfaces.Datenbankmethoden;
using Aufgabe_1.Model;
using C1.C1Preview.Export;
using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace Aufgabe_1.View
{
    public partial class frmPlatzsuche : Form
    {
        const string connectionString = "Data Source = Datenbank.sqlite;";
        private SQLiteConnection db_Connection = null;
        private List<Sitzplatz> _sitzplatzliste;
        public CellStyle WhiteCellStyle;
        string eventname;
        int reihen;
        int sitzplaetze;
        CellStyle c1;
        CellStyle c2;
        CellStyle c3;
        CellStyle c4;
        CellStyle c5;

        public frmPlatzsuche(string EventName, int Reihen, int Sitzplaetze)
        {
            InitializeComponent();
            db_Connection = new SQLiteConnection();
            db_Connection.ConnectionString = connectionString;

            c1 = gridPlatzsuche2.Styles.Add("Freier Platz");
            c2 = gridPlatzsuche2.Styles.Add("Ausgewählt");
            c3 = gridPlatzsuche2.Styles.Add("Grün");
            c4 = gridPlatzsuche2.Styles.Add("Reserviert");
            c5 = gridPlatzsuche2.Styles.Add("Blau");
            c1.BackColor = Color.White;
            c2.BackColor = Color.Gold;
            c3.BackColor = Color.LightGreen;
            c4.BackColor = Color.Gray;
            c5.BackColor = Color.Blue;


            gridPlatzsuche2.Styles.Normal.Border.Style = BorderStyleEnum.Flat;
            gridPlatzsuche2.Styles.Normal.Border.Color = Color.DarkGray;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            nudTickets.Value = 1;
            eventname = EventName;
            sitzplaetze = Sitzplaetze;
            reihen = Reihen;
            gridPlatzsuche2.Cols.Count = sitzplaetze;
            gridPlatzsuche2.Rows.Count = reihen;

            for (int i = 0; i < gridPlatzsuche2.Cols.Count; i++)
            {
                gridPlatzsuche2.Cols[i].Visible = false;
            }
            for (int i = 1; i < sitzplaetze; i++)
            {
                gridPlatzsuche2.Cols[i].Width = 35;
                gridPlatzsuche2.Cols[i].Caption = i.ToString();
            }
        }
        private void frmPlatzsuche_Load(object sender, EventArgs e)
        {
            LadeDatenbankeintraege();
        }

        public void LadeDatenbankeintraege()
        {
            try
            {
                for (int i = 0; i < gridPlatzsuche2.Cols.Count; i++)
                {
                    gridPlatzsuche2.Cols[i].Visible = true;
                }
                db_Connection.Open();
                SQLiteCommand sql_Command = new SQLiteCommand();
                sql_Command = db_Connection.CreateCommand();
                sql_Command.CommandText = $"SELECT * FROM [{eventname}]";
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

                for (int i = gridPlatzsuche2.Rows.Fixed; i < gridPlatzsuche2.Rows.Count; i++)
                {
                    gridPlatzsuche2[i, 0] = i.ToString();
                }

                for (int rows = 1; rows < gridPlatzsuche2.Rows.Count; rows++)
                {
                    for (int cols = 1; cols < gridPlatzsuche2.Cols.Count; cols++)
                    {
                        Sitzplatz sitzplatz = _sitzplatzliste.Where(x => x.Reihe == rows && x.Spalte == cols).FirstOrDefault();
                        string Zustand = sitzplatz.Zustand;

                        if (Zustand == "Freier Platz" || Zustand == "")
                        {
                            gridPlatzsuche2.SetCellStyle(rows, cols, c1);
                        }
                        else if (Zustand == "Reserviert" || Zustand == "Platzhalter")
                        {
                            gridPlatzsuche2.SetCellStyle(rows, cols, c4);
                        }
                    }
                }
                db_Connection.Close();
            }
            catch (Exception) { }
        }

        private void SitzplaetzeAnzeigen()
        {
            int tickets = (int)nudTickets.Value;
            if (tickets <= 0)
            {
                return;
            }
            if (cbZusammen.Checked)
            {
                ErmittleZusammenhaengendeSitzplaetze(tickets);
            }
            else
            {
                ErmittleUnzusammenhaengendeSitzplaetze(tickets);
            }
        }

        private int NaechsterPlatz(int sitzplatz)
        {
            int sitzplaetze = gridPlatzsuche2.Cols.Count;
            int mid = sitzplaetze / 2;
            if (sitzplatz <= mid)
            {
                sitzplatz = sitzplatz + ((mid - sitzplatz) * 2) + 1;
                return sitzplatz;
            }
            else if (sitzplatz > mid)
            {
                sitzplatz = sitzplatz - ((sitzplatz - mid) * 2);
                return sitzplatz;
            }
            return sitzplatz;
        }

        private void ErmittleZusammenhaengendeSitzplaetze(int tickets)
        {
            bool platzcounterGleichTickets = false;
            for (int reihe = 1; reihe < gridPlatzsuche2.Rows.Count; reihe++)
            {
                if (platzcounterGleichTickets)
                {
                    break;
                }
                int platzcounter = 0;
                for (int sitzplatz = 1; sitzplatz < gridPlatzsuche2.Cols.Count; sitzplatz++)
                {
                    if (gridPlatzsuche2.GetCellStyle(reihe, sitzplatz).BackColor == Color.White)
                    {
                        platzcounter++;
                        gridPlatzsuche2.SetCellStyle(reihe, sitzplatz, c2);
                    }
                    if (platzcounter == tickets)
                    {
                        platzcounterGleichTickets = true;
                        break;
                    }
                    if (gridPlatzsuche2.GetCellStyle(reihe, sitzplatz).BackColor == Color.Gray || sitzplatz == gridPlatzsuche2.Cols.Count - 1)
                    {
                        for (int r = 1; r <= reihe; r++)
                        {
                            for (int c = 1; c < gridPlatzsuche2.Cols.Count; c++)
                            {
                                if (gridPlatzsuche2.GetCellStyle(r, c).BackColor == Color.Gold)
                                {
                                    gridPlatzsuche2.SetCellStyle(r, c, c1);
                                }
                            }
                        }
                        platzcounter = 0;
                    }
                }
            }
        }



        private void ErmittleUnzusammenhaengendeSitzplaetze(int tickets)
        {
            int mid = sitzplaetze / 2;
            int reihe = 1;
            int spalte = mid;

            for (int anzahlAktuellesTicket = 0; anzahlAktuellesTicket < tickets; anzahlAktuellesTicket++)
            {
                while (reihe < gridPlatzsuche2.Rows.Count)
                {
                    int naechsterFreierPlatz = ErmittleNaechstenFreienPlatz(reihe, spalte);

                    bool hatReiheFreienPlatz = naechsterFreierPlatz > 0;
                    if (hatReiheFreienPlatz)
                    {
                        gridPlatzsuche2.SetCellStyle(reihe, naechsterFreierPlatz, c2);
                        spalte = naechsterFreierPlatz;
                        break;
                    }
                    else
                    {
                        reihe++;
                        spalte = mid;
                    }
                }
            }
        }

        private int ErmittleNaechstenFreienPlatz(int reihe, int spalte)
        {
            for (int aktuelleSpalte = spalte; aktuelleSpalte < gridPlatzsuche2.Cols.Count && aktuelleSpalte > 0; aktuelleSpalte = BerrechneNaechstenPlatz(aktuelleSpalte))
            {
                if (gridPlatzsuche2.GetCellStyle(reihe, aktuelleSpalte).BackColor == Color.White)
                {
                    return aktuelleSpalte;
                }
            }
            return -1;
        }

        private int BerrechneNaechstenPlatz(int aktuelleSpalte)
        {
            int spalte = sitzplaetze / 2;
            if (aktuelleSpalte <= spalte)
            {
                aktuelleSpalte = aktuelleSpalte + ((spalte - aktuelleSpalte) * 2) + 1;
                return aktuelleSpalte;
            }
            else if (aktuelleSpalte > spalte)
            {
                aktuelleSpalte = aktuelleSpalte - ((aktuelleSpalte - spalte) * 2);
                return aktuelleSpalte;
            }
            return aktuelleSpalte;
        }

        private void btnAnzeigen_Click(object sender, EventArgs e)
        {
            LadeDatenbankeintraege();
            SitzplaetzeAnzeigen();
        }

        private void btnReservieren_Click(object sender, EventArgs e)
        {
            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();

            for (int rows = 1; rows < gridPlatzsuche2.Rows.Count; rows++)
            {
                for (int cols = 1; cols < gridPlatzsuche2.Cols.Count; cols++)
                {
                    Sitzplatz sitzplatz = _sitzplatzliste.Where(x => x.Reihe == rows && x.Spalte == cols).FirstOrDefault();
                    string Zustand = sitzplatz.Zustand;

                    if (gridPlatzsuche2.GetCellStyle(rows, cols).BackColor == Color.Gold)
                    {
                        sql_Command.CommandText = $"UPDATE [{eventname}] SET Zustand = 'Reserviert' WHERE Reihe = '{rows}' AND Sitzplatz = '{cols}'";
                        sql_Command.ExecuteNonQuery();
                    }
                }
            }
            sql_Command.Dispose();
            db_Connection.Close();
            LadeDatenbankeintraege();
        }
        private void btnAbbrechen_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridPlatzsuche2_Click(object sender, EventArgs e)
        {
            int row = gridPlatzsuche2.RowSel;
            int col = gridPlatzsuche2.ColSel;
            if (gridPlatzsuche2.GetCellStyle(row, col).BackColor == Color.White)
            {
                gridPlatzsuche2.SetCellStyle(row, col, c2);
                return;
            }
            if (gridPlatzsuche2.GetCellStyle(row, col).BackColor == Color.Gold)
            {
                gridPlatzsuche2.SetCellStyle(row, col, c1);
                return;
            }
        }
    }
}
