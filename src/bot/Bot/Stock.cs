using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace Bot
{
    internal static class Stock
    {
        public static void ProcessStock(string code)
        {
            using HttpClient client = new();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var stockUrl = "https://stooq.com/q/l/?s=aapl.us&f=sd2t2ohlcv&h&e=csv";

            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, stockUrl);

            var response = client.Send(requestMessage);

            var stream = response.Content.ReadAsStream();


            using (var reader = new StreamReader(stream))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<StockModel>();
            }
        }
    }
}
