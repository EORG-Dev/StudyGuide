using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.Models
{
    [DataContract]
    public class C_Einsatz
    {
        [PrimaryKey, AutoIncrement, Unique, IgnoreDataMember]
        public int ID { get; set; }
        #region ED
        [DataMember]
        public long Erstellt { get; set; } = DateTime.Now.Ticks;
        [DataMember]
        public long Datum { get; set; } = DateTime.Today.Ticks;
        public string S_Datum {  get => new DateTime(Datum).ToString("yyyy'.'MM'.'dd"); }
        [Ignore]
        public DateTime DT_Datum { 
            get => new DateTime(Datum);
            set => Datum = value.Ticks;
        }
        [DataMember]
        public string Rettungsmittel { get;set; }  
        [DataMember]
        public string Alarmtext { get; set; }  
        #endregion ED
        #region SSS
        [DataMember]
        public string Einsatzort { get; set; }  
        [DataMember]
        public string Situation { get; set; }
        #endregion SSS
        #region Patient
        [DataMember]
        public object Geschlecht { get; set; } = "";
        [DataMember]
        public int Alter { get; set; } = -1;
        #endregion Patient
        #region Tatigkeiten
        // Hier Liste mit Beobachtungen, dafür neue Klasse
        #endregion Tatigkeiten
        #region Auswertung
        [DataMember]
        public string RDVersorgung { get; set; }
        [DataMember]
        public object EinweisungDurch { get; set; } = "";
        #endregion Auswertung
        #region Sonstiges
        #endregion Sonstiges
        List<int> GetBlob(string s)
        {
            var listi = new List<int>();
            var lists = s?.Split(',').ToList();
            lists?.ForEach(X => listi.Add(Convert.ToInt16(X)));
            return listi;
        }
        string SetBlob(List<int> value)
        {
            if (value != null && value.Count > 0)
            {
                string s = "";
                value.ForEach(X => s += $"{X},");
                return s.Remove(s.Length - 1);
            }
            else
            {
                return null;
            }
        }
    }
    [DataContract]
    public class EinsatzExport
    {
        public EinsatzExport()
        {

        }
        [DataMember]
        public List<C_Einsatz> Entries { get; set; } = new List<C_Einsatz>();
        [DataMember]
        public long Datum { get; set; } = DateTime.Now.Ticks;
        [DataMember]
        public byte[] DBOptions { get; set; }
        [DataMember]
        public string Name { get; set; }
        public static EinsatzExport Load(string FilePath)
        {
            if (!File.Exists(FilePath))
            {
                throw new Exception("Die Datei konnte nicht geladen werden.");
            }
            using (FileStream reader = new FileStream(FilePath, FileMode.Open, FileAccess.ReadWrite))
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(EinsatzExport));
                var data = (EinsatzExport)ser.ReadObject(reader);
                return data;
            }
        }
        public void Save(string filepath = "")
        {
            using (FileStream writer = new FileStream(filepath, FileMode.Create, FileAccess.Write))
            {
                DataContractSerializer ser = new DataContractSerializer(typeof(EinsatzExport));
                ser.WriteObject(writer, this ?? throw new ArgumentNullException("Die Datei konnte nicht gespeichert werden."));
            }
        }
    }
}
