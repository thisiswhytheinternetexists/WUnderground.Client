﻿using ServiceStack;
using System;
using System.Threading.Tasks;
using WUnderground.Client.Models;

namespace WUnderground.Client
{
    /// <summary>
    /// The client object to access the WUnderground API
    /// </summary>
    public class WUndergroundClient
    {
        private const string GeolookupAndCurrentConditionsUri = "http://api.wunderground.com/api/{0}/geolookup/conditions/q/{1},{2}.json";
        private const string CityLookupAndCurrentConditionsUri = "http://api.wunderground.com/api/{0}/conditions/q/{1}/{2}.json";
        private const string GeolookupCurrentConditionsAndForecastUri = "http://api.wunderground.com/api/{0}/geolookup/conditions/forecast/q/{1},{2}.json";
        private const string GeolookupHourlyForecastUri = "http://api.wunderground.com/api/{0}/geolookup/hourly/q/{1},{2}.json";
        private const string GeolookupHistoryUri = "http://api.wunderground.com/api/{0}/conditions/{3}/geolookup/q/{1},{2}.json";

        private static Task<WeatherResponse> GetResponse(Uri m_uri)
        {
            JsonServiceClient client = new JsonServiceClient();
            return client.GetAsync<WeatherResponse>(m_uri.ToString());
        }

        /// <summary>
        /// Gets the current conditions for the specified city in the specified country
        /// </summary>
        /// <param name="city">City name, for example Utrecht</param>
        /// <param name="countrycode">Country code, for example NL for Netherlands</param>
        /// <returns></returns>
        public static async Task<WeatherResponse> GetConditionsForCityAsync(string city, string countrycode)
        {
            Uri m_uri = new Uri(string.Format(CityLookupAndCurrentConditionsUri, Config.ApiKey, countrycode, city));
            return await GetResponse(m_uri);
        }

        /// <summary>
        /// Gets the current conditions for the specified coordinates
        /// </summary>
        /// <param name="lat">The latitude</param>
        /// <param name="lng">The longitude</param>
        /// <returns>The response object</returns>
        public static async Task<WeatherResponse> GetConditionsForLocationAsync(double lat, double lng)
        {
            Uri m_uri = new Uri(string.Format(GeolookupAndCurrentConditionsUri, Config.ApiKey, lat, lng));
            return await GetResponse(m_uri);
        }

        /// <summary>
        /// Gets the current conditions and forecast of the specified coordinates
        /// </summary>
        /// <param name="lat">The latitude</param>
        /// <param name="lng">The longitude</param>
        /// <returns>The response object</returns>
        public static async Task<WeatherResponse> GetConditionsAndForecastForLocationAsync(double lat, double lng)
        {
            Uri m_uri = new Uri(string.Format(GeolookupCurrentConditionsAndForecastUri, Config.ApiKey, lat, lng));
            return await GetResponse(m_uri);
        }

        /// <summary>
        /// The configuration for the WUnderground Client
        /// </summary>
        public static class Config
        {
            /// <summary>
            /// The API Key for the WUnderground API. Get yours at http://www.wunderground.com
            /// </summary>
            public static string ApiKey { get; set; }
        }
    }

    /// <summary>
    /// An exception thrown by the WUnderground service
    /// </summary>
    public class WUndergroundException : Exception
    {
        /// <summary>
        /// Creates a new WUnderground exception with the specified message
        /// </summary>
        /// <param name="message">The message</param>
        public WUndergroundException(string message) : base(message) { }
    }
}
