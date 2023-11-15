using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Proj_AzubiGuide.Models
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
        public int RW { get; set; }  
        public bool IsRW 
        { 
            get 
            {
                return RW != 555;
            } 
        }
        public bool IsNotRW
        {
            get
            {
                return !IsRW;
            }
        }

        [DataMember]
        public int Rettungsmittel { get;set; }  
        [DataMember]
        public int Stichwort_RM { get; set; }  
        [DataMember]
        public int Stichwort { get; set; }  
        /*public string S_StichwortGesamt
        {
            get => Srv_Options.GetItemName(Stichwort_RM) + "-" + Srv_Options.GetItemName(Stichwort);
        }*/
        [DataMember]
        public int Fehleinsatz { get; set; }
        [DataMember]
        public string Funkrufname { get; set; } = "";
        [DataMember]
        public int FF { get; set; }
        [DataMember]
        public string FF_Name { get; set; } 
        [DataMember] 
        public byte[] Unterschrift { get; set; }
        public bool HasUnterschrift
        {
            get
            {
                return Unterschrift?.Length > 0;
            }
        }
        [DataMember]
        public bool ZumPatMitSoSi { get; set; } = true;
        [DataMember]
        public bool MitNa { get; set; }
        [DataMember]
        public int Schichtart { get; set; }
        #endregion ED
        #region Beteiligung
        [DataMember]
        public bool Telefon { get; set; }
        [DataMember]
        public bool Beifahrer { get; set; }
        [DataMember]
        public bool Anreichen { get; set; }
        [DataMember]
        public bool Diagnostik { get; set; }
        [DataMember]
        public bool Anamnese { get; set; }
        [DataMember]
        public bool TherapieB { get; set; }
        [DataMember]
        public bool Transport { get; set; }
        #endregion Beteiligung
        #region SSS
        [DataMember]
        public int Einsatzort { get; set; }  
        [DataMember]
        public int Situation { get; set; }
        [Ignore]
        public List<int> Safety
        {
            get => GetBlob(SafetyBlobbed);
            set => SafetyBlobbed = SetBlob(value);
        }
        [DataMember]
        public string SafetyBlobbed { get; set; }
        [DataMember]
        public int Tageszeit { get; set; }  
        #endregion SSS
        #region Patient
        [DataMember]
        public int Geschlecht { get; set; }
        [DataMember]
        public int Alter { get; set; } = -1;
        /*public string S_AlterGeschlecht
        {
            get
            {
                return $"{Alter}, {Srv_Options.GetItemName(Geschlecht)}";
            }
        }*/
        [DataMember]
        public string Altersgruppe { get; set; }  
        [DataMember]
        public int Lebenssituation { get; set; }
        //public int AnzahlVorerkrankungen { get; set; } = -1;
        [Ignore]
        public List<int> Diagnosen
        {
            get => GetBlob(DiagnosenBlobbed);
            set => DiagnosenBlobbed = SetBlob(value);
        }
        [DataMember]
        public string DiagnosenBlobbed { get; set; }
        /*public string ErsteDiagnose
        {
            get => Srv_Options.GetItemName(Diagnosen.FirstOrDefault(), "unklar");
        }
        public string ErsteZweiDiagnose
        {
            get
            {
                if (Diagnosen.Count < 2 || Diagnosen == null) return ErsteDiagnose;
                return Srv_Options.GetItemName(Diagnosen[0]) + ", " + Srv_Options.GetItemName(Diagnosen[1]);
            }
        }*/
        [DataMember]
        public int Therapie { get; set; }
        [Ignore]
        public List<int> BesonderM
        {
            get => GetBlob(BesondereMBlobbed);
            set => BesondereMBlobbed = SetBlob(value);
        }
        [DataMember]
        public string BesondereMBlobbed { get; set; }
        [Ignore]
        public List<int> Medikamente
        {
            get => GetBlob(MedikamenteBlobbed);
            set => MedikamenteBlobbed = SetBlob(value);
        }
        [DataMember]
        public string MedikamenteBlobbed { get; set; }
        #endregion Patient
        #region Auswertung
        [DataMember]
        public int NACA { get; set; }
        [DataMember]
        public int RDVersorgung { get; set; }  
        [DataMember]
        public bool TrspMitSoSi { get; set; } = false;
        [DataMember]
        public int NKat { get; set; }
        [Ignore]
        public List<int> Einsatzbesonderheiten
        {
            get => GetBlob(EinsatzbesonderheitenBlobbed);
            set => EinsatzbesonderheitenBlobbed = SetBlob(value);
        }
        [DataMember]
        public string EinsatzbesonderheitenBlobbed { get; set; }

        #endregion Auswertung
        #region Sonstiges
        [DataMember]
        public string Sonstiges { get; set; } = "";
        [DataMember]
        public bool Einsatzbericht { get; set; }
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
