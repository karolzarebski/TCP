# TCP Server
 Asynchronous TCP Server
 
Server fetches weather data from OpenWeatherMap API based on given location eg. warsaw

<ul>
 <li>Example communication between server and client after data being successfully fetched:</li>
<ul>
 
Enter location (Only english letters): warszawa

Fetching data from API

Location: warszawa</br>
Temperature: 7.01 'C</br>
Max temperature: 8.89 'C</br>
Min temperature: 5.56 'C</br>
Humidity: 93 %</br>
Pressure: 1018 hPa</br>
Feels like temperature: 3.56 'C</br>
Visibility: 9000 m</br>
Wind speed: 3.6 m/s</br>
Wind name: Gentle Breeze</br>
Wind direction: SouthEast</br>
Clouds: broken clouds</br>
General weather: broken clouds</br>

Enter location (Only english letters):

<ul>
 <li>Example communication between server and client after data being unsuccessfully fetched:</li>
<ul>

Enter location (Only english letters): warszawa1234

Fetching data from API

Error: Serwer zdalny zwr?ci? b??d: (404) Nie znaleziono.

Enter location (Only english letters):
