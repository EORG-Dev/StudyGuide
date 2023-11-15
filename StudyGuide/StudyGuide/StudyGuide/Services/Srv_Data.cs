using StudyGuide.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudyGuide.Services
{
    public static class Srv_Data
    {
        public static void Setup()
        {
            if (!File.Exists(Singleton.Instance.DataFilePath))
            {
                using (SQLite.SQLiteConnection con = new SQLite.SQLiteConnection(Singleton.Instance.DataFilePath))
                {
                    con.CreateTable<C_Einsatz>();
                }
            }
        }
        public static C_Einsatz GetPreset()
        {
            // Get last or return new
            var last = GetLastEinsatz();
            if (last == null)
            {
                return new C_Einsatz();
            }

            // copy certain values from last to new
            var neu = new C_Einsatz();
            neu.Standort = last.Standort;
            neu.Rettungsmittel = last.Rettungsmittel;

            // return new
            return neu;
        }
        #region REST
        public static ObservableCollection<T> GetAll<T>() where T : new()
        {
            using (SQLite.SQLiteConnection con = new SQLite.SQLiteConnection(Singleton.Instance.DataFilePath))
            {
                var res = new ObservableCollection<T>();
                var list = con.Table<T>().ToList();
                list.ForEach(t => res.Add(t));
                return res;
            }
        }
        public static C_Einsatz GetLastEinsatz()
        {
            using (SQLite.SQLiteConnection con = new SQLite.SQLiteConnection(Singleton.Instance.DataFilePath))
            {
                List<C_Einsatz> res = con.Table<C_Einsatz>().OrderBy(X => X.Erstellt).ToList();
                return res.LastOrDefault();
            }
        }
        public static C_Einsatz GetByID(string ID)
        {
            using (SQLite.SQLiteConnection con = new SQLite.SQLiteConnection(Singleton.Instance.DataFilePath))
            {
                var res = con.Query<C_Einsatz>($"SELECT * FROM C_Einsatz WHERE ID = \"{ID}\"");
                return res.FirstOrDefault();
            }
        }
        public static bool InsertAll<T>(List<object> Insert) where T : class
        {
            foreach (object o in Insert)
            {
                Insert<T>(o);
            }
            return true;
        }
        public static bool Insert<T>(object Insert) where T : class
        {
            using (SQLite.SQLiteConnection con = new SQLite.SQLiteConnection(Singleton.Instance.DataFilePath))
            {
                con.Insert(Insert as T);
                return true;
            }
        }
        public static bool InsertOrReplace<T>(object Insert) where T : class
        {
            using (SQLite.SQLiteConnection con = new SQLite.SQLiteConnection(Singleton.Instance.DataFilePath))
            {
                con.InsertOrReplace(Insert as T);
                return true;
            }
        }
        public static bool Remove<T>(object Delete) where T : class
        {
            using (SQLite.SQLiteConnection con = new SQLite.SQLiteConnection(Singleton.Instance.DataFilePath))
            {
                con.Delete(Delete as T);
                return true;
            }
        }
        #endregion REST
        public static List<(long, List<C_Einsatz>)> GroupByDienste()
        {
            using (SQLite.SQLiteConnection con = new SQLite.SQLiteConnection(Singleton.Instance.DataFilePath))
            {
                List<(long, List<C_Einsatz>)> res = new List<(long, List<C_Einsatz>)>();
                var groups = con.Table<C_Einsatz>().GroupBy(X => X.S_Datum);
                foreach (var g in groups.ToList())
                {
                    DateTime dtdate = new DateTime();
                    DateTime.TryParse(g.Key, out dtdate);
                    long date = dtdate.Ticks;
                    List<C_Einsatz> lE = new List<C_Einsatz>();
                    foreach (var gg in g)
                    {
                        lE.Add(gg);
                    }
                    res.Add((date, lE));
                }
                return res;
            }
        }
        public static List<C_Einsatz> GetLastDienst()
        {
            var dienste = GroupByDienste();
            var dienstdate = dienste.Max(X => X.Item1);
            var dienst = dienste.Where(X => X.Item1 == dienstdate).FirstOrDefault();
            return dienst.Item2 ?? null;
        }
    }
}
