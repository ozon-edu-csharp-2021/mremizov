using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseApi.Http.Contracts;

namespace OzonEdu.MerchandiseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MerchController : ControllerBase
    {
        [HttpGet]
        public Task<GetMerchInfoResponse> GetMerchInfo(CancellationToken token)
        {
            //throw new System.Exception("test");

            return Task.FromResult(new GetMerchInfoResponse
            {
                Data = "SomeData"
            });
        }

        [HttpPost]
        public Task<GiveOutMerchResponse> GiveOutMerch(GiveOutMerchRequest request, CancellationToken token)
        {
            return Task.FromResult(new GiveOutMerchResponse
            {
                Data = "SomeData"
            });
        }
    }
}
