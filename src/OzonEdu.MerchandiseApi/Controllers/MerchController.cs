using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseApi.Domain.Services;
using OzonEdu.MerchandiseApi.Http.Contracts;

namespace OzonEdu.MerchandiseApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public sealed class MerchController : ControllerBase
    {
        private readonly IMerchDomainService _merchDomainService;

        public MerchController(IMerchDomainService merchDomainService)
        {
            _merchDomainService = merchDomainService;
        }

        [HttpPost]
        public async Task<ActionResult<GetMerchInfoResponse>> GetMerchInfo(GetMerchInfoRequest request, CancellationToken token)
        {
            var merchs = await _merchDomainService.GetMerchInfo(request.EmployeeId, token);

            return Ok(new GetMerchInfoResponse
            {
                // TODO: замаппить мерч на модель
            });
        }

        [HttpPost]
        public async Task<ActionResult<GiveOutMerchResponse>> GiveOutMerch(GiveOutMerchRequest request, CancellationToken token)
        {
            var merch = await _merchDomainService.GiveOutMerch(request.EmployeeId, request.MerchType, token);

            return Ok(new GiveOutMerchResponse
            {
                // TODO: замаппить мерч на модель
            });
        }
    }
}
