using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PM_Merger.Models
{
    class Select_Models
    {
        public class Article_Struct
        {
            public int id { get; set; }
            public string nev { get; set; }
            public string path { get; set; }
            public bool Checked { get; set; }
        }
    }
}
