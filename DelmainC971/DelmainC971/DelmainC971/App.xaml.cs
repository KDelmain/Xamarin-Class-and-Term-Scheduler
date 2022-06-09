using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DelmainC971.Database;
using DelmainC971.Models;
using DelmainC971.Views;

namespace DelmainC971
{
    public partial class App : Application
    {
        public App()
        {
            addTesterData();
            InitializeComponent();
            NotificationHandler.startupNotifs();

            MainPage = new NavigationPage(new MainPage());
        }

        private void addTesterData()
        {
            var database = new DBHelper();
            database.Initialize();

            var dbData = database.GetTerms();

            if (dbData.Count == 0)
            {


                var currentMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);

                database.AddTerm(new Term
                {
                    Name = "Tester Term",
                    StartDate = currentMonth,
                    EndDate = currentMonth.AddMonths(6).AddDays(-1)
                });

                database.AddCourse(new Course
                {
                    Name = "Tester Course",
                    StartDate = currentMonth,
                    EndDate = currentMonth.AddMonths(2).AddDays(-1),
                    Status = "In Progress",
                    InstructorName = "Kaitlyn Delmain",
                    InstructorPhone = "636-222-3297",
                    InstructorEmail = "kdelmai@wgu.edu",
                    Notifications = true,
                    Notes = "",
                    TermID = 1
                });

                database.AddAssessment(new Assessment
                {
                    Name = "Tester PA",
                    StartDate = currentMonth.AddMonths(1),
                    EndDate = currentMonth.AddMonths(2).AddDays(-1),
                    Type = "Performance",
                    Notifications = true,
                    CourseID = 1
                });

                database.AddAssessment(new Assessment
                {
                    Name = "Tester OA",
                    StartDate = currentMonth.AddMonths(1),
                    EndDate = currentMonth.AddMonths(2).AddDays(-1),
                    Type = "Objective",
                    Notifications = false,
                    CourseID = 1
                });
            }

            database.Close();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
