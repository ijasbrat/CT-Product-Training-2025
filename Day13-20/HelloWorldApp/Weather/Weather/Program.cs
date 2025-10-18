using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    private static readonly string apiKey = "c4c99f1a6d094a468e872429251310";
    static async Task Main()
    {
        Console.WriteLine("  Weather Forecast App ");
        Console.Write("Enter a city name: ");
        string city = Console.ReadLine()?.Trim();

        if (string.IsNullOrWhiteSpace(city))
        {
            Console.WriteLine("  Please enter a valid city name.");
            return;
        }
        await GetWeatherAsync(city);

        Console.WriteLine(" Weather data fetched successfully!");
    }
    static async Task GetWeatherAsync(string city)
    {
        string url = $"https://api.weatherapi.com/v1/current.json?key={apiKey}&q={city}&aqi=no";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject data = JObject.Parse(json);
                    string cityName = data["location"]["name"].ToString();
                    string region = data["location"]["region"].ToString();
                    string country = data["location"]["country"].ToString();
                    string condition = data["current"]["condition"]["text"].ToString();
                    double tempC = (double)data["current"]["temp_c"];
                    double feelsLike = (double)data["current"]["feelslike_c"];
                    double windKph = (double)data["current"]["wind_kph"];
                    int humidity = (int)data["current"]["humidity"];
                    Console.WriteLine($" {cityName}, {region}, {country}");
                    Console.WriteLine($"   Temperature: {tempC}°C (Feels like {feelsLike}°C)");
                    Console.WriteLine($"   Condition: {condition}");
                    Console.WriteLine($"   Wind: {windKph} km/h");
                    Console.WriteLine($"   Humidity: {humidity}%");
                }
                else
                {
                    Console.WriteLine($"  Could not fetch weather for '{city}'. ({response.ReasonPhrase})");
                }
            }
        }
        catch (HttpRequestException)
        {
            Console.WriteLine($" Network error while fetching data for '{city}'.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($" Error for '{city}': {ex.Message}");
        }
    }
}
