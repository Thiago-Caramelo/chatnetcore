using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Runtime.ConstrainedExecution;

namespace Bot
{
    internal static class Stock
    {
        public static string ProcessStock(string code)
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/csv"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stockUrl = $"https://stooq.com/q/l/?s={code.ToLower()}&f=sd2t2ohlcv&h&e=csv";

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, stockUrl);

            var response = client.Send(requestMessage);

            var stream = response.Content.ReadAsStream();


            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<StockModel>();

                var firstQuote = records.FirstOrDefault();

                var currentQuote = $"{code.ToUpper()} quote is ${firstQuote?.Close} per share";

                Console.WriteLine(currentQuote);

                return currentQuote;
            }
        }
    }
}
