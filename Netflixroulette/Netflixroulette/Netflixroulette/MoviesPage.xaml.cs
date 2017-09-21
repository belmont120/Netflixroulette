using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Netflixroulette.Models;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Netflixroulette
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoviesPage : ContentPage
    {
        private List<Movie> _movies;

        private BindableProperty IsSearchingProperty = 
            BindableProperty.Create("IsSearching", typeof(bool), typeof(MoviesPage), false);

        public bool IsSearching
        {
            get { return (bool) GetValue(IsSearchingProperty); }
            set { SetValue(IsSearchingProperty, value);}
        }

        public MoviesPage()
        {
            InitializeComponent();

            BindingContext = this;
        }

        private async void Cell_OnTapped(object sender, EventArgs e)
        {
            if (moviesListView.SelectedItem == null)
                return;

            var movie = (Movie) moviesListView.SelectedItem;
            await Navigation.PushAsync(new MovieDetailsPage(movie));

            moviesListView.SelectedItem = null;
        }

        private async void SearchBar_OnSearchButtonPressed(object sender, EventArgs e)
        {

            if (searchBar.Text.Length < 5)
                await DisplayAlert("Warning", "Search content must be longer than 5 characters.", "OK");

            var movieService = new MovieService();
            try
            {
                IsSearching = true;
                var movies = await movieService.FindMoviesByActor(searchBar.Text);

                if (!movies.Any())
                {
                    moviesListView.IsVisible = false;
                    notFound.IsVisible = true;
                }
                else
                {
                    moviesListView.IsVisible = true;
                    notFound.IsVisible = false;
                    _movies = new List<Movie>(movies);
                    moviesListView.ItemsSource = _movies;
                }
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Warning", "Could not retrieve the list of movies", "OK");
            }
            finally
            {
                IsSearching = false;
            }

        }
    }
}