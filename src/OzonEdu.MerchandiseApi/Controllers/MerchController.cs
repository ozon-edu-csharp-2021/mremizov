using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OzonEdu.MerchandiseApi.Domain.Enumerations;
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
        [Route("get-merch-info")]
        public async Task<ActionResult<GetMerchInfoResponse>> GetMerchInfo(GetMerchInfoRequest request, CancellationToken token)
        {
            var employeeParameters = new EmployeeParameters
            {
                EmployeeEmail = request.EmployeeEmail,
                EmployeeName = request.EmployeeName
            };

            var merchs = await _merchDomainService.GetMerchInfo(employeeParameters, token);

            return Ok(new GetMerchInfoResponse
            {
                // TODO: замаппить мерч на модель
            });
        }

        [HttpPost]
        [Route("give-out-merch")]
        public async Task<ActionResult<GiveOutMerchResponse>> GiveOutMerch(GiveOutMerchRequest request, CancellationToken token)
        {
            var employeeParameters = new EmployeeParameters
            {
                EmployeeEmail = request.EmployeeEmail,
                EmployeeName = request.EmployeeName
            };

            var merchParameters = new MerchParameters
            {
                MerchType = request.MerchType,
                MerchMode = MerchMode.Manual
            };

            var merch = await _merchDomainService.GiveOutMerch(employeeParameters, merchParameters, token);

            return Ok(new GiveOutMerchResponse
            {
                // TODO: замаппить мерч на модель
            });
        }
    }
}
