using System;

namespace OzonEdu.MerchandiseApi.Domain.Exceptions
{
    public sealed class ClothingSizeNotFoundException : DomainException
    {
        public ClothingSizeNotFoundException() : base()
        {

        }

        public ClothingSizeNotFoundException(string message) : base(message)
        {

        }

        public ClothingSizeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
