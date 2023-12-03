/// <summary>
/// Created by Alexander Carvalho, Functionality to convert address to lat and lng
/// using Google Geocode API
/// </summary>

using System.Net;
using Newtonsoft.Json.Linq;
using PARKIT_enterprise_final.Models;

namespace PARKIT_enterprise_final.Utils
{
    /// <summary>
    /// Functionality to convert address to lat and lng
    /// using Google Geocode API
    /// </summary>
    public class GeocodeApi
    {
        private readonly HttpClient _client;

        public GeocodeApi(HttpClient client)
        {
            _client = client;
        }

        /// <summary>
        /// Sends a request to the Google Geocode API to convert an address to lat and lng
        /// </summary>
        /// <param name="address"></param>
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
