using System;
using System.Collections.ObjectModel;
using SQLite;
using System.Linq;
using DelmainC971.Models;
using DelmainC971.Database;
using System.Collections.Generic;

namespace DelmainC971.Models
{
    public class Course
    {
        public static ObservableCollection<Course> CourseCollection = new ObservableCollection<Course>();

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Status { get; set; }
        public string InstructorName { get; set; }
        public string InstructorPhone { get; set; }
        public string InstructorEmail { get; set; }
        public bool Notifications { get; set; }
        public string Notes { get; set; }
        public int TermID { get; set; }

        public static void PopulateCourseCollection()
        {
            var database = new DBHelper();
            database.Initialize();

            var courses = database.GetCourses();

            CourseCollection.Clear();

            courses.ForEach(course => CourseCollection.Add(course));

            database.Close();
        }

        public static void PopulateCourseCollection(int termID)
        {
            var database = new DBHelper();
            database.Initialize();

            var courses = database.GetCoursesByTermID(termID);

            CourseCollection.Clear();

            courses.ForEach(course => CourseCollection.Add(course));

            database.Close();
        }

        public static void AddCourse(Course course)
        {
            CourseCollection.Add(course);
        }

        public static void UpdateCourse(Course course)
        {
            int courseIndex = 0;

            foreach (Course item in CourseCollection)
            {
                if (item.ID == course.ID)
                {
                    courseIndex = CourseCollection.IndexOf(item);
                }
            }

            CourseCollection.Insert(courseIndex, course);
            CourseCollection.RemoveAt(courseIndex + 1);
        }

        public static void DeleteCourse(Course course)
        {
            CourseCollection.Remove(course);
        }
    }
}
