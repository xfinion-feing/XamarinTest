using System;
using System.Collections.Generic;
using System.Text;
using XamarinTutorial.Model;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace XamarinTutorial.Logic
{
    class VenueLogic
    {
        public static async Task<List<Venue>> GetVenues (double lat, double longitude)
        {
            List<Venue> venues = new List<Venue>();
            var url = VenueRoot.GenerateUrl(lat, longitude);

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                var json = await response.Content.ReadAsStringAsync();
                var venueRoot = JsonConvert.DeserializeObject<VenueRoot>(json);
                venues = venueRoot.response.venues as List<Venue>;
            }

            return venues;
        }
    }
}
