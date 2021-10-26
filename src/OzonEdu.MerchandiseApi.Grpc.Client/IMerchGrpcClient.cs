using System.Threading.Tasks;
using OzonEdu.MerchandiseApi.Grpc.Contracts;

namespace OzonEdu.MerchandiseApi.Grpc.Client
{
    public interface IMerchGrpcClient
    {
        Task<GetMerchInfoResponse> GetMerchInfo();

        Task<GiveOutMerchResponse> GiveOutMerch(GiveOutMerchRequest request);
    }
}
