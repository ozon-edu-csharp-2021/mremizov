using System;

namespace OzonEdu.MerchandiseApi.Domain.Exceptions
{
    public sealed class DomainException : ApplicationException
    {
        public DomainException(string message) : base(message)
        {

        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
