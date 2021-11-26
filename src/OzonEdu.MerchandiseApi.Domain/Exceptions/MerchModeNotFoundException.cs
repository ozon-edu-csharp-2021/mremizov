using System;

namespace OzonEdu.MerchandiseApi.Domain.Exceptions
{
    public sealed class MerchModeNotFoundException : DomainException
    {
        public MerchModeNotFoundException() : base()
        {

        }

        public MerchModeNotFoundException(string message) : base(message)
        {

        }

        public MerchModeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
