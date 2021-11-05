using System;

namespace OzonEdu.MerchandiseApi.Domain.Exceptions
{
    public sealed class MerchPackNotFoundException : DomainException
    {
        public MerchPackNotFoundException() : base()
        {

        }

        public MerchPackNotFoundException(string message) : base(message)
        {

        }

        public MerchPackNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
