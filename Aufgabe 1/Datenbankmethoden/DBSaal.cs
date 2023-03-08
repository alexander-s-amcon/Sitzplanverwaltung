using Aufgabe_1.Interfaces.Datenbankmethoden;
using Aufgabe_1.Model;
using C1.Win.C1FlexGrid;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aufgabe_1.Datenbankmethoden
{
    public class DBSaal : IDBSaal
    {
        const string connectionString = "Data Source = Datenbank.sqlite;";
        private SQLiteConnection db_Connection = new SQLiteConnection();
        public DBSaal()
        {
            db_Connection.ConnectionString = connectionString;
        }

        public void AddSaal(Saele saal)
        {
            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();
            sql_Command.CommandText = $"INSERT INTO Saele(Saalname, Reihen, Sitzplaetze) VALUES ('{saal.Saalname}','{saal.Reihen}','{saal.Sitzplaetze}')";
            sql_Command.ExecuteNonQuery();
            sql_Command.Dispose();
            db_Connection.Close();
        }

        public void DeleteSaal(Saele saal)
        {
            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();
            sql_Command.CommandText = "DELETE FROM Saele WHERE Id = '" + saal.Id + "'";
            sql_Command.ExecuteNonQuery();
            sql_Command.Dispose();
            db_Connection.Close();
            return;
        }

        public void EditSaal(Saele saal)
        {
            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();
            sql_Command.CommandText = $"UPDATE Saele SET Saalname = '{saal.Saalname}', Reihen = {saal.Reihen}, Sitzplaetze = {saal.Sitzplaetze} WHERE Id = {saal.Id}";
            sql_Command.ExecuteNonQuery();
            sql_Command.Dispose();
            db_Connection.Close();
            return;
        }

        public List<Saele> LadeSaal()
        {
            db_Connection = new SQLiteConnection();
            db_Connection.ConnectionString = connectionString;
            db_Connection.Open();
            SQLiteCommand sql_Command = new SQLiteCommand();
            sql_Command = db_Connection.CreateCommand();
            sql_Command.CommandText = "SELECT * FROM Saele";
            SQLiteDataReader reader = sql_Command.ExecuteReader();
            List<Saele> saalliste = new List<Saele>();
            while (reader.Read())
            {
                Saele Saal = new Saele();
                Saal.Id = reader.GetInt32(0);
                Saal.Saalname = reader.GetString(1);
                Saal.Reihen = reader.GetInt32(2);
                Saal.Sitzplaetze = reader.GetInt32(3);
                saalliste.Add(Saal);
            }
            sql_Command.Dispose();
            db_Connection.Close();
            return saalliste;
        }
    }
}
