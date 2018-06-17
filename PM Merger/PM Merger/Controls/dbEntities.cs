using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PM_Merger.Models.Select_Models;

namespace PM_Merger.Controls
{
    class dbEntities
    {
        public static string innerDataSourceURL = "Data Source = innerDatabase.db";
        public void SqliteQueryExecute(string query)
        {
            SQLiteConnection connSqlite = new SQLiteConnection(innerDataSourceURL);
            var command = connSqlite.CreateCommand();
            connSqlite.Open();
            //command.CommandText = "CREATE TABLE IF NOT EXISTS 'app' ('username' TEXT);";
            //command.ExecuteNonQuery();
            command.CommandText = query;
            command.ExecuteNonQuery();
            connSqlite.Close();
        }
        public string SqliteReaderExecute(string query)
        {
            SQLiteConnection connSqlite = new SQLiteConnection(innerDataSourceURL);
            connSqlite.Open();
            var command = connSqlite.CreateCommand();
            command.CommandText = query;
            SQLiteDataReader sdr = command.ExecuteReader();
            string data = "";
            while (sdr.Read())
            {
                data = sdr.GetValue(0).ToString();
            }
            sdr.Close();
            connSqlite.Close();
            return data;
        }
    }
}
