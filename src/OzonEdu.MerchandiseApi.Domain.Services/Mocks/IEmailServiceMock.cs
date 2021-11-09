using System;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;

namespace OzonEdu.MerchandiseApi.Domain.Services.Mocks
{
    public interface IEmailServiceMock
    {
        Task SendMerchOutOfStockNotification(string email, MerchType merchType);

        Task SendCanPickupMerchNotification(string email);

        Task SendPickupMerchNotification(string email);
    }

    public sealed class EmailServiceMock : IEmailServiceMock
    {
        public Task SendMerchOutOfStockNotification(string email, MerchType merchType)
        {
            throw new NotImplementedException();
        }

        public Task SendCanPickupMerchNotification(string email)
        {
            throw new NotImplementedException();
        }

        public Task SendPickupMerchNotification(string email)
        {
            throw new NotImplementedException();
        }
    }
}
