using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;

namespace APIExercise2
{
    class Program
    {
        static void Main(string[] args)
        {
            var line = File.ReadLines("APIKey.txt").ToArray();
            var client = new HttpClient();
            var APIKey = line[0];
            Console.WriteLine("Which city do you want to check weather for?");
            var city = Console.ReadLine();
            Console.Clear();
            var URL = $"https://api.openweathermap.org/data/2.5/weather?q={city},US&appid={APIKey}&units=imperial";
            var weatherString = client.GetStringAsync(URL).Result;
            //var weatherObject = JObject.Parse(weatherString);

            //var main = weatherObject.GetValue("main");
            //var weather = weatherObject.GetValue("weather");

            var currentTemp = JToken.Parse(weatherString).SelectToken("main.temp").ToString();
            var weather = JToken.Parse(weatherString).SelectToken("weather[0].main").ToString();
            var low = JToken.Parse(weatherString).SelectToken("main.temp_min").ToString();
            var high = JToken.Parse(weatherString).SelectToken("main.temp_max").ToString();

            Console.WriteLine($"The weather in {city} is currently: {weather}.");
            Console.WriteLine($"The temperature is currently: {currentTemp} degrees Fahrenheit.");
            Console.WriteLine($"The high for today is {high} degrees with a low of {low} degrees.");
        }
    }
}
