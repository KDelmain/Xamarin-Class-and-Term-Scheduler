using System;
using System.Collections.ObjectModel;
using SQLite;
using System.Linq;
using DelmainC971.Models;
using DelmainC971.Database;
using System.Collections.Generic;

namespace DelmainC971.Models
{
    public class Assessment
    {
        public static ObservableCollection<Assessment> AssessmentCollection = new ObservableCollection<Assessment>();

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Type { get; set; }
        public bool Notifications { get; set; }
        public int CourseID { get; set; }

        public static void PopulateAssessmentCollection()
        {
            var database = new DBHelper();
            database.Initialize();

            var assessments = database.GetAssessments();

            AssessmentCollection.Clear();

            assessments.ForEach(assessment => AssessmentCollection.Add(assessment));

            database.Close();
        }

        public static void PopulateAssessmentCollection(int courseID)
        {
            var database = new DBHelper();
            database.Initialize();

            var assessments = database.GetAssessmentsByCourseID(courseID);

            AssessmentCollection.Clear();

            assessments.ForEach(assessment => AssessmentCollection.Add(assessment));

            database.Close();
        }

        public static void AddAssessment(Assessment assessment)
        {
            AssessmentCollection.Add(assessment);
        }

        public static void UpdateAssessment(Assessment assessment)
        {
            int assessmentIndex = 0;

            foreach (Assessment item in AssessmentCollection)
            {
                if (item.ID == assessment.ID)
                {
                    assessmentIndex = AssessmentCollection.IndexOf(item);
                }
            }

            AssessmentCollection.Insert(assessmentIndex, assessment);
            AssessmentCollection.RemoveAt(assessmentIndex + 1);
        }

        public static void DeleteAssessment(Assessment assessment)
        {
            AssessmentCollection.Remove(assessment);
        }
    }
}
