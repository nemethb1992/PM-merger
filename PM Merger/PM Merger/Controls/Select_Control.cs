using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static PM_Merger.Models.Select_Models;

namespace PM_Merger.Controls
{
    class Select_Control
    {
        dbEntities dbE = new dbEntities();

        private static string FolderUrls;
        public string FolderUrl { get { return FolderUrls; } set { FolderUrls = value; } }

        public Select_Control()
        {
            FolderUrl = dbE.SqliteReaderExecute("SELECT * FROM folderurl;");
        }


        public List<Article_Struct> FolderReadOut()
        {
            DirectoryInfo directory;
            List<Article_Struct> list = new List<Article_Struct>();
            FileInfo[] articles;
                try
                {
                    directory = new DirectoryInfo(FolderUrl);
                    articles = directory.GetFiles("*.pdf");
                    foreach (FileInfo file in articles)
                    {
                        list.Add(new Article_Struct { id = 0, nev = file.Name, path = file.DirectoryName });
                    }
                }
                catch (Exception)
                {
                }


            return list;
        }

        public string OpenFolderDialog_URL()
        {
            string url = "";
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    url = fbd.SelectedPath;
                    dbE.SqliteQueryExecute("DELETE FROM folderurl");
                    dbE.SqliteQueryExecute("INSERT INTO folderurl (url) VALUES ('" + url + "')");
                    FolderUrl = url;
                }
            }
            return url;
        }
    }
}
