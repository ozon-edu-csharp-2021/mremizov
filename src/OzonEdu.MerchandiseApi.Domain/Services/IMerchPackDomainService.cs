using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using OzonEdu.MerchandiseApi.Domain.Common;
using OzonEdu.MerchandiseApi.Domain.Entities;
using OzonEdu.MerchandiseApi.Domain.Enumerations;

namespace OzonEdu.MerchandiseApi.Domain.Services
{
    public interface IMerchPackDomainService : IDomainService
    {
        Task<MerchPack> FindBy(MerchType merchType, ClothingSize clothingSize, CancellationToken token);
    }
}
