using ClosedXML.Excel;
using DocumentFormat.OpenXml.Packaging;
using StudyGuide;
using StudyGuide.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace StudyGuide.Services
{
    class SRV_Export
    {
        //filter and public stuff
        public static async Task ExportEntries(List<C_Einsatz> Entries, string Title = "")
        { 
            try
            {
                string FolderPath = Singleton.Instance.FolderPath;
                string FilePath = await CreateSpreadsheet(FolderPath, Entries, Title);

                await Share.RequestAsync(new ShareFileRequest
                {
                    Title = "Wähle, wohin du die Tabelle exportieren möchtest.",
                    File = new ShareFile(FilePath)
                });
            } catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Fehler", "Leider hat der Export der erhobenen Daten nicht geklappt. Versuche es erneut oder wende dich an einen Administrator.", "Ok");
            }
        }
        //create document
        private static async Task<string> CreateSpreadsheet(string FolderPath, List<C_Einsatz> Entries, string Title)
        {
            // Delete Template
            File.Delete(Singleton.Instance.TemplateFilePath);
            // Re-Add Template
            byte[] template = Resources.Ressources.ToolAnalyse_v0;
            if (template?.Length > 0)
            {
                File.WriteAllBytes(Singleton.Instance.TemplateFilePath, template);
            } else
            {
                throw new Exception("Die Vorlage konnte nicht gefunden werden.");
            }

            //init
            var workbook = new XLWorkbook(Singleton.Instance.TemplateFilePath);
            var tempWorksheet = workbook.Worksheets.Worksheet("inkl. A - C");
            tempWorksheet.Name = "Vorlage";
            
            foreach (var entry in Entries)
            {
                // Create by copying template (Auto-Add)
                var worksheet = tempWorksheet.CopyTo($"Beob_{entry.ID}");

                // Edit
                // -- Kopfzeilen ---
                // Spalte Links
                worksheet.Cell("B2").Value = entry.Name;
                worksheet.Cell("B3").Value = entry.Rettungsmittel;
                worksheet.Cell("B4").Value = entry.Standort;
                worksheet.Cell("B5").Value = entry.Alter;
                worksheet.Cell("D5").Value = entry.Geschlecht;
                // Spalte Mitte
                worksheet.Cell("F2").Value = entry.S_Datum;
                worksheet.Cell("F3").Value = entry.ID;
                worksheet.Cell("E4").Value = entry.Situation;
                // Spalte Rechts
                worksheet.Cell("K2").Value = entry.Alarmtext;
                worksheet.Cell("K3").Value = entry.Einsatzort;
                worksheet.Cell("K4").Value = entry.RDVersorgung;
                worksheet.Cell("K5").Value = entry.EinweisungDurch;

                // -- Tabelle ---
                // To Be Added
            }

            // Remove Template
            tempWorksheet.Delete();

            //save
            //filename
            string FilePath = FolderPath + "Export_" + RandomString(3) + "_" + DateTime.Now.ToString("yyyy'-'MM'-'dd");
            if (Title != "" && Title.Length < 48)
            {
                FilePath += "_" + Title;
            }
            FilePath += ".xlsx";
            workbook.SaveAs(FilePath);
            return FilePath;
        }
        public static string RandomString(int length)
        {
            string result = "";
            Random r = new Random();
            for (int i = 0; i < length; i++)
            {
                int cc = (int)r.Next(65, 90);
                result += (char)cc;
            }
            return result;
        }
    }
}
