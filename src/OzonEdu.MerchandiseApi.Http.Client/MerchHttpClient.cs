using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Http.Contracts;

namespace OzonEdu.MerchandiseApi.Http.Client
{
    public class MerchHttpClient : IMerchHttpClient
    {
        private readonly HttpClient _httpClient;

        public MerchHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetMerchInfoResponse> GetMerchInfo(CancellationToken token)
        {
            using (var response = await _httpClient.GetAsync("api/merch", token))
            {
                var body = await response.Content.ReadAsStringAsync(token);
                return JsonSerializer.Deserialize<GetMerchInfoResponse>(body);
            }
        }

        public async Task<GiveOutMerchResponse> GiveOutMerch(GiveOutMerchRequest request, CancellationToken token)
        {
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync("api/merch", content, token))
            {
                var body = await response.Content.ReadAsStringAsync(token);
                return JsonSerializer.Deserialize<GiveOutMerchResponse>(body);
            }
        }
    }
}
