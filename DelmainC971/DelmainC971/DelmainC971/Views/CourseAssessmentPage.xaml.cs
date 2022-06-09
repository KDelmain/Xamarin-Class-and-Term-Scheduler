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
    public partial class CourseAssessmentPage : ContentPage
    {
        public Course currentCourse { get; set; }
        public CourseAssessmentPage(Course course)
        {
            Assessment.PopulateAssessmentCollection(course.ID);
            InitializeComponent();

            currentCourse = course;

            pageTitle.Text = $"{course.Name} Assessments";
        }

        private async void Assessment_Clicked(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var assessment = (Assessment)layout.BindingContext;

            await Navigation.PushAsync(new AssessmentDetailPage(assessment));
        }

        private async void AddAssessment_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Assessment.AssessmentCollection.Count >= 2)
                {
                    throw new Exception("You cannot add more than 2 assessments per course.");
                }
                await Navigation.PushModalAsync(new AssessmentDataPage(currentCourse.ID));
            }
            catch (Exception error)
            {
                await DisplayAlert("Alert", $"{error.Message}", "OK");
            }
        }
    }
}