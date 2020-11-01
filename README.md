# TCP Server
Asynchronous TCP Server
 
Server fetches weather data from OpenWeatherMap API based on given location eg. warsaw

<ul>
    <li>
        Example communication between server and client after data being successfully fetched:

        Enter location (Only english letters): warszawa

        Fetching data from API

        Location: warszawa
        Temperature: 7.01 'C
        Max temperature: 8.89 'C
        Min temperature: 5.56 'C
        Humidity: 93 %
        Pressure: 1018 hPa
        Feels like temperature: 3.56 'C
        Visibility: 9000 m
        Wind speed: 3.6 m/s
        Wind name: Gentle Breeze
        Wind direction: SouthEast
        Clouds: broken clouds
        General weather: broken clouds

        Enter location (Only english letters):
 </ul>
 
 <ul>
    <li>
        Example communication between server and client after data being unsuccessfully fetched:

        Enter location (Only english letters): warszawa1234

        Fetching data from API

        Error: Serwer zdalny zwr?ci? b??d: (404) Nie znaleziono.

        Enter location (Only english letters):
 </ul>  

 <ul>
    <li>
        Example communication between server and client after using non ASCII chars:

        Enter location (Only english letters): poznań

        Non ASCII char detected (use only english letters), try again

        Enter location (Only english letters):
</ul>
