using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PM_Merger.Models.Select_Models;

namespace PM_Merger.Controls
{
    class Select_Control
    {
        dbEntities dbE = new dbEntities();
        public List<Article_Struct> Article_Datasource()
        {
            List<Article_Struct> list = new List<Article_Struct>();
            try
            {
                list = dbE.Article_SelectAll_Sqlite("select id, nev, tipus from article");
            }
            catch (Exception)
            {
                dbE.SqliteQueryExecute("CREATE TABLE IF NOT EXISTS `article` (`id`	INTEGER PRIMARY KEY AUTOINCREMENT,`nev`	TEXT,`tipus`	INTEGER DEFAULT 00); ");
                list = dbE.Article_SelectAll_Sqlite("select id, nev, tipus from article");
            }
            return list;
        }
        public void WriteRememberedUser(string username)
        {
            dbE.SqliteQueryExecute("DELETE FROM 'app';");
            dbE.SqliteQueryExecute("INSERT INTO 'app' (username) VALUES ('" + username + "');");
        }
        public void DeleteRememberedUser()
        {
            dbE.SqliteQueryExecute("DELETE FROM 'app';");
        }
    }
}
