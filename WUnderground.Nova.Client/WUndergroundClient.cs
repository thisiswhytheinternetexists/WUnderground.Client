using ServiceStack;
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
        private const string GeolookupAndCurrentConditionsUri = "/{0}/geolookup/conditions/q/{1},{2}.json";
        private const string CityLookupAndCurrentConditionsUri = "/{0}/conditions/q/{1}/{2}.json";
        private const string PersonalWeatherStationCurrentConditionsUri = "/{0}/conditions/q/pws:{1}.json";
        private const string GeolookupCurrentConditionsAndForecastUri = "/{0}/geolookup/conditions/forecast/q/{1},{2}.json";
        private const string GeolookupHourlyForecastUri = "/{0}/geolookup/hourly/q/{1},{2}.json";
        private const string GeolookupHistoryUri = "/{0}/conditions/{3}/geolookup/q/{1},{2}.json";

        private static Task<WeatherResponse> GetResponse(string m_uri)
        {
            JsonServiceClient client = new JsonServiceClient("http://api.wunderground.com/api");
            return client.GetAsync<WeatherResponse>(m_uri);
        }

        /// <summary>
        /// Gets the current conditions for the specified city in the specified country
        /// </summary>
        /// <param name="city">City name, for example Utrecht</param>
        /// <param name="countrycode">Country code, for example NL for Netherlands</param>
        /// <returns></returns>
        public static async Task<WeatherResponse> GetConditionsForCityAsync(string city, string countrycode)
        {
            return await GetResponse(string.Format(CityLookupAndCurrentConditionsUri, Config.ApiKey, countrycode, city));
        }

        /// <summary>
        /// Gets the current conditions for the specified Personal Weather Station ID
        /// </summary>
        /// <param name="pws_id">The ID of the Personal Weather Station. DO NOT PREFIX WITH pws:!</param>
        /// <returns></returns>
        public static async Task<WeatherResponse> GetConditionsForPersonalWeatherStationAsync(string pws_id) {
            return await GetResponse(string.Format(PersonalWeatherStationCurrentConditionsUri, Config.ApiKey, pws_id));
        }

        /// <summary>
        /// Gets the current conditions for the specified coordinates
        /// </summary>
        /// <param name="lat">The latitude</param>
        /// <param name="lng">The longitude</param>
        /// <returns>The response object</returns>
        public static async Task<WeatherResponse> GetConditionsForLocationAsync(double lat, double lng)
        {
            return await GetResponse(string.Format(GeolookupAndCurrentConditionsUri, Config.ApiKey, lat, lng));
        }

        /// <summary>
        /// Gets the current conditions and forecast of the specified coordinates
        /// </summary>
        /// <param name="lat">The latitude</param>
        /// <param name="lng">The longitude</param>
        /// <returns>The response object</returns>
        public static async Task<WeatherResponse> GetConditionsAndForecastForLocationAsync(double lat, double lng)
        {
            return await GetResponse(string.Format(GeolookupCurrentConditionsAndForecastUri, Config.ApiKey, lat, lng));
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
