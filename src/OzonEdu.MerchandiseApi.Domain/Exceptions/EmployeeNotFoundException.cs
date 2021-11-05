using System;

namespace OzonEdu.MerchandiseApi.Domain.Exceptions
{
    public sealed class EmployeeNotFoundException : DomainException
    {
        public EmployeeNotFoundException() : base()
        {

        }

        public EmployeeNotFoundException(string message) : base(message)
        {

        }

        public EmployeeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
