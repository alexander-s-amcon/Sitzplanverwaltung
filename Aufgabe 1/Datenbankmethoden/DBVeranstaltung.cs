using Aufgabe_1.Interfaces.Datenbankmethoden;
using Aufgabe_1.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aufgabe_1.Datenbankmethoden
{
    public class DBVeranstaltung : IDBVeranstaltung
    {
        int reihen;
        int sitzplaetze;
        const string connectionString = "Data Source = Datenbank.sqlite;";
        private SQLiteConnection db_Connection = new SQLiteConnection();
        string Saalname;
        public DBVeranstaltung()
        {
            db_Connection.ConnectionString = connectionString;
        }

        public void AddVeranstaltung(Veranstaltungen veranstaltung)
        {
            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();
            sql_Command.CommandText = $"INSERT INTO Veranstaltungen(Name, Saal, Datum) VALUES ('{veranstaltung.Name}','{veranstaltung.Saal}','{veranstaltung.DatumVon + " - " + veranstaltung.DatumBis}')";
            sql_Command.ExecuteNonQuery();
            sql_Command.CommandText = $"CREATE TABLE '{veranstaltung.Name}'(Id INTEGER PRIMARY KEY AUTOINCREMENT, Reihe INTEGER, Sitzplatz INTEGER, Zustand TEXT)";
            sql_Command.ExecuteNonQuery();
            IDBSaal dBSaal = new DBSaal();
            Saele saal = new Saele();
            sql_Command.CommandText = $"SELECT Reihen, Sitzplaetze FROM Saele WHERE Saalname = '{veranstaltung.Saal}'";
            SQLiteDataReader reader;
            reader = sql_Command.ExecuteReader();
            while (reader.Read())
            {
                reihen = (int)reader.GetInt32(0);
                sitzplaetze = (int)reader.GetInt32(1);
            }
            reader.Close();
            for (int rows = 1; rows < reihen; rows++)
            {
                for (int cols = 1; cols < sitzplaetze; cols++)
                {
                    sql_Command.CommandText = $"INSERT INTO {veranstaltung.Name} (Reihe, Sitzplatz, Zustand) VALUES ('{rows} ', '{cols}', '')";
                    sql_Command.ExecuteNonQuery();
                }
            }
            sql_Command.Dispose();
            db_Connection.Close();
        }

        public void DeleteVeranstaltung(Veranstaltungen veranstaltung)
        {
            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();
            sql_Command.CommandText = $"DELETE FROM Veranstaltungen WHERE Id = {veranstaltung.Id}";
            sql_Command.ExecuteNonQuery();
            sql_Command.Dispose();
            db_Connection.Close();
        }

        public void EditVeranstaltung(Veranstaltungen veranstaltung)
        {
            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();
            sql_Command.CommandText = $"UPDATE Veranstaltungen SET Name = '{veranstaltung.Name}', Saal = '{veranstaltung.Saal}', Datum = '{veranstaltung.DatumVon + " - " + veranstaltung.DatumBis}' WHERE Id = {veranstaltung.Id}";
            sql_Command.ExecuteNonQuery();
            sql_Command.Dispose();
            db_Connection.Close();
            return;
        }

        public List<Veranstaltungen> LadeVeranstaltung()
        {
            db_Connection = new SQLiteConnection();
            db_Connection.ConnectionString = connectionString;
            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();
            sql_Command.CommandText = $"SELECT * FROM Veranstaltungen WHERE Saal = 'saal.Saalname'";
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

            return veranstaltungsliste;
        }
    }
}
