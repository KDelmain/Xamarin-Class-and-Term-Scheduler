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
    public partial class AssessmentDataPage : ContentPage
    {
        private AssessmentDetailPage adp;
        private Assessment currentAssessment { get; set; }
        private int CourseID;
        public AssessmentDataPage(int courseID)
        {
            InitializeComponent();

            CourseID = courseID;

            saveEditButton.IsVisible = false;
        }

        public AssessmentDataPage(AssessmentDetailPage assessmentData, Assessment assessment)
        {
            InitializeComponent();

            CourseID = assessment.CourseID;
            adp = assessmentData;
            currentAssessment = assessment;

            assessmentName.Text = assessment.Name;
            assessmentStart.Date = assessment.StartDate;
            assessmentEnd.Date = assessment.EndDate;
            assessmentType.SelectedItem = assessment.Type;
            notifications.IsToggled = assessment.Notifications;

            saveButton.IsVisible = false;
        }
        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (assessmentName.Text == null || assessmentName.Text == "")
                {
                    throw new Exception("Assessment name cannot be left blank.");
                }

                if (new DateTime(assessmentStart.Date.Year, assessmentStart.Date.Month, assessmentStart.Date.Day) > new DateTime(assessmentEnd.Date.Year, assessmentEnd.Date.Month, assessmentEnd.Date.Day))
                {
                    throw new Exception("Assessment start date must be before course end date.");
                }

                if (assessmentType.SelectedItem == null)
                {
                    throw new Exception("You must pick an assessment type.");
                }

                if (Assessment.AssessmentCollection.Any())
                {
                    if (Assessment.AssessmentCollection.FirstOrDefault().Type == assessmentType.SelectedItem.ToString())
                    {
                        throw new Exception("You cannot have two assessments of the same type.");
                    }
                }

                var newAssessmentData = new Assessment
                {
                    CourseID = CourseID,
                    Name = assessmentName.Text,
                    StartDate = assessmentStart.Date,
                    EndDate = assessmentEnd.Date,
                    Type = assessmentType.SelectedItem.ToString(),
                    Notifications = notifications.IsToggled
                };

                var db = new DBHelper();
                db.Initialize();
                db.AddAssessment(newAssessmentData);
                db.Close();

                Assessment.AddAssessment(newAssessmentData);

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
                if (assessmentName.Text == "")
                {
                    throw new Exception("Assessment name cannot be left blank.");
                }

                if (new DateTime(assessmentStart.Date.Year, assessmentStart.Date.Month, assessmentStart.Date.Day) > new DateTime(assessmentEnd.Date.Year, assessmentEnd.Date.Month, assessmentEnd.Date.Day))
                {
                    throw new Exception("Assessment start date must be before course end date.");
                }
                var db = new DBHelper();
                db.Initialize();

                var assessmentList = db.GetAssessmentsByCourseID(adp.currentAssessment.CourseID);

                if(assessmentList.Count > 1)
                {
                    if (Assessment.AssessmentCollection.Where(test => test.ID != adp.currentAssessment.ID).FirstOrDefault().Type == assessmentType.SelectedItem.ToString())
                    {
                        throw new Exception("You cannot have two assessments of the same type.");
                    }
                }
                
                
                var newAssessment = adp;

                var newAssessmentData = new Assessment
                {
                    ID = newAssessment.currentAssessment.ID,
                    CourseID = newAssessment.currentAssessment.CourseID,
                    Name = assessmentName.Text,
                    StartDate = assessmentStart.Date,
                    EndDate = assessmentEnd.Date,
                    Type = assessmentType.SelectedItem.ToString(),
                    Notifications = notifications.IsToggled
                };

                
                db.UpdateAssessment(newAssessmentData);
                db.Close();

                Assessment.UpdateAssessment(newAssessmentData);

                newAssessment.CurrentAssessment(newAssessmentData);

                await Navigation.PopModalAsync();

                //await Navigation.PushModalAsync(new AssessmentDetailPage(newAssessmentData));
            }
            catch (Exception error)
            {
                await DisplayAlert("Alert", $"{error.Message}", "OK");
            }

            await Navigation.PopModalAsync();
        }

        private async void CancelButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}