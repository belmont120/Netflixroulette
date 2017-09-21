using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Netflixroulette.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Netflixroulette
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MovieDetailsPage : ContentPage
    {
        private Movie _movie;
        private Movie _movieFull;
        private MovieService _movieService = new MovieService();
        public MovieDetailsPage(Movie movie)
        {
            InitializeComponent();

            _movie = movie;
        }

        protected override async void OnAppearing()
        {
            _movieFull = await _movieService.GetMovie(_movie.ShowTitle);

            BindingContext = _movieFull;
            base.OnAppearing();


        }
    }
}