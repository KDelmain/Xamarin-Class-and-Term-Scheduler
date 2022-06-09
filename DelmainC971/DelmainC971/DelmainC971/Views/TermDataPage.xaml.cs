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
    public partial class TermDataPage : ContentPage
    {
        private TermDetailPage tdp;
        public TermDataPage()
        {
            InitializeComponent();

            saveEditButton.IsVisible = false;
        }

        public TermDataPage(TermDetailPage termData)
        {
            InitializeComponent();

            tdp = termData;
            termName.Text = termData.currentTerm.Name;
            termStart.Date = termData.currentTerm.StartDate;
            termEnd.Date = termData.currentTerm.EndDate;

            saveButton.IsVisible = false;
        }

        private async void SaveButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (termName.Text == null || termName.Text == "")
                {
                    throw new Exception("Term name cannot be left blank.");
                }

                if (new DateTime(termStart.Date.Year, termStart.Date.Month, termStart.Date.Day) > new DateTime(termEnd.Date.Year, termEnd.Date.Month, termEnd.Date.Day))
                {
                    throw new Exception("Term start date must be before term end date.");
                }

                var newTermData = new Term
                {
                    Name = termName.Text,
                    StartDate = termStart.Date,
                    EndDate = termEnd.Date
                };

                var db = new DBHelper();
                db.Initialize();
                db.AddTerm(newTermData);
                db.Close();
                Term.AddTerm(newTermData);

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
                if (termName.Text == "")
                {
                    throw new Exception("Term name cannot be left blank.");
                }

                if (new DateTime(termStart.Date.Year, termStart.Date.Month, termStart.Date.Day) > new DateTime(termEnd.Date.Year, termEnd.Date.Month, termEnd.Date.Day))
                {
                    throw new Exception("Term start date must be before term end date.");
                }

                var newTerm = tdp;

                var newTermData = new Term
                {
                    ID = newTerm.currentTerm.ID,
                    Name = termName.Text,
                    StartDate = termStart.Date,
                    EndDate = termEnd.Date
                };

                var db = new DBHelper();
                db.Initialize();
                db.UpdateTerm(newTermData);
                db.Close();

                Term.UpdateTerm(newTermData);

                newTerm.CurrentTerm(newTermData);

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
    }
}