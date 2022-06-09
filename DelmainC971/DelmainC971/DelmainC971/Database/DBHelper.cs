using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using DelmainC971.Models;
using System.IO;

namespace DelmainC971.Database
{
    class DBHelper
    {
        private SQLiteConnection database;

        public void Initialize()
        {
            if (database == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DelmainC971PA.db3");
                database = new SQLiteConnection(dbPath);
            }

            database.CreateTables<Term, Course, Assessment>();
        }

        public void Close()
        {
            database.Close();
        }

        public List<Term> GetTerms()
        {
            return database.Table<Term>().ToList();
        }

        public List<Course> GetCourses()
        {
            return database.Table<Course>().ToList();
        }

        public List<Course> GetCoursesByTermID(int termID)
        {
            return database.Query<Course>($"SELECT * FROM course WHERE course.TermID={termID}");
        }

        public List<Assessment> GetAssessments()
        {
            return database.Table<Assessment>().ToList();
        }

        public List<Assessment> GetAssessmentsByCourseID(int courseID)
        {
            return database.Query<Assessment>($"SELECT * FROM assessment WHERE assessment.CourseID={courseID}");
        }

        /*** Term: Add, Update, Delete Functions ***/
        public void AddTerm(Term term)
        {
            database.Insert(term);
        }

        public void UpdateTerm(Term term)
        {
            database.Update(term);
        }

        public void DeleteTerm(Term term)
        {
            database.Delete(term);
        }

        /*** Course: Add, Update, Delete Functions ***/
        public void AddCourse(Course course)
        {
            database.Insert(course);
        }

        public void UpdateCourse(Course course)
        {
            database.Update(course);
        }

        public void DeleteCourse(Course course)
        {
            database.Delete(course);
        }

        /*** Assessment: Add, Update, Delete Functions ***/
        public void AddAssessment(Assessment assessment)
        {
            database.Insert(assessment);
        }

        public void UpdateAssessment(Assessment assessment)
        {
            database.Update(assessment);
        }

        public void DeleteAssessment(Assessment assessment)
        {
            database.Delete(assessment);
        }
    }
}
