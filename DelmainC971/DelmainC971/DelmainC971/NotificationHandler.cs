using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DelmainC971.Database;
using DelmainC971.Models;
using Plugin.LocalNotifications;

namespace DelmainC971
{
    class NotificationHandler
    {
        public static void startupNotifs() //Finish bodies for notifs
        {
            var currentDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day);

            var database = new DBHelper();
            database.Initialize();

            var courses = database.GetCourses();
            courses.ForEach(course =>
            {
                if (course.Notifications)
                {
                    if (course.StartDate == currentDate)
                    {
                        CrossLocalNotifications.Current.Show("Course Starts Today", "");
                    }
                    else if (course.EndDate == currentDate)
                    {
                        CrossLocalNotifications.Current.Show("Course Ends Today", "");
                    }
                }
            });

            var assessments = database.GetAssessments();
            assessments.ForEach(assessment =>
            {
                if (assessment.Notifications)
                {
                    if (assessment.StartDate == currentDate)
                    {
                        CrossLocalNotifications.Current.Show("Assessment Starts Today", "");
                    }
                    else if (assessment.EndDate == currentDate)
                    {
                        CrossLocalNotifications.Current.Show("Assessment Ends Today", "");
                    }
                }
            });

            database.Close();
        }
    }
}
