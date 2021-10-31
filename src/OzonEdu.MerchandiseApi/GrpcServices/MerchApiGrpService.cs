using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using OzonEdu.MerchandiseApi.Grpc.Contracts;

namespace OzonEdu.MerchandiseApi.GrpcServices
{
    public sealed class MerchApiGrpService : MerchApiGrpc.MerchApiGrpcBase
    {
        public override Task<GetMerchInfoResponse> GetMerchInfo(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new GetMerchInfoResponse
            {
                Data = "SomeData"
            });
        }

        public override Task<GiveOutMerchResponse> GiveOutMerch(GiveOutMerchRequest request, ServerCallContext context)
        {
            return Task.FromResult(new GiveOutMerchResponse
            {
                Data = "SomeData"
            });
        }
    }
}
