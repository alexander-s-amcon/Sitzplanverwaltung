﻿using Aufgabe_1.Interfaces.Datenbankmethoden;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aufgabe_1.Datenbankmethoden
{
    public class DBSaal : IDBSaal
    {
        const string connectionString = "Data Source = Datenbank.sqlite;";
        private SQLiteConnection db_Connection = null;

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
                saalliste.Add(Saal);
            }
            sql_Command.Dispose();
            db_Connection.Close();

            return saalliste;
        }
    }
}
