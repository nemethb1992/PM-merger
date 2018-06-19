using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static PM_Merger.Models.Select_Models;

namespace PM_Merger.Controls
{
    class dbEntities
    {


        SQLiteConnection connSqlite;

        public dbEntities()
        {
            SetupDb();
            SqliteReaderExecute("CREATE TABLE IF NOT EXISTS `folderurl` (`url`	TEXT);");
        }

        private void SetupDb()
        {
            string connectionString = "Data Source = innerDatabase.db";
            connSqlite = new SQLiteConnection(connectionString);
        }
        public bool dbOpen()
        {
            try
            {
                connSqlite.Open();
                return true;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
                return false;
            }
        }
        private bool dbClose()
        {
            try
            {
                connSqlite.Close();
                return true;
            }
            catch (SQLiteException)
            {
                return false;
            }
        }
        public static string innerDataSourceURL = "Data Source = innerDatabase.db";
        public void SqliteQueryExecute(string query)
        {
            if (this.dbOpen() == true)
            {
                var command = connSqlite.CreateCommand();
                //command.CommandText = "CREATE TABLE IF NOT EXISTS 'app' ('username' TEXT);";
                //command.ExecuteNonQuery();
                command.CommandText = query;
                command.ExecuteNonQuery();
                connSqlite.Close();
            }
        }
        public string SqliteReaderExecute(string query)
        {
            string data = "";
            if (this.dbOpen() == true)
            {
                var command = connSqlite.CreateCommand();
                command.CommandText = query;
                SQLiteDataReader sdr = command.ExecuteReader();
                while (sdr.Read())
                {
                    data = sdr.GetValue(0).ToString();
                }
                sdr.Close();
                connSqlite.Close();
            }
            return data;
        }
    }
}
