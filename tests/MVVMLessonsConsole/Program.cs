using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Linq;
using MVVMLessonsConsole;

class Program
{
    private const string data_url = @"https://api.coingecko.com/api/v3/search/trending";

    #region StreamCreator
    private static async Task<Stream> GetDateStream()
    {
        var client = new HttpClient();
        var response = await client.GetAsync(data_url, HttpCompletionOption.ResponseHeadersRead);
        return await response.Content.ReadAsStreamAsync();
    }
    #endregion

    #region StringSubsequence
    private static IEnumerable<string> GetDateLines()
    {
        using var data_stream = GetDateStream().Result;
        using var data_reader = new StreamReader(data_stream);

        while (!data_reader.EndOfStream)
        {
            var line = data_reader.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;
            yield return line;
        }
    }
    #endregion

    #region StringFilter
    private static IEnumerable<CoinData> GetCoinData()
    {
        var coinInfo = GetDateLines()
            .ElementAtOrDefault(1)
            .Split(',')
            .Select(part => part.Trim())
            .Where(part => part.StartsWith("\"coin_id\":") || part.StartsWith("\"name\":"))
            .ToArray();

        var coinDataList = new List<CoinData>();

        for (int i = 0; i < coinInfo.Length; i += 2)
        {
            var coinId = int.Parse(coinInfo[i].Split(':')[1]);
            var coinName = coinInfo[i + 1].Split(':')[1].Trim('"');
            coinDataList.Add(new CoinData { CoinId = coinId, CoinName = coinName });
        }

        return coinDataList;
    }
    #endregion

    static void Main()
    {
         var coinDataList = GetCoinData();

        Console.WriteLine("Coin Information:");
        foreach (var coinData in coinDataList)
        {
            Console.WriteLine($"Coin ID: {coinData.CoinId}, Coin Name: {coinData.CoinName}");
        }
    }
}


    //public class Coin
    //{
    //    public string Name { get; set; }
    //    public decimal CurrentPrice { get; set; }
    //}

    //class Program
    //{

    //    static async Task Main()
    //    {
    //        // URL API CoinGecko для получения информации о криптовалютах
    //        string apiUrl = "https://api.coingecko.com/api/v3/coins/markets";

    //        // Параметры запроса, например, идентификаторы криптовалют и валюта
    //        string ids = "bitcoin,ethereum,litecoin";
    //        string vsCurrency = "usd";

    //        // Формирование URL с параметрами
    //        string fullUrl = $"{apiUrl}?ids={ids}&vs_currency={vsCurrency}";

    //        // Получение данных с использованием HttpClient и десериализация JSON
    //        List<Coin> coins = await GetCoinsFromApi(fullUrl);

    //        // LINQ-запрос для выбора названия и текущей цены каждой криптовалюты
    //        var query = from coin in coins
    //                    select new { coin.Name, coin.CurrentPrice };

    //        // Вывод результатов
    //        foreach (var result in query)
    //        {
    //            Console.WriteLine($"Название: {result.Name}, Цена: {result.CurrentPrice}");
    //        }
    //    }

    //    static async Task<List<Coin>> GetCoinsFromApi(string apiUrl)
    //    {
    //        using (HttpClient client = new HttpClient())
    //        {
    //            try
    //            {
    //                // Отправка GET-запроса и десериализация JSON
    //                List<Coin> coins = await client.GetFromJsonAsync<List<Coin>>(apiUrl);
    //                return coins;
    //            }
    //            catch (Exception ex)
    //            {
    //                Console.WriteLine($"Ошибка при получении данных с API: {ex.Message}");
    //                return null;
    //            }
    //        }
    //    }

    //private const string data_url = @"https://microsoft.com/";

    ////private static async Task<Stream> GetDataStream()
    ////{
    ////    var client = new HttpClient();
    ////    var response = await client.GetAsync(data_url,HttpCompletionOption.ResponseHeadersRead);
    ////    return await response.Content.ReadAsStreamAsync();
    ////}

    //static  void Main(string[] args)
    //{
    //    using var client = new HttpClient();
    //    var result =  client.GetStringAsync(data_url);

    //    var content = response.Content.ReadAsStringAsync();
    //}

    //private static  IEnumerable<string> GetDataLines()
    //{
    //     using var data_stream =  GetDataStream().Result;
    //     using var data_reader = new StreamReader(data_stream);

    //    while (!data_reader.EndOfStream)
    //    {
    //        var line =  data_reader.ReadLine();
    //        if (string.IsNullOrEmpty(line)) continue;
    //        yield return line.Replace("Korea,","Korea -"); 
    //    }
    //}

    //private static DateTime[] GetDates() => GetDataLines()
    //    .First()
    //    .Split(',')
    //    .Skip(4)
    //    .Select(s => DateTime.Parse(s, CultureInfo.InvariantCulture))
    //    .ToArray();

    //private static IEnumerable<(string Country, string Province, int[] Counts)> GetData()
    //{
    //    var lines = GetDataLines()
    //        .Skip(1)
    //        .Select(line => line.Split(','));

    //    foreach (var row in lines)
    //    {
    //        var province = row[0].Trim();
    //        var country_name = row[1].Trim(' ', '"');
    //        var counts = row.Skip(4).Select(int.Parse).ToArray();

    //        yield return (province, country_name, counts);
    //    }
    //}

    //static void Main(string[] args)
    //{
    //    //var client = new HttpClient();

    //    //var response = client.GetAsync(data_url).Result;

    //    //var csv_str = response.Content.ReadAsStringAsync().Result;

    //    //foreach(var data_line in GetDataLines())
    //    //    Console.WriteLine(data_line);

    //    //var dates = GetDates();

    //    //Console.WriteLine(string.Join("\r\n",dates));


    //    var ukraine_data = GetData()
    //        .First(v => v.Country.Equals("Ukraine"));

    //    Console.WriteLine(string.Join("\r\n",GetDates().Zip(ukraine_data.Counts,(date,count)=>$"{date}-{count}")));

    //    Console.ReadLine();
    //}


