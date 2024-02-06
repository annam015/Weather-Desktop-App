using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Weather_Desktop_App
{
    public partial class MainForm : Form
    {
        private WeatherAPI weatherAPI;

        public MainForm()
        {
            InitializeComponent();
        }

        private async void MainForm_Load(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            string currentCity = await LocationAPI.getCurrentCity();
            if(currentCity != null) 
            {
                this.weatherAPI = await WeatherAPI.setDataAsync(currentCity);
            }
            else
            {
                MessageBox.Show("An unexpected error occurred: We could not identify your location!",
                                "General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.weatherAPI = await WeatherAPI.setDataAsync("London");

            }
            UpdateUI();
            Cursor = Cursors.Default;
        }

        private async void tbSearchCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && tbSearchCity.Text.Length > 1)
            {
                Cursor = Cursors.WaitCursor;
                string city = tbSearchCity.Text;
                tbSearchCity.Text = "";
                this.weatherAPI = await WeatherAPI.setDataAsync(city);
                UpdateUI();
                Cursor = Cursors.Default;
            }
        }

        private void tbSearchCity_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void UpdateUI()
        {
            if (this.weatherAPI != null)
            {
                this.SuspendLayout();

                try
                {
                    lbCityValue.Text = weatherAPI.getLocationDetails().getCity();
                    lbCountryValue.Text = weatherAPI.getLocationDetails().getCountry();
                    lbLatitudeValue.Text = weatherAPI.getLocationDetails().getLatitude().ToString();
                    lbLongitudeValue.Text = weatherAPI.getLocationDetails().getLongitude().ToString();

                    lbActualTemp.Text = weatherAPI.getWeatherDetailsList().ElementAt(0).getTemperature().ToString() + "°C";
                    lbSummary.Text = weatherAPI.getWeatherDetailsList().ElementAt(0).getSummary();
                    lbWindSpeedValue.Text = weatherAPI.getWeatherDetailsList().ElementAt(0).getWindSpeed().ToString() + " km/h";
                    lbWindDirection.Text = weatherAPI.getWeatherDetailsList().ElementAt(0).getWindDirection();
                    lbHumidityValue.Text = weatherAPI.getWeatherDetailsList().ElementAt(0).getHumidity().ToString() + "%";
                    lbVisibilityValue.Text = weatherAPI.getWeatherDetailsList().ElementAt(0).getVisibility().ToString() + " km";
                    lbPressureValue.Text = weatherAPI.getWeatherDetailsList().ElementAt(0).getPressure().ToString() + " hPa";
                    lbMinActualValue.Text = weatherAPI.getWeatherDetailsList().ElementAt(0).getMin().ToString() + "°C";
                    lbMaxActualValue.Text = weatherAPI.getWeatherDetailsList().ElementAt(0).getMax().ToString() + "°C";
                    lbSunrise.Text = weatherAPI.getWeatherDetailsList().ElementAt(0).getSunrise();
                    lbSunset.Text = weatherAPI.getWeatherDetailsList().ElementAt(0).getSunset();

                    label1InfoMin.Text = weatherAPI.getWeatherDetailsList().ElementAt(1).getMin().ToString() + "°C";
                    label1InfoMax.Text = weatherAPI.getWeatherDetailsList().ElementAt(1).getMax().ToString() + "°C";
                    label2InfoMin.Text = weatherAPI.getWeatherDetailsList().ElementAt(2).getMin().ToString() + "°C";
                    label2InfoMax.Text = weatherAPI.getWeatherDetailsList().ElementAt(2).getMax().ToString() + "°C";
                    label3InfoMin.Text = weatherAPI.getWeatherDetailsList().ElementAt(3).getMin().ToString() + "°C";
                    label3InfoMax.Text = weatherAPI.getWeatherDetailsList().ElementAt(3).getMax().ToString() + "°C";
                    label4InfoMin.Text = weatherAPI.getWeatherDetailsList().ElementAt(4).getMin().ToString() + "°C";
                    label4InfoMax.Text = weatherAPI.getWeatherDetailsList().ElementAt(4).getMax().ToString() + "°C";
                    label5InfoMin.Text = weatherAPI.getWeatherDetailsList().ElementAt(5).getMin().ToString() + "°C";
                    label5InfoMax.Text = weatherAPI.getWeatherDetailsList().ElementAt(5).getMax().ToString() + "°C";

                    pictureBoxActual.Image = getImage(weatherAPI.getWeatherDetailsList().ElementAt(0).getSummary());
                    pictureBox1.Image = getImage(weatherAPI.getWeatherDetailsList().ElementAt(1).getSummary());
                    pictureBox2.Image = getImage(weatherAPI.getWeatherDetailsList().ElementAt(2).getSummary());
                    pictureBox3.Image = getImage(weatherAPI.getWeatherDetailsList().ElementAt(3).getSummary());
                    pictureBox4.Image = getImage(weatherAPI.getWeatherDetailsList().ElementAt(4).getSummary());
                    pictureBox5.Image = getImage(weatherAPI.getWeatherDetailsList().ElementAt(5).getSummary());

                    label1Day.Text = DateTime.Now.AddDays(1).ToString("dddd");
                    label2Day.Text = DateTime.Now.AddDays(2).ToString("dddd");
                    label3Day.Text = DateTime.Now.AddDays(3).ToString("dddd");
                    label4Day.Text = DateTime.Now.AddDays(4).ToString("dddd");
                    label5Day.Text = DateTime.Now.AddDays(5).ToString("dddd");
                } 
                catch (Exception ex)
                {
                    MessageBox.Show("An unexpected error occurred: We could not update the user interface!",
                                "General Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.ResumeLayout(true);
                }
            }
        }

        private Image getImage(string summary)
        {
            if(summary.ToLower().Contains("sun"))
            {
                return Properties.Resources.sun;
            } 
            else if(summary.ToLower().Contains("partly cloudy"))
            {
                return Properties.Resources.cloudy;
            }
            else if (summary.ToLower().Contains("cloud") || summary.ToLower().Contains("haze"))
            {
                return Properties.Resources.clouds;
            } 
            else if (summary.ToLower().Contains("rain") || summary.ToLower().Contains("showers"))
            {
                return Properties.Resources.rainy_day;
            }
            else if (summary.ToLower().Contains("storm"))
            {
                return Properties.Resources.storm;
            }
            else
            {
                return Properties.Resources.snow;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
