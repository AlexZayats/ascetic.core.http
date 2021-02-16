using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Ascetic.Core.Http.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<TResult> GetJsonAsync<TResult>(this HttpClient httpClient, string requestUri, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.GetAsync(requestUri, cancellationToken);
            return await response.ReadFromJsonAsync<TResult>(cancellationToken);
        }

        public static Task PostJsonAsync<TData>(this HttpClient httpClient, string requestUri, TData data, CancellationToken cancellationToken = default)
        {
            return httpClient.PostAsync(requestUri, GetJsonHttpContent(data), cancellationToken);
        }

        public static async Task<TResult> PostJsonAsync<TResult, TData>(this HttpClient httpClient, string requestUri, TData data, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.PostAsync(requestUri, GetJsonHttpContent(data), cancellationToken);
            return await response.ReadFromJsonAsync<TResult>(cancellationToken);
        }

        public static Task PutJsonAsync<TData>(this HttpClient httpClient, string requestUri, TData data, CancellationToken cancellationToken = default)
        {
            return httpClient.PutAsync(requestUri, GetJsonHttpContent(data), cancellationToken);
        }

        public static async Task<TResult> PutJsonAsync<TResult, TData>(this HttpClient httpClient, string requestUri, TData data, CancellationToken cancellationToken = default)
        {
            var response = await httpClient.PutAsync(requestUri, GetJsonHttpContent(data), cancellationToken);
            return await response.ReadFromJsonAsync<TResult>(cancellationToken);
        }

        public static Task DeleteJsonAsync(this HttpClient httpClient, string requestUri, CancellationToken cancellationToken = default)
        {
            return httpClient.DeleteAsync(requestUri, cancellationToken);
        }

        private static async Task<TResult> ReadFromJsonAsync<TResult>(this HttpResponseMessage response, CancellationToken cancellationToken = default)
        {
            var contentStream = await response.Content.ReadAsStreamAsync();
            if (contentStream.Length > 0)
            {
                return await JsonSerializer.DeserializeAsync<TResult>(contentStream, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                }, cancellationToken);
            }
            return default(TResult);
        }

        private static HttpContent GetJsonHttpContent<TData>(TData data)
        {
            var dataJson = JsonSerializer.Serialize(data, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            });
            return new StringContent(dataJson, Encoding.UTF8, "application/json");
        }
    }
}
