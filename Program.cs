using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var fortunes = new[]
        {
            "You will have a great day!",
            "Something unexpected will make you smile.",
            "A new opportunity is coming your way.",
            "Be patient, good things take time.",
            "Today is a good day to start something new."
        };

        var random = new Random();
        var fortune = fortunes[random.Next(fortunes.Length)];

        Console.WriteLine($"Selected fortune: {fortune}");

        await SendSms(fortune);
    }

    static async Task SendSms(string message)
    {
        var username = "YOUR_API_USERNAME";
        var password = "YOUR_API_PASSWORD";

        var to = "+467...";   // recipient
        var from = "FortuneApp";
        
        Console.WriteLine("Please enter your phone number:");
        to = Console.ReadLine();

        using var client = new HttpClient();

        var byteArray = Encoding.ASCII.GetBytes($"{username}:{password}");
        client.DefaultRequestHeaders.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Basic", Convert.ToBase64String(byteArray));

        var content = new FormUrlEncodedContent(new[]
        {
            new KeyValuePair<string, string>("to", to),
            new KeyValuePair<string, string>("from", from),
            new KeyValuePair<string, string>("message", message)
        });

        var response = await client.PostAsync("https://api.46elks.com/a1/sms", content);

        var responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine($"Response: {response.StatusCode}");
        Console.WriteLine(responseString);
    }
}