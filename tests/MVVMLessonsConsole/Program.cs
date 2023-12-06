using System.IO;
using System.Globalization;

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

        private static DateTime[] GetDates() => GetDataLines()
            .First()
            .Split(',')
            .Skip(4)
            .Select(s =>
            {
                if (DateTime.TryParse(s, out var result))
                {
                    return result;
                }
                else
                {
                    // Обработка случая, когда строку не удалось распознать как дату.
                    // Можете вернуть дату-метку по умолчанию или null, в зависимости от вашей логики.
                    return DateTime.MinValue; // Пример: возвращение минимальной даты.
                }
            })
            .ToArray();

        static void Main(string[] args)
        {
            //var client = new HttpClient();

            //var response = client.GetAsync(data_url).Result;

            //var csv_str = response.Content.ReadAsStringAsync().Result;

            //foreach(var data_line in GetDataLines())
            //    Console.WriteLine(data_line);

            var dates = GetDates();

            Console.WriteLine(string.Join("\r\n",dates));

            Console.ReadLine();
        }

    }
}