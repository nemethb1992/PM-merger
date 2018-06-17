using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PM_Merger.Models.Select_Models;

namespace PM_Merger.Controls
{
    class Select_Control
    {
        dbEntities dbE = new dbEntities();
        public List<Article_Struct> FolderReadOut()
        {
            List<Article_Struct> list = new List<Article_Struct>();
            DirectoryInfo directory = new DirectoryInfo(@"C:\Users\fzbal\Desktop\PDF test");//Assuming Test is your Folder
            FileInfo[] articles = directory.GetFiles("*.pdf"); //Getting Text files
            
            foreach (FileInfo file in articles)
            {
                list.Add(new Article_Struct { id = 0, nev = file.Name, path = file.DirectoryName });
            }
            return list;
        }
    }
}
