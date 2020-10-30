using System;
using System.Net;
using System.Text;
using System.Xml;

namespace WeatherLibrary
{
    public static class Weather
    {
        private static readonly string ApiKey = "5bf3dfd23ebc1b9ca655e79ca30982c5";

        private static string weatherUrl = $"http://api.openweathermap.org/data/2.5/weather?q=@lokalizacja@&mode=xml&units=metric&appid={ApiKey}";

        private static WeatherModel DownloadWeather(string xml, string location)
        {
            XmlDocument xmlDocument = new XmlDocument();

            xmlDocument.LoadXml(xml);

            XmlNode temperatureNode = xmlDocument.SelectSingleNode("//temperature");
            XmlNode windNode = xmlDocument.SelectSingleNode("//wind");

            return new WeatherModel()
            {
                Location = location,
                Temperature = $"{temperatureNode.Attributes["value"].Value} 'C",
                MinTemperature = $"{temperatureNode.Attributes["min"].Value} 'C",
                MaxTemperature = $"{temperatureNode.Attributes["max"].Value} 'C",
                Humidity = $"{xmlDocument.SelectSingleNode("//humidity").Attributes["value"].Value} %",
                Pressure = $"{xmlDocument.SelectSingleNode("//pressure").Attributes["value"].Value} hPa",
                FeelsLikeTemperature = $"{xmlDocument.SelectSingleNode("//feels_like").Attributes["value"].Value} 'C",
                Visibility = $"{xmlDocument.SelectSingleNode("//visibility").Attributes["value"].Value} m",
                WindSpeed = $"{windNode.SelectSingleNode("speed").Attributes["value"].Value} m/s",
                WindName = windNode.SelectSingleNode("speed").Attributes["name"].Value,
                WindDirection = $"{windNode.SelectSingleNode("direction").Attributes["name"].Value}",
                CloudsName = xmlDocument.SelectSingleNode("//clouds").Attributes["name"].Value,
                GeneralWeather = xmlDocument.SelectSingleNode("//weather").Attributes["value"].Value
            };
        }

        private static string ConvertToString(this WeatherModel weatherModel)
        {
            StringBuilder weatherString = new StringBuilder();

            weatherString.Append($"Location: {weatherModel.Location}\r\n");
            weatherString.Append($"Temperature: {weatherModel.Temperature}\r\n");
            weatherString.Append($"Max temperature: {weatherModel.MaxTemperature}\r\n");
            weatherString.Append($"Min temperature: {weatherModel.MinTemperature}\r\n");
            weatherString.Append($"Humidity: {weatherModel.Humidity}\r\n");
            weatherString.Append($"Pressure: {weatherModel.Pressure}\r\n");
            weatherString.Append($"Feels like temperature: {weatherModel.FeelsLikeTemperature}\r\n");
            weatherString.Append($"Visibility: {weatherModel.Visibility}\r\n");
            weatherString.Append($"Wind speed: {weatherModel.WindSpeed}\r\n");
            weatherString.Append($"Wind name: {weatherModel.WindName}\r\n");
            weatherString.Append($"Wind direction: {weatherModel.WindDirection}\r\n");
            weatherString.Append($"Clouds: {weatherModel.CloudsName}\r\n");
            weatherString.Append($"General weather: {weatherModel.GeneralWeather}\r\n");

            return weatherString.ToString();
        }

        public static string GetWeather(string location)
        {
            using var webClient = new WebClient();

            try
            {
                return DownloadWeather(webClient.DownloadString(weatherUrl.Replace("@lokalizacja@", location)), location).ConvertToString();
            }
            catch (Exception ex)
            {
                return $"Error: {ex.Message}\r\n";
            }
        }
    }
}
