using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Jokes.Webservices
{
    /// <summary>
    /// Webservice Client
    /// </summary>
    public class WSClient
    {
        /// <summary>
        /// Performs a Webservice Call
        /// </summary>
        /// <typeparam name="T">The Model Type</typeparam>
        /// <param name="url">Webservice Request URL</param>
        /// <returns>Webservice Response</returns>
        public async Task<T> Get<T>(string url)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync(url);
            var json = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
