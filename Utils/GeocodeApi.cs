using System.Text.Json;
using System.Net;
using Newtonsoft.Json.Linq;

namespace PARKIT_enterprise_final.Utils
{
    public class LatLng
    {
        public string Lat { get; set; }
        public string Lng { get; set; }
    }

    public class GeocodeApi
    {
        private readonly HttpClient _client;

        public GeocodeApi(HttpClient client)
        {
            _client = client;
        }

        public async Task<LatLng> GetGeocode(string address)
        {
            // Encode the address to be URL friendly
            var encodedAddress = WebUtility.UrlEncode(address);

            // Make the request to the Google Geocode API
            var response = await _client.GetAsync($"https://maps.googleapis.com/maps/api/geocode/json?address={encodedAddress}&key={Environment.GetEnvironmentVariable("GOOGLE_API_KEY")}");
            var content = await response.Content.ReadAsStringAsync();
            var json = JObject.Parse(content);

            // Get lat and lng from the response
            var lat = Convert.ToString(json["results"][0]["geometry"]["location"]["lat"].Value<double>());
            var lng = Convert.ToString(json["results"][0]["geometry"]["location"]["lng"].Value<double>());

            return new LatLng { Lat = lat, Lng = lng };
        }
    }
}
