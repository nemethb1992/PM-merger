using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using static PM_Merger.Models.Select_Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Diagnostics;

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
                        list.Add(new Article_Struct { id = 0, nev =file.Name.Split('.')[0], path = file.FullName, Checked = false });
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
        public void OpenExplorer()
        {
            Process.Start(FolderUrl);
        }
        public void OpenPDF(string url)
        {
            Process.Start(url);
        }
        public List<byte[]> FileReadIn(List<string> li)
        {
            List<byte[]> list = new List<byte[]>();
            foreach (var item in li)
            {
                list.Add(File.ReadAllBytes(item));
            }
            return list;
        }
            public byte[] PDF_Merger(List<string> li, string PrintedName)
            {
            List<byte[]> sourceFiles = FileReadIn(li);
                Document document = new Document();
                using (MemoryStream ms = new MemoryStream())
                {
                    PdfCopy copy = new PdfCopy(document, ms);
                    document.Open();
                    int documentPageCounter = 0;

                    // Iterate through all pdf documents
                    for (int fileCounter = 0; fileCounter < sourceFiles.Count; fileCounter++)
                    {
                        // Create pdf reader
                        PdfReader reader = new PdfReader(sourceFiles[fileCounter]);
                        int numberOfPages = reader.NumberOfPages;

                        // Iterate through all pages
                        for (int currentPageIndex = 1; currentPageIndex <= numberOfPages; currentPageIndex++)
                        {
                            documentPageCounter++;
                            PdfImportedPage importedPage = copy.GetImportedPage(reader, currentPageIndex);
                            PdfCopy.PageStamp pageStamp = copy.CreatePageStamp(importedPage);

                            // Write header
                            ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
                                new Phrase("PDF Merger by Helvetic Solutions"), importedPage.Width / 2, importedPage.Height - 30,
                                importedPage.Width < importedPage.Height ? 0 : 1);

                            // Write footer
                            ColumnText.ShowTextAligned(pageStamp.GetOverContent(), Element.ALIGN_CENTER,
                                new Phrase(String.Format("Page {0}", documentPageCounter)), importedPage.Width / 2, 30,
                                importedPage.Width < importedPage.Height ? 0 : 1);

                            pageStamp.AlterContents();

                            copy.AddPage(importedPage);
                        }

                        copy.FreeReader(reader);
                        reader.Close();
                    }

                    document.Close();
                if (PrintedName == "")
                {
                    PrintedName = "PM Merge Document ";
                }
                DateTime thisDay = DateTime.Today;
                System.IO.Directory.CreateDirectory(FolderUrl+"\\Merged Documents");
                System.IO.File.WriteAllBytes(FolderUrl+ "\\Merged Documents\\" + PrintedName +" ("+ thisDay.ToString("yyyy.MM.dd") + ").pdf", ms.GetBuffer());
                Process.Start(FolderUrl + "\\Merged Documents");

                return ms.GetBuffer();
                }
            }
        }

    }
    

