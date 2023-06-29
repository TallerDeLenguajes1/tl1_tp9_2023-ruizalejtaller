using EspValores;
using System.Net;
using System.Text.Json;

internal class Program
{
    private static void Main(string[] args)
    {
        Get();
    }

    private static void Get()
    {
        var url = $"https://api.coindesk.com/v1/bpi/currentprice.json";
        var request = (HttpWebRequest)WebRequest.Create(url);
        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";

        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    if (strReader != null)
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string responseBody = objReader.ReadToEnd();
                        var moneda = JsonSerializer.Deserialize<Valores>(responseBody);

                        Console.WriteLine("\nValor del bitcoin");
                        Console.WriteLine("------------------");
                        Console.WriteLine(moneda.bpi.EUR.rate_float + " Euros");
                        Console.WriteLine(moneda.bpi.USD.rate_float + " Dolares");
                        Console.WriteLine(moneda.bpi.GBP.rate_float + " Libras esterlinas");
                        Console.WriteLine("------------------");
                        
                        // Mostrando datos del dolar respecto al bitcoin
                        Console.WriteLine(moneda.disclaimer);
                        Console.WriteLine("\n"+moneda.chartName + " en " + moneda.bpi.USD.description);
                        Console.WriteLine("Valor actual: " + moneda.bpi.USD.rate + " (" + moneda.bpi.USD.code + ")");
                        Console.WriteLine("Siendo " + moneda.time.updated);
                    }
                }
            }
        } catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    } 
}