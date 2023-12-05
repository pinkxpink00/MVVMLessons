using System;

namespace MVVMLessonsConsole
{
    class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_daily_reports/01-01-2021.csv";

        static void Main(string[] args)
        {
            var client = new HttpClient();

            var response = client.GetAsync(data_url).Result;

            var csv_str = response.Content.ReadAsStringAsync().Result;

            Console.ReadLine();
        }

    }
}