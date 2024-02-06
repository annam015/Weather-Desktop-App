namespace Weather_Desktop_App
{
    internal class WeatherDetails
    {
        private int temperature;
        private string summary;
        private int min;
        private int max;
        private string sunrise;
        private string sunset;
        private int windSpeed;
        private string windDirection;
        private int humidity;
        private int visibility;
        private double pressure;

        public WeatherDetails(string summary, int min, int max)
        {
            this.summary = summary;
            this.min = min;
            this.max = max;
        }

        public WeatherDetails(int temperature, string summary, int min, int max, string sunrise, 
            string sunset, int windSpeed, string windDirection, int humidity, int visibility, double pressure)
        {
            this.temperature = temperature;
            this.summary = summary;
            this.min = min;
            this.max = max;
            this.sunrise = sunrise;
            this.sunset = sunset;
            this.windSpeed = windSpeed;
            this.windDirection = windDirection;
            this.humidity = humidity;
            this.visibility = visibility;
            this.pressure = pressure;
        }

        public int getTemperature() { return temperature; }
        public string getSummary() { return summary; }
        public int getMin() { return min; }
        public int getMax() { return max; }
        public string getSunrise() { return sunrise; }
        public string getSunset() { return sunset; }
        public int getWindSpeed() { return windSpeed; }
        public string getWindDirection() { return windDirection; }
        public int getHumidity() { return humidity; }
        public int getVisibility() { return visibility; }
        public double getPressure() { return pressure; }
    }
}
