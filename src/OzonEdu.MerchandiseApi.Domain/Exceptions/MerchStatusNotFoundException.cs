using System;

namespace OzonEdu.MerchandiseApi.Domain.Exceptions
{
    public sealed class MerchStatusNotFoundException : DomainException
    {
        public MerchStatusNotFoundException() : base()
        {

        }

        public MerchStatusNotFoundException(string message) : base(message)
        {

        }

        public MerchStatusNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
