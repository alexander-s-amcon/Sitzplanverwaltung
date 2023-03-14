using Aufgabe_1.Datenbankmethoden;
using Aufgabe_1.Interfaces.Datenbankmethoden;
using Aufgabe_1.Model;
using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        public frmPlatzsuche(string EventName, int Reihen, int Sitzplaetze)
        {
            InitializeComponent();
            gridPlatzsuche.Styles.Normal.Border.Style = BorderStyleEnum.Flat;
            gridPlatzsuche.Styles.Normal.Border.Color = Color.DarkGray;
            nudTickets.Value = 1;
            eventname = EventName;
            db_Connection = new SQLiteConnection();
            db_Connection.ConnectionString = connectionString;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
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
            sql_Command.Dispose();
            db_Connection.Close();

            for (int rows = 0; rows < gridPlatzsuche.Rows.Count; rows++)
            {
                for (int cols = 0; cols < gridPlatzsuche.Cols.Count; cols++)
                {
                    gridPlatzsuche.SetCellStyle(rows, cols, WhiteCellStyle);
                }
            }

            for (int i = gridPlatzsuche.Rows.Fixed; i < gridPlatzsuche.Rows.Count; i++)
            {
                gridPlatzsuche[i, 0] = i.ToString();
            }

            db_Connection.Open();
            CellStyle c1 = gridPlatzsuche.Styles.Add("Reserviert");
            CellStyle c2 = gridPlatzsuche.Styles.Add("Freier Platz");
            CellStyle c3 = gridPlatzsuche.Styles.Add("Platzhalter");
            c1.BackColor = Color.Red;
            c2.BackColor = Color.White;
            c3.BackColor = Color.Gray;

            for (int rows = 1; rows < gridPlatzsuche.Rows.Count; rows++)
            {
                for (int cols = 1; cols < gridPlatzsuche.Cols.Count; cols++)
                {
                    Sitzplatz sitzplatz = _sitzplatzliste.Where(x => x.Reihe == rows && x.Spalte == cols).FirstOrDefault();
                    string Zustand = sitzplatz.Zustand;

                    if (Zustand == "Reserviert" || Zustand == "Platzhalter")
                    {
                        gridPlatzsuche.SetCellStyle(rows, cols, c3);
                    }
                    else if (Zustand == "Freier Platz" || Zustand == "")
                    {
                        gridPlatzsuche.SetCellStyle(rows, cols, c2);
                    }
                }
            }
            db_Connection.Close();
        }

        private void HighlightSitzplätze()
        {
            CellStyle c1 = gridPlatzsuche.Styles.Add("Freier Platz");
            CellStyle c2 = gridPlatzsuche.Styles.Add("Ausgewählt");
            CellStyle c3 = gridPlatzsuche.Styles.Add("Verfügbar");
            CellStyle c4 = gridPlatzsuche.Styles.Add("Reserviert");
            c1.BackColor = Color.White;
            c2.BackColor = Color.Gold;
            c3.BackColor = Color.LightGreen;
            c4.BackColor = Color.Gray;
            int tickets = (int)nudTickets.Value;

            if (cbZusammen.Checked == false)
            {
                int mid = sitzplaetze / 2;
                int right = mid + 1;
                int left = mid - 1;
                bool erfolg = false;
                if (tickets > 0)
                {
                    for (int a = 0; a < tickets; a++)
                    {
                        erfolg = false;
                        for (int rows = 1; rows < gridPlatzsuche.Rows.Count; rows++)
                        {
                            for (int cols = 1; cols < gridPlatzsuche.Cols.Count; cols++)
                            {
                                if (gridPlatzsuche.GetCellStyle(rows, mid).BackColor == Color.White)
                                {
                                    gridPlatzsuche.SetCellStyle(rows, mid, c2);
                                    erfolg = true;
                                    break;
                                }
                                else
                                {
                                    if (left <= 0 && right >= sitzplaetze)
                                    {
                                        break;
                                    }

                                    if ((right - mid) * (right - mid) <= (left - mid) * (left - mid))
                                    {
                                        if (gridPlatzsuche.GetCellStyle(rows, right).BackColor == Color.White)
                                        {
                                            gridPlatzsuche.SetCellStyle(rows, right, c2);
                                            erfolg = true;
                                            right++;
                                            break;
                                        }
                                        else if (gridPlatzsuche.GetCellStyle(rows, right).BackColor == Color.Gray)
                                        {
                                            gridPlatzsuche.SetCellStyle(rows, right + 1, c2);
                                            erfolg = true;
                                            right = right + 2;
                                            break;
                                        }
                                    }

                                    if ((right - mid) * (right - mid) > (left - mid) * (left - mid))
                                    {

                                        if (gridPlatzsuche.GetCellStyle(rows, left).BackColor == Color.White)
                                        {
                                            gridPlatzsuche.SetCellStyle(rows, left, c2);
                                            erfolg = true;
                                            left--;
                                            break;
                                        }

                                        else if (gridPlatzsuche.GetCellStyle(rows, left).BackColor == Color.Gray)
                                        {
                                            gridPlatzsuche.SetCellStyle(rows, left - 1, c2);
                                            erfolg = true;
                                            left = left - 2;
                                            break;
                                        }
                                    }
                                }
                            }
                            if (erfolg == true)
                            {
                                break;
                            }
                        }
                    }
                }
            }
            if (cbZusammen.Checked == true)
            {

            }
        }

        private void btnAnzeigen_Click(object sender, EventArgs e)
        {
            LadeDatenbankeintraege();
            HighlightSitzplätze();
        }

        private void gridPlatzsuche_Click(object sender, EventArgs e)
        {
            int row = gridPlatzsuche.RowSel;
            int col = gridPlatzsuche.ColSel;
            CellStyle c1 = gridPlatzsuche.Styles.Add("Freier Platz");
            CellStyle c2 = gridPlatzsuche.Styles.Add("Ausgewählt");
            c1.BackColor = Color.White;
            c2.BackColor = Color.Gold;
            if (gridPlatzsuche.GetCellStyle(row, col).BackColor == Color.White)
            {
                gridPlatzsuche.SetCellStyle(row, col, c2);
                //db_Connection.Open();
                //SQLiteCommand sql_Command = new SQLiteCommand();
                //sql_Command = db_Connection.CreateCommand();
                //sql_Command.CommandText = $"UPDATE [{eventname}] SET Zustand = 'Reserviert' WHERE Reihe = '{row}' AND Sitzplatz = '{col}'";
                //sql_Command.ExecuteNonQuery();
                //sql_Command.Dispose();
                //db_Connection.Close();
                return;
            }
            if (gridPlatzsuche.GetCellStyle(row, col).BackColor == Color.Gold)
            {
                gridPlatzsuche.SetCellStyle(row, col, c1);
                //db_Connection.Open();
                //SQLiteCommand sql_Command = new SQLiteCommand();
                //sql_Command = db_Connection.CreateCommand();
                //sql_Command.CommandText = $"UPDATE [{eventname}] SET Zustand = 'Freier Platz' WHERE Reihe = '{row}' AND Sitzplatz = '{col}'";
                //sql_Command.ExecuteNonQuery();
                //sql_Command.Dispose();
                //db_Connection.Close();
                return;
            }
        }

        private void btnAbbrechen_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmPlatzsuche_Load(object sender, EventArgs e)
        {
            LadeDatenbankeintraege();
        }
    }
}
