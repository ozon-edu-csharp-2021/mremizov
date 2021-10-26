using System.Threading.Tasks;
using Google.Protobuf.WellKnownTypes;
using Grpc.Net.Client;
using OzonEdu.MerchandiseApi.Grpc.Contracts;

namespace OzonEdu.MerchandiseApi.Grpc.Client
{
    public class MerchGrpcClient : IMerchGrpcClient
    {
        private readonly string _address;

        public MerchGrpcClient(string address)
        {
            _address = address;
        }

        public async Task<GetMerchInfoResponse> GetMerchInfo()
        {
            // TODO: перенести channel в поле класса и реализовать IDisposable?
            using (var channel = GrpcChannel.ForAddress(_address))
            {
                var client = new MerchApiGrpc.MerchApiGrpcClient(channel);

                return await client.GetMerchInfoAsync(new Empty());
            }
        }

        public async Task<GiveOutMerchResponse> GiveOutMerch(GiveOutMerchRequest request)
        {
            // TODO: перенести channel в поле класса и реализовать IDisposable?
            using (var channel = GrpcChannel.ForAddress(_address))
            {
                var client = new MerchApiGrpc.MerchApiGrpcClient(channel);

                return await client.GiveOutMerchAsync(request);
            }
        }
    }
}
