using DocumentFormat.OpenXml.Bibliography;
using StudyGuide.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyGuide.Models
{
    public class C_Beobachtung
    {
        #region Variablen
        public string ID { get; set; } = SRV_Random.RandomString(4);
        // Zeit
        public long Zeitpunkt { get; set; } = DateTime.Now.TimeOfDay.Ticks;
        public TimeSpan TS_Zeitpunkt
        {
            get
            {
                return new TimeSpan(Zeitpunkt);
            } set
            {
                Zeitpunkt = value.Ticks;
            }
        }
        public string S_Zeitpunkt
        {
            get
            {
                if (Zeitpunkt == 0)
                    return "--:--";
                return new DateTime(Zeitpunkt).ToString("HH:mm");
            }
        }
        public string Phase { get; set; }
        public string ZeitBeschreibung { get; set; }
        // Handlung
        public string A_Akteur { get; set; } = "Akteur";
        public string A_Bemerkung { get; set; }
        // Anlass
        public string B_Akteur{ get; set; } = "Akteur";
        public string B_Bemerkung { get; set; }
        // Anlass
        public string C_Bemerkung { get; set; }
        // Anlass
        public string S_Symbol { get; set; }
        public string S_Bemerkung { get; set; }
        #endregion Variablen

        #region Methodes
        public static List<C_Beobachtung> ListFromBlob(string blob)
        {
            try
            {
                var res = new List<C_Beobachtung>();   
                string[] splits = blob.Split(';');
                foreach (string s in splits)
                {
                    // Split
                    var beob = new C_Beobachtung();
                    string[] beob_splits = s.Split(',');
                    // Add Values
                    beob.ID = beob_splits[0];
                    beob.Zeitpunkt = Convert.ToInt64(beob_splits[1]);
                    beob.Phase = beob_splits[2];
                    beob.ZeitBeschreibung = beob_splits[3];
                    beob.A_Akteur = beob_splits[4];
                    beob.A_Bemerkung = beob_splits[5];
                    beob.B_Akteur = beob_splits[6];
                    beob.B_Bemerkung = beob_splits[7];
                    beob.C_Bemerkung = beob_splits[8];
                    beob.S_Symbol = beob_splits[9];
                    beob.S_Bemerkung = beob_splits[10];
                    // Add to List
                    res.Add(beob);
                }
                return res;
            } catch (Exception ex)
            {
            }
            return new List<C_Beobachtung>();
        }
        public static string BlobFromList(List<C_Beobachtung> values)
        {
            try
            {
                string res = "";
                foreach (var beob in values)
                {
                    string beob_s = "";
                    //
                    beob_s += Cln(beob.ID) + ",";
                    beob_s += Cln(beob.Zeitpunkt.ToString()) + ",";
                    beob_s += Cln(beob.Phase) + ",";
                    beob_s += Cln(beob.ZeitBeschreibung) + ",";
                    beob_s += Cln(beob.A_Akteur) + ",";
                    beob_s += Cln(beob.A_Bemerkung) + ",";
                    beob_s += Cln(beob.B_Akteur) + ",";
                    beob_s += Cln(beob.B_Bemerkung) + ",";
                    beob_s += Cln(beob.C_Bemerkung) + ",";
                    beob_s += Cln(beob.S_Symbol) + ",";
                    beob_s += Cln(beob.S_Bemerkung);
                    //
                    res += beob_s;
                    res += ";";
                }
                int index = res.Length - 1;
                return res.Remove(index);

            } catch (Exception ex)
            {

            }
            return "";
        }
        static string Cln(string str)
        {
            return str?.Replace(',', ' ').Replace(';', ' ') ?? "";
        }
        #endregion Methodes
    }
}
