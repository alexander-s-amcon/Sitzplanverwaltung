using Aufgabe_1.Datenbankmethoden;
using Aufgabe_1.Interfaces.Datenbankmethoden;
using Aufgabe_1.Model;
using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Net;
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
        public frmPlatzsuche(string EventName, int Reihen, int Sitzplaetze)
        {
            InitializeComponent();
            db_Connection = new SQLiteConnection();
            db_Connection.ConnectionString = connectionString;

            c1 = gridPlatzsuche.Styles.Add("Freier Platz");
            c2 = gridPlatzsuche.Styles.Add("Ausgewählt");
            c3 = gridPlatzsuche.Styles.Add("Verfügbar");
            c4 = gridPlatzsuche.Styles.Add("Reserviert");
            c1.BackColor = Color.White;
            c2.BackColor = Color.Gold;
            c3.BackColor = Color.LightGreen;
            c4.BackColor = Color.Gray;

            gridPlatzsuche.Styles.Normal.Border.Style = BorderStyleEnum.Flat;
            gridPlatzsuche.Styles.Normal.Border.Color = Color.DarkGray;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            nudTickets.Value = 1;
            eventname = EventName;
            sitzplaetze = Sitzplaetze;
            reihen = Reihen;
            gridPlatzsuche.Cols.Count = sitzplaetze;
            gridPlatzsuche.Rows.Count = reihen;

            for (int i = 0; i < gridPlatzsuche.Cols.Count; i++)
            {
                gridPlatzsuche.Cols[i].Visible = false;
            }
            for (int i = 1; i < sitzplaetze; i++)
            {
                gridPlatzsuche.Cols[i].Width = 35;
                gridPlatzsuche.Cols[i].Caption = i.ToString();
            }
        }

        public void LadeDatenbankeintraege()
        {
            try
            {
                for (int i = 0; i < gridPlatzsuche.Cols.Count; i++)
                {
                    gridPlatzsuche.Cols[i].Visible = true;
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

                for (int i = gridPlatzsuche.Rows.Fixed; i < gridPlatzsuche.Rows.Count; i++)
                {
                    gridPlatzsuche[i, 0] = i.ToString();
                }

                for (int rows = 1; rows < gridPlatzsuche.Rows.Count; rows++)
                {
                    for (int cols = 1; cols < gridPlatzsuche.Cols.Count; cols++)
                    {
                        Sitzplatz sitzplatz = _sitzplatzliste.Where(x => x.Reihe == rows && x.Spalte == cols).FirstOrDefault();
                        string Zustand = sitzplatz.Zustand;

                        if (Zustand == "Freier Platz" || Zustand == "")
                        {
                            gridPlatzsuche.SetCellStyle(rows, cols, c1);
                        }
                        else if (Zustand == "Reserviert" || Zustand == "Platzhalter")
                        {
                            gridPlatzsuche.SetCellStyle(rows, cols, c4);
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

        private void ErmittleZusammenhaengendeSitzplaetze(int tickets)
        {
            int mid = sitzplaetze / 2;
            int reihe = 1;
            int spalte = mid;

            for (int anzahlAktuellesTicket = 0; anzahlAktuellesTicket < tickets; anzahlAktuellesTicket++)
            {
                while (reihe < gridPlatzsuche.Rows.Count)
                {
                    int naechsterFreierPlatz = ErmittleNaechstenFreienPlatz(reihe, spalte);

                    bool hatReiheFreienPlatz = naechsterFreierPlatz > 0;
                    if (hatReiheFreienPlatz)
                    {
                        gridPlatzsuche.SetCellStyle(reihe, naechsterFreierPlatz, c2);
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

        private void ErmittleUnzusammenhaengendeSitzplaetze(int tickets)
        {
            int mid = sitzplaetze / 2;
            int reihe = 1;
            int spalte = mid;

            for (int anzahlAktuellesTicket = 0; anzahlAktuellesTicket < tickets; anzahlAktuellesTicket++)
            {
                while (reihe < gridPlatzsuche.Rows.Count)
                {
                    int naechsterFreierPlatz = ErmittleNaechstenFreienPlatz(reihe, spalte);

                    bool hatReiheFreienPlatz = naechsterFreierPlatz > 0;
                    if (hatReiheFreienPlatz)
                    {
                        gridPlatzsuche.SetCellStyle(reihe, naechsterFreierPlatz, c2);
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
            for (int aktuelleSpalte = spalte; aktuelleSpalte < gridPlatzsuche.Cols.Count && aktuelleSpalte > 0; aktuelleSpalte = BerrechneNaechstenPlatz(aktuelleSpalte))
            {
                if (gridPlatzsuche.GetCellStyle(reihe, aktuelleSpalte).BackColor == Color.White)
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

        private void frmPlatzsuche_Load(object sender, EventArgs e)
        {
            LadeDatenbankeintraege();
        }

        private void btnAnzeigen_Click(object sender, EventArgs e)
        {
            LadeDatenbankeintraege();
            SitzplaetzeAnzeigen();
        }

        private void gridPlatzsuche_Click(object sender, EventArgs e)
        {
            int row = gridPlatzsuche.RowSel;
            int col = gridPlatzsuche.ColSel;
            if (gridPlatzsuche.GetCellStyle(row, col).BackColor == Color.White)
            {
                gridPlatzsuche.SetCellStyle(row, col, c2);
                return;
            }
            if (gridPlatzsuche.GetCellStyle(row, col).BackColor == Color.Gold)
            {
                gridPlatzsuche.SetCellStyle(row, col, c1);
                return;
            }
        }

        private void btnReservieren_Click(object sender, EventArgs e)
        {
            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();

            for (int rows = 1; rows < gridPlatzsuche.Rows.Count; rows++)
            {
                for (int cols = 1; cols < gridPlatzsuche.Cols.Count; cols++)
                {
                    Sitzplatz sitzplatz = _sitzplatzliste.Where(x => x.Reihe == rows && x.Spalte == cols).FirstOrDefault();
                    string Zustand = sitzplatz.Zustand;

                    if (gridPlatzsuche.GetCellStyle(rows, cols).BackColor == Color.Gold)
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
    }
}
