using System;

namespace OzonEdu.MerchandiseApi.Domain.Exceptions
{
    public sealed class MerchInvalidStatusException : DomainException
    {
        public MerchInvalidStatusException() : base()
        {

        }

        public MerchInvalidStatusException(string message) : base(message)
        {

        }

        public MerchInvalidStatusException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
