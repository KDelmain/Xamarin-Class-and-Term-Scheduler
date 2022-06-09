using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DelmainC971.Models;
using DelmainC971.Views;
using DelmainC971.Database;

namespace DelmainC971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDataPage : ContentPage
    {
        private CourseDetailPage cdp;
        private int TermID;

        public CourseDataPage(int termID) //Adding New Course
        {
            InitializeComponent();

            TermID = termID;

            saveEditButton.IsVisible = false;
        }

        public CourseDataPage(CourseDetailPage courseData) //Edit Existing Course
        {
            InitializeComponent();

            cdp = courseData;
            Course course = courseData.currentCourse;
            courseName.Text = course.Name;
            courseStart.Date = course.StartDate;
            courseEnd.Date = course.EndDate;
            courseStatus.SelectedItem = course.Status;
            instructorName.Text = course.InstructorName;
            instructorPhone.Text = course.InstructorPhone;
            instructorEmail.Text = course.InstructorEmail;
            notes.Text = course.Notes;
            notifications.IsToggled = course.Notifications;
            TermID = course.TermID;

            saveButton.IsVisible = false;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (courseName.Text == null || courseName.Text == "")
                {
                    throw new Exception("Course name cannot be left blank.");
                }

                if (new DateTime(courseStart.Date.Year, courseStart.Date.Month, courseStart.Date.Day) > new DateTime(courseEnd.Date.Year, courseEnd.Date.Month, courseEnd.Date.Day))
                {
                    throw new Exception("Course start date must be before course end date.");
                }

                if (courseStatus.SelectedItem == null)
                {
                    throw new Exception("You must pick a course status.");
                }

                if (
                        instructorName.Text == null || instructorName.Text == "" ||
                        instructorPhone.Text == null || instructorPhone.Text == "" ||
                        instructorEmail.Text == null || instructorEmail.Text == ""
                    )
                {
                    throw new Exception("Course instructor info cannot be left blank.");
                }

                if (emailInvalid(instructorEmail.Text))
                {
                    throw new Exception("You must provide a valid email.");
                }

                if (notes.Text == null)
                {
                    notes.Text = "";
                }

                var newCourseData = new Course
                {
                    TermID = TermID,
                    Name = courseName.Text,
                    StartDate = courseStart.Date,
                    EndDate = courseEnd.Date,
                    Status = courseStatus.SelectedItem.ToString(),
                    InstructorName = instructorName.Text,
                    InstructorPhone = instructorPhone.Text,
                    InstructorEmail = instructorEmail.Text,
                    Notes = notes.Text,
                    Notifications = notifications.IsToggled
                };

                var db = new DBHelper();
                db.Initialize();
                db.AddCourse(newCourseData);
                db.Close();

                Course.AddCourse(newCourseData);

                await Navigation.PopModalAsync();
            }
            catch (Exception error)
            {
                await DisplayAlert("Alert", $"{error.Message}", "OK");
            }
        }

        private async void SaveEditButton_Clicked(object sender, EventArgs e)
        {
            try
            {

                if (courseName.Text == "")
                {
                    throw new Exception("Course name cannot be left blank.");
                }

                if (new DateTime(courseStart.Date.Year, courseStart.Date.Month, courseStart.Date.Day) > new DateTime(courseEnd.Date.Year, courseEnd.Date.Month, courseEnd.Date.Day))
                {
                    throw new Exception("Course start date must be before course end date.");
                }

                if (instructorName.Text == "" || instructorPhone.Text == "" || instructorEmail.Text == "")
                {
                    throw new Exception("Course instructor info cannot be left blank.");
                }

                if (emailInvalid(instructorEmail.Text))
                {
                    throw new Exception("You must provide a valid email.");
                }

                var newCourse = cdp;

                var newCourseData = new Course
                {
                    ID = newCourse.currentCourse.ID,
                    TermID = TermID,
                    Name = courseName.Text,
                    StartDate = courseStart.Date,
                    EndDate = courseEnd.Date,
                    Status = courseStatus.SelectedItem.ToString(),
                    InstructorName = instructorName.Text,
                    InstructorPhone = instructorPhone.Text,
                    InstructorEmail = instructorEmail.Text,
                    Notes = notes.Text,
                    Notifications = notifications.IsToggled
                };

                var db = new DBHelper();
                db.Initialize();
                db.UpdateCourse(newCourseData);
                db.Close();

                Course.UpdateCourse(newCourseData);

                newCourse.CurrentCourse(newCourseData);

                await Navigation.PopModalAsync();
            }
            catch (Exception error)
            {
                await DisplayAlert("Alert", $"{error.Message}", "OK");
            }
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        private bool emailInvalid(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return !(addr.Address == email);
            }
            catch
            {
                return true;
            }
        }
    }
}