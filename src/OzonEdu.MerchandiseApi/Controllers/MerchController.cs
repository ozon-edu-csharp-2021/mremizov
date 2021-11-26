using System.Linq;
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
        private readonly IEmployeeWithMerchsDomainService _employeeWithMerchsDomainService;
        private readonly IMerchDomainService _merchDomainService;

        public MerchController(
            IEmployeeWithMerchsDomainService employeeWithMerchsDomainService,
            IMerchDomainService merchDomainService)
        {
            _employeeWithMerchsDomainService = employeeWithMerchsDomainService;
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

            var employee = await _employeeWithMerchsDomainService.FindBy(employeeParameters, token);

            return Ok(new GetMerchInfoResponse
            {
                Merchs = employee.Merchs
                    .Select(e => new MerchDto
                    {
                        CreatedUtc = e.CreatedUtc,
                        Status = e.Status.Name,
                        Type = e.MerchPack.MerchType.Value.ToString(),
                        SkuList = e.MerchPack.SkuListValues.ToArray()
                    })
                    .ToArray()
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
                Merch = new MerchDto
                {
                    CreatedUtc = merch.CreatedUtc,
                    Status = merch.Status.Name,
                    Type = merch.MerchPack.MerchType.Value.ToString(),
                    SkuList = merch.MerchPack.SkuListValues.ToArray()
                }
            });
        }
    }
}
