using System.Threading;
using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Http.Contracts;

namespace OzonEdu.MerchandiseApi.Http.Client
{
    public interface IMerchHttpClient
    {
        Task<GetMerchInfoResponse> GetMerchInfo(CancellationToken token);

        Task<GiveOutMerchResponse> GiveOutMerch(GiveOutMerchRequest request, CancellationToken token);
    }
}
