using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Weather_Desktop_App
{
    internal class WeatherAPI
    {
        private LocationDetails locationDetails;
        private List<WeatherDetails> weatherDetailsList;

        private WeatherAPI(LocationDetails locationDetails, List<WeatherDetails> weatherDetailsList)
        {
            this.locationDetails = locationDetails;
            this.weatherDetailsList = weatherDetailsList;
        }

        public LocationDetails getLocationDetails() { return locationDetails; }
        public List<WeatherDetails> getWeatherDetailsList() {  return weatherDetailsList; }

        public static async Task<WeatherAPI> setDataAsync(string location)
        {
            try
            {
                HttpClient client = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri("https://yahoo-weather5.p.rapidapi.com/weather?location=" + location + "&format=json&u=f"),
                    Headers = { { "X-RapidAPI-Key", ConfigurationManager.AppSettings["APIKey"] },
                                { "X-RapidAPI-Host", "yahoo-weather5.p.rapidapi.com" } }
                };
                using (HttpResponseMessage response = await client.SendAsync(request))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("We did not find data for the entered city!",
                                        "General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return null;
                    }

                    string body = await response.Content.ReadAsStringAsync();
                    JObject jsonResponse = JObject.Parse(body);

                    LocationDetails locationDetails = null;
                    List<WeatherDetails> weatherDetailsList = new List<WeatherDetails>();

                    string city = jsonResponse["location"]["city"].Value<string>();
                    string country = jsonResponse["location"]["country"].Value<string>();
                    double latitude = jsonResponse["location"]["lat"].Value<double>();
                    double longitude = jsonResponse["location"]["long"].Value<double>();
                    locationDetails = new LocationDetails(city, country, latitude, longitude);

                    int temperature = jsonResponse["current_observation"]["condition"]["temperature"].Value<int>();
                    string summary = jsonResponse["current_observation"]["condition"]["text"].Value<string>();
                    int min = jsonResponse["forecasts"][0]["low"].Value<int>();
                    int max = jsonResponse["forecasts"][0]["high"].Value<int>();
                    string sunrise = jsonResponse["current_observation"]["astronomy"]["sunrise"].Value<string>();
                    string sunset = jsonResponse["current_observation"]["astronomy"]["sunset"].Value<string>();
                    int windSpeed = jsonResponse["current_observation"]["wind"]["speed"].Value<int>();
                    string windDirection = jsonResponse["current_observation"]["wind"]["direction"].Value<string>();
                    int humidity = jsonResponse["current_observation"]["atmosphere"]["humidity"].Value<int>();
                    int visibility = jsonResponse["current_observation"]["atmosphere"]["visibility"].Value<int>();
                    double pressure = jsonResponse["current_observation"]["atmosphere"]["pressure"].Value<double>();
                    WeatherDetails weatherDetailsToday = new WeatherDetails(toCelsius(temperature), summary, toCelsius(min),
                        toCelsius(max), sunrise, sunset, windSpeed, windDirection, humidity, visibility, pressure);
                    weatherDetailsList.Add(weatherDetailsToday);

                    for(int i = 1; i <= 5; i++)
                    {
                        summary = jsonResponse["forecasts"][i]["text"].Value<string>();
                        min = jsonResponse["forecasts"][i]["low"].Value<int>();
                        max = jsonResponse["forecasts"][i]["high"].Value<int>();
                        WeatherDetails weatherDetails = new WeatherDetails(summary, toCelsius(min), toCelsius(max));
                        weatherDetailsList.Add(weatherDetails);
                    }
                    return new WeatherAPI(locationDetails, weatherDetailsList);
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

        private static int toCelsius(int fahrenheit)
        {
            return (int)Math.Round((fahrenheit - 32) / 1.8);
        }
    }
}
