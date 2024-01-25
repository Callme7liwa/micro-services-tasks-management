using Newtonsoft.Json;
using System.Text;

namespace ApiGateway.Utils
{
    public class FunctionsUtils
    {
        public static async Task<T> GetApiResponse<T>(IHttpClientFactory httpClientFactory, string apiUrl)
        {
            using (var httpClient = httpClientFactory.CreateClient())
            {
                using (var response = await httpClient.GetAsync(apiUrl))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<T>(content);
                    }
                    else
                    {
                        throw new Exception($"Request failed with status code {response.StatusCode}");
                    }
                }
            }
        }

        public static async Task<HttpResponseMessage> SendHttpPostRequest<T>(IHttpClientFactory httpClientFactory, string apiUrl, T model)
        {
            using (var httpClient = httpClientFactory.CreateClient())
            {
                httpClient.BaseAddress = new Uri(ConstantsUtils.USERS_API);
                var request = new HttpRequestMessage(HttpMethod.Post, apiUrl);
                request.Content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(request);

                return response;
            }
        }
    }
}