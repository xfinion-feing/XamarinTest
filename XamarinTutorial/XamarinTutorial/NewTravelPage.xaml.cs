using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Geolocator;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTutorial.Logic;
using XamarinTutorial.Model;

namespace XamarinTutorial
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewTravelPage : ContentPage
	{
		public NewTravelPage ()
		{
			InitializeComponent ();
		}

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var locator = CrossGeolocator.Current;
            var position = await locator.GetPositionAsync();

            var venues = await VenueLogic.GetVenues(position.Latitude, position.Longitude);
            venueListView.ItemsSource = venues;
        }
        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            try
            {
                var selectedVenue = (Venue)venueListView.SelectedItem;
                var firstCategory = selectedVenue.categories.FirstOrDefault();

                var post = new Model.Post
                {
                    Experience = experienceEntry.Text,
                    CategoryId = firstCategory.id,
                    CategoryName = firstCategory.name,
                    Address = selectedVenue.location.address,
                    Distance = selectedVenue.location.distance,
                    Latitude = selectedVenue.location.lat,
                    Longitude = selectedVenue.location.lng,
                    VenueName = selectedVenue.name
                };

                using (var sqlConn = new SQLiteConnection(App.DatabaseLocation))
                {
                    sqlConn.CreateTable<Post>();
                    var rows = sqlConn.Insert(post);

                    if (rows > 0)
                    {
                        DisplayAlert("Success", "Experience successfully inserted", "OK");
                    }
                    else
                    {
                        DisplayAlert("Error", "Experience failed to be inserted", "OK");
                    }
                }
            }
            catch(NullReferenceException nre)
            {
                DisplayAlert("Error", $"Experience failed to be inserted, {nre.Message}", "OK");
            }           
            catch(Exception ex)
            {
                DisplayAlert("Error", $"Experience failed to be inserted, {ex.Message}", "OK");
            }
        }
    }
}