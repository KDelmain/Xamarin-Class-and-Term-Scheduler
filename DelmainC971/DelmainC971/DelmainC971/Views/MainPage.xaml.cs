using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DelmainC971.Models;

namespace DelmainC971.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            Term.PopulateTermCollection();
            InitializeComponent();
        }

        private async void Term_Clicked(object sender, EventArgs e)
        {
            var layout = (BindableObject)sender;
            var term = (Term)layout.BindingContext;

            await Navigation.PushAsync(new TermDetailPage(term));
        }

        private async void AddTerm_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new TermDataPage());
        }
    }
}