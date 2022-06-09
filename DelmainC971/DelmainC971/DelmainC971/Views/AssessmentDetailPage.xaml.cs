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

namespace DelmainC971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AssessmentDetailPage : ContentPage
    {
        public Assessment currentAssessment { get; set; }
        public AssessmentDetailPage(Assessment assessment)
        {
            InitializeComponent();
            CurrentAssessment(assessment);
        }

        public void CurrentAssessment(Assessment assessment)
        {
            currentAssessment = assessment;
            pageTitle.Text = assessment.Name;
            assessmentDates.Text = $"{assessment.StartDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)} - {assessment.EndDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)}";
            assessmentType.Text = assessment.Type;
            notifications.Text = assessment.Notifications ? "ON" : "OFF";
        }

        private async void EditAssessment_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new AssessmentDataPage(this, currentAssessment));
        }

        private async void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var db = new DBHelper();
            db.Initialize();
            db.DeleteAssessment(currentAssessment);
            db.Close();

            Assessment.DeleteAssessment(currentAssessment);
            
            await Navigation.PopAsync();
        }
    }
}