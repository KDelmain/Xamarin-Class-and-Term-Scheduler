using System;
using System.Collections.ObjectModel;
using SQLite;
using System.Linq;
using DelmainC971.Models;
using DelmainC971.Database;
using System.Collections.Generic;

namespace DelmainC971.Models
{
    public class Term
    {
        public static ObservableCollection<Term> TermCollection = new ObservableCollection<Term>();

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public static void PopulateTermCollection()
        {
            var database = new DBHelper();
            database.Initialize();

            var terms = database.GetTerms();

            TermCollection.Clear();

            terms.ForEach(term => TermCollection.Add(term));

            database.Close();
        }

        public static void AddTerm(Term term)
        {
            TermCollection.Add(term);
        }

        public static void UpdateTerm(Term term)
        {
            int termIndex = 0;

            foreach (Term item in TermCollection)
            {
                if (item.ID == term.ID)
                {
                    termIndex = TermCollection.IndexOf(item);
                }
            }

            TermCollection.Insert(termIndex, term);
            TermCollection.RemoveAt(termIndex + 1);

        }

        public static void DeleteTerm(Term term)
        {
            TermCollection.Remove(term);
        }
    }
}
