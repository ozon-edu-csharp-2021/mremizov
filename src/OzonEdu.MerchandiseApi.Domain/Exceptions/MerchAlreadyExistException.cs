using System;

namespace OzonEdu.MerchandiseApi.Domain.Exceptions
{
    public sealed class MerchAlreadyExistException : DomainException
    {
        public MerchAlreadyExistException() : base()
        {

        }

        public MerchAlreadyExistException(string message) : base(message)
        {

        }

        public MerchAlreadyExistException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
