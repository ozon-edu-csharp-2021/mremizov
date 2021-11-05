using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Http.Contracts;

namespace OzonEdu.MerchandiseApi.Http.Client
{
    public sealed class MerchHttpClient : IMerchHttpClient
    {
        private readonly HttpClient _httpClient;

        public MerchHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetMerchInfoResponse> GetMerchInfo(GetMerchInfoRequest request, CancellationToken token)
        {
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync("api/merch/get-merch-info", content, token))
            {
                return await response.Content.ReadFromJsonAsync<GetMerchInfoResponse>(cancellationToken: token);
            }
        }

        public async Task<GiveOutMerchResponse> GiveOutMerch(GiveOutMerchRequest request, CancellationToken token)
        {
            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            using (var response = await _httpClient.PostAsync("api/merch/give-out-merch", content, token))
            {
                return await response.Content.ReadFromJsonAsync<GiveOutMerchResponse>(cancellationToken: token);
            }
        }
    }
}
