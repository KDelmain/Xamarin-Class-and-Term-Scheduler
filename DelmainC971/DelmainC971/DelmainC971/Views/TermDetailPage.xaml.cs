using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DelmainC971.Models;
using DelmainC971.Database;
using DelmainC971.Views;
using System.Globalization;

namespace DelmainC971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TermDetailPage : ContentPage
    {
        public Term currentTerm { get; set; }
        public TermDetailPage(Term term)
        {
            Course.PopulateCourseCollection(term.ID);
            InitializeComponent();

            CurrentTerm(term);
        }

        public void CurrentTerm(Term term)
        {
            currentTerm = term;
            pageTitle.Text = term.Name;
            termDates.Text = $"{term.StartDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)} - {term.EndDate.ToString("MM-dd-yyyy", DateTimeFormatInfo.InvariantInfo)}";
        }

        private async void Course_Clicked(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var course = (Course)layout.BindingContext;

            await Navigation.PushAsync(new CourseDetailPage(course));
        }

        private async void AddCourse_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (Course.CourseCollection.Count >= 6)
                {
                    throw new Exception("You cannot add more than 6 courses per term.");
                }

                await Navigation.PushModalAsync(new CourseDataPage(currentTerm.ID));
            }
            catch (Exception error)
            {
                await DisplayAlert("Alert", $"{error.Message}", "OK");
            }
        }

        private async void EditTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TermDataPage(this));
        }

        private async void DeleteTerm_Clicked(object sender, EventArgs e)
        {
            var db = new DBHelper();
            db.Initialize();
            db.DeleteTerm(currentTerm);
            db.Close();

            Term.DeleteTerm(currentTerm);

            await Navigation.PopAsync();
        }
    }
}