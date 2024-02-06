using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weather_Desktop_App
{
    internal class LocationAPI
    {
        public static async Task<string> getCurrentCity()
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage
                {
                    RequestUri = new Uri("https://find-any-ip-address-or-domain-location-world-wide.p.rapidapi.com/iplocation?apikey=873dbe322aea47f89dcf729dcc8f60e8"),
                    Headers = { { "X-RapidAPI-Key", ConfigurationManager.AppSettings["APIKey"] },
                                { "X-RapidAPI-Host", "find-any-ip-address-or-domain-location-world-wide.p.rapidapi.com" } }
                };
                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Failed to retrieve location data from the server.",
                                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }

                    string body = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(body);
                    string currentCity = jsonResponse["city"].Value<string>();
                    return currentCity;
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show($"There was an error retrieving weather data: {e.Message}",
                                "Weather Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            catch (Exception e)
            {
                MessageBox.Show($"An unexpected error occurred: {e.Message}",
                                "General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
    }
}
