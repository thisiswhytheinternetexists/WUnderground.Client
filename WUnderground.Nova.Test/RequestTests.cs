using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WUnderground.Client;
using WUnderground.Client.Models;

namespace WUnderground.Nova.Test
{
    [TestClass]
    public class RequestTests
    {
        [TestInitialize]
        public void Init()
        {
            //Configure the client by setting the API Key. Get yours at http://www.wunderground.com
            if (Properties.Settings.Default.Apikey == null || Properties.Settings.Default.Apikey.Length == 0)
                throw new ArgumentException("No API-key provided in App.config");
            WUndergroundClient.Config.ApiKey = Properties.Settings.Default.Apikey;
        }

        [TestMethod]
        public void GetConditionsForLocationAsync()
        {
            //Get the current weather conditions for the specified location
            WeatherResponse current = WUndergroundClient.GetConditionsForLocationAsync(51.4800, 0.0).Result;
            Assert.AreNotEqual(current.current_observation.feelslike_string.Length, 0);
        }

        [TestMethod]
        public void GetConditionsForPwsIdAsync()
        {
            WeatherResponse current_city = WUndergroundClient.GetConditionsForPersonalWeatherStationAsync("KCASANFR70").Result;
            Assert.AreEqual(current_city.current_observation.display_location.full, "San Francisco, CA");
        }

        [TestMethod]
        public void GetConditionsAndForecastForLocationAsync()
        {
            //Get the weather forecast for the specified location
            WeatherResponse forecast = WUndergroundClient.GetConditionsAndForecastForLocationAsync(51.4800, 0.0).Result;
            var v = forecast.forecast.txt_forecast.forecastday[0].fcttext;
            //Debug.WriteLine(forecast.forecast.txt_forecast.forecastday[0].fcttext);
        }

        [TestMethod]
        public void GetConditionsForCityAsync()
        {
            WeatherResponse current_city = WUndergroundClient.GetConditionsForCityAsync("Utrecht", "NL").Result;
            Assert.AreEqual(current_city.current_observation.display_location.full, "Utrecht, Netherlands");
        }
    }
}
