using System.Windows.Forms;

namespace Weather_Desktop_App
{
    internal class LocationDetails
    {
        private string city;
        private string country;
        private double latitude;
        private double longitude;

        public LocationDetails(string city)
        {
            this.city = city;
        }

        public LocationDetails(string city, string country, double latitude, double longitude)
        {
            this.city = city;
            this.country = country;
            this.latitude = latitude;
            this.longitude = longitude;
        }

        public string getCity() { return this.city; }
        public string getCountry() { return this.country; }
        public double getLatitude() { return this.latitude; }
        public double getLongitude() { return this.longitude; }
    }
}
