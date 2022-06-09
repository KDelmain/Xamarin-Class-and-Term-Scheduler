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
using System.Globalization;
using Xamarin.Essentials;

namespace DelmainC971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CourseDetailPage : ContentPage
    {
        public Course currentCourse { get; set; }
        public CourseDetailPage(Course course)
        {
            InitializeComponent();
            CurrentCourse(course);
        }

        public void CurrentCourse(Course course)
        {
            currentCourse = course;
            pageTitle.Text = course.Name;
            courseDates.Text = $"{course.StartDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)} - {course.EndDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)}";
            courseStatus.Text = course.Status;
            instructorName.Text = course.InstructorName;
            instructorPhone.Text = course.InstructorPhone;
            instructorEmail.Text = course.InstructorEmail;
            notes.Text = course.Notes;
            notifications.Text = course.Notifications ? "ON" : "OFF";
        }

        private async void CourseAssessments_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CourseAssessmentPage(currentCourse));
        }

        private async void EditCourse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new CourseDataPage(this));
        }

        private async void DeleteCourse_Clicked(object sender, EventArgs e)
        {
            var db = new DBHelper();
            db.Initialize();
            db.DeleteCourse(currentCourse);
            db.Close();

            Course.DeleteCourse(currentCourse);

            await Navigation.PopAsync();
        }

        private async void ShareNotes_Clicked(object sender, EventArgs e)
        {
            await Share.RequestAsync($"{pageTitle.Text} notes:\n{notes.Text}");
        }
    }
}