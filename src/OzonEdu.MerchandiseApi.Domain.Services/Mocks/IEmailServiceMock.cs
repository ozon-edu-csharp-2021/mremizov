using System;
using System.Threading.Tasks;
using CSharpCourse.Core.Lib.Enums;

namespace OzonEdu.MerchandiseApi.Domain.Services.Mocks
{
    public interface IEmailServiceMock
    {
        Task SendMerchOutOfStockNotification(string email, string name, MerchType merchType);

        Task SendCanPickupMerchNotification(string email, string name);

        Task SendPickupMerchNotification(string email, string name);
    }

    public sealed class EmailServiceMock : IEmailServiceMock
    {
        public Task SendMerchOutOfStockNotification(string email, string name, MerchType merchType)
        {
            throw new NotImplementedException();
        }

        public Task SendCanPickupMerchNotification(string email, string name)
        {
            throw new NotImplementedException();
        }

        public Task SendPickupMerchNotification(string email, string name)
        {
            throw new NotImplementedException();
        }
    }
}
