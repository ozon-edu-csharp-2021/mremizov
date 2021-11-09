using System.Linq;
using OzonEdu.MerchandiseApi.Domain.Entities;
using OzonEdu.MerchandiseApi.Domain.Enumerations;
using OzonEdu.MerchandiseApi.Domain.ValueObjects;

namespace OzonEdu.MerchandiseApi.Domain.Tests
{
    public static class TestData
    {
        public static Merch CreateMerch(MerchStatusEnum status)
        {
            var merchStatus = MerchStatus.GetAll<MerchStatus>().First(e => e.Id == (int)status);

            return new Merch(
                MerchMode.Auto,
                CreateEmployee(),
                CreateMerchPack(CSharpCourse.Core.Lib.Enums.MerchType.WelcomePack),
                merchStatus);
        }

        public static Employee CreateEmployee()
        {
            return new Employee(
                new Name("Иванов Иван"),
                new Email("ivanov_ivan@test.ru"),
                ClothingSize.XS);
        }

        public static MerchPack CreateMerchPack(CSharpCourse.Core.Lib.Enums.MerchType merchType)
        {
            switch (merchType)
            {
                case CSharpCourse.Core.Lib.Enums.MerchType.WelcomePack:
                    return new MerchPack(
                        new MerchType(merchType),
                        new SkuList(new[] { new Sku(1) }));
                case CSharpCourse.Core.Lib.Enums.MerchType.ConferenceListenerPack:
                    return new MerchPack(
                        new MerchType(merchType),
                        new SkuList(new[] { new Sku(2) }));
                case CSharpCourse.Core.Lib.Enums.MerchType.ConferenceSpeakerPack:
                    return new MerchPack(
                        new MerchType(merchType),
                        new SkuList(new[] { new Sku(3) }));
                case CSharpCourse.Core.Lib.Enums.MerchType.ProbationPeriodEndingPack:
                    return new MerchPack(
                        new MerchType(merchType),
                        new SkuList(new[] { new Sku(4) }));
                case CSharpCourse.Core.Lib.Enums.MerchType.VeteranPack:
                    return new MerchPack(
                        new MerchType(merchType),
                        new SkuList(new[] { new Sku(5) }));
                default:
                    throw new System.NotImplementedException();
            }
        }
    }
}