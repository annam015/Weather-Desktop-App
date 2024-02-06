# Weather Desktop App

A desktop app to stay up to date with current weather conditions and forecasts. Built with the .NET Framework 4.8.1, this Windows Forms application uses the Yahoo Weather API and IP Location API to provide accurate and detailed weather data.

## Features

- Real-time weather information for your current location.
- Ability to search and display weather data for any city.
- Detailed information including temperature, humidity, wind speed, atmospheric pressure etc.
- Intuitive and responsive user interface.

## Getting Started

Follow these steps to get the application up and running on your machine:

### Prerequisites

- .NET Framework 4.8.1.
- Valid API Keys for [Yahoo Weather API](https://rapidapi.com/apishub/api/yahoo-weather5) and [IP Location API](https://rapidapi.com/ipfind/api/find-any-ip-address-or-domain-location-world-wide).


### Configuration

1. Clone the repository to your local machine.
2. Open the solution in Visual Studio.
3. Navigate to the `App.config` file.
4. Replace `ENTER_YOUR_API_KEY` with your actual API Keys.

```xml
<appSettings>
    <add key="APIKey" value="ENTER_YOUR_API_KEY"/>
</appSettings>
```

### Running the Application

Once the API Keys are configured, you can run the application through Visual Studio.
