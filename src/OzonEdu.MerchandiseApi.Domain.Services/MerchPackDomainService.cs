using System.Threading;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;
using OzonEdu.MerchandiseApi.Domain.Entities;
using OzonEdu.MerchandiseApi.Domain.Enumerations;
using OzonEdu.MerchandiseApi.Domain.Exceptions;
using OzonEdu.MerchandiseApi.Domain.Repositories;

namespace OzonEdu.MerchandiseApi.Domain.Services
{
    public sealed class MerchPackDomainService : IMerchPackDomainService
    {
        private readonly IMerchPackRepository _merchPackRepository;

        public MerchPackDomainService(IMerchPackRepository merchPackRepository)
        {
            _merchPackRepository = merchPackRepository;
        }

        public async Task<MerchPack> FindBy(MerchType merchType, ClothingSize clothingSize, CancellationToken token)
        {
            var merchPack = await _merchPackRepository.FindBy(merchType, clothingSize, token);

            if (merchPack == null)
            {
                throw new MerchPackNotFoundException();
            }

            return merchPack;
        }
    }
}
