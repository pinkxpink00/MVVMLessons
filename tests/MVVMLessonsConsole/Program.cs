using System;
using System.Collections;

namespace MVVMLessonsConsole
{
    class Program
    {
        private const string data_url = @"https://raw.githubusercontent.com/CSSEGISandData/COVID-19/master/csse_covid_19_data/csse_covid_19_daily_reports/01-01-2021.csv";

        private static async Task<Stream> GetDataStream()
        {
            var client = new HttpClient();
            var response = await client.GetAsync(data_url,HttpCompletionOption.ResponseHeadersRead);
            return await response.Content.ReadAsStreamAsync();
        }

        private static  IEnumerable<string> GetDataLines()
        {
             using var data_stream =  GetDataStream().Result;
            using var data_reader = new StreamReader(data_stream);

            while (!data_reader.EndOfStream)
            {
                var line =  data_reader.ReadLine();
                if (string.IsNullOrEmpty(line)) continue;
                yield return line; 
            }
        }

        static void Main(string[] args)
        {
            var client = new HttpClient();

            var response = client.GetAsync(data_url).Result;

            var csv_str = response.Content.ReadAsStringAsync().Result;

            Console.ReadLine();
        }

    }
}