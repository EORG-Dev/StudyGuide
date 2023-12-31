﻿using SQLite;
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
        public string Standort { get; set; }

        #endregion ED
        #region SSS
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Alarmtext { get; set; }
        [DataMember]
        public string Einsatzort { get; set; }  
        [DataMember]
        public string Situation { get; set; }
        #endregion SSS
        #region Patient
        [DataMember]
        public string Geschlecht { get; set; } = "";
        [DataMember]
        public int Alter { get; set; } = -1;
        [Ignore]
        public string AlterGeschlecht
        {
            get
            {
                return Geschlecht.ToUpper() + Alter.ToString();
            }
        }
            
        #endregion Patient
        #region Tatigkeiten
        // Hier Liste mit Beobachtungen, dafür neue Klasse
        [DataMember]
        public string Beobachtungen_Blob { get; set; }
        [Ignore]
        public List<C_Beobachtung> Beobachtungen
        {
            get
            {
                return C_Beobachtung.ListFromBlob(Beobachtungen_Blob);
            } 
            set
            {
                Beobachtungen_Blob = C_Beobachtung.BlobFromList(value);
            }
        }
        [Ignore]
        public int Beobachtungen_Count
        {
            get
            {
                return Beobachtungen.Count;
            }
        }
        #endregion Tatigkeiten
        #region Auswertung
        [DataMember]
        public string RDVersorgung { get; set; }
        [DataMember]
        public string EinweisungDurch { get; set; } = "";
        
        #endregion Auswertung
        #region Sonstiges
        #endregion Sonstiges
        /*List<string> GetBlob(string s)
        {
            var listi = new List<string>();
            var lists = s?.Split(';').ToList();
            lists?.ForEach(X => listi.Add(X));
            return listi;
        }
        string SetBlob(List<string> value)
        {
            if (value != null && value.Count > 0)
            {
                foreach (var item in value)
                {
                    item.Replace(';', ' ');
                }
                string s = "";
                value.ForEach(X => s += $"{X};");
                return s.Remove(s.Length - 1);
            }
            else
            {
                return null;
            }
        }*/
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
