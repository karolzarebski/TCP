namespace WeatherLibrary
{
    public class WeatherModel
    {
        public string Location { get; set; }
        public string Temperature { get; set; }
        public string MinTemperature { get; set; }
        public string MaxTemperature { get; set; }
        public string Humidity { get; set; }
        public string Pressure { get; set; }
        public string FeelsLikeTemperature { get; set; }
        public string Visibility { get; set; }
        public string WindSpeed { get; set; }
        public string WindName { get; set; }
        public string WindDirection { get; set; }
        public string CloudsName { get; set; }
        public string GeneralWeather { get; set; }
    }
}
