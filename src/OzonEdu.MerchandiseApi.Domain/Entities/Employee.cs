using OzonEdu.MerchandiseApi.Domain.Common;
using OzonEdu.MerchandiseApi.Domain.Enumerations;
using OzonEdu.MerchandiseApi.Domain.ValueObjects;

namespace OzonEdu.MerchandiseApi.Domain.Entities
{
    public sealed class Employee : Entity
    {
        public Employee(
            Name name,
            Email email,
            ClothingSize clothingSize)
        {
            Name = name;
            Email = email;
            ClothingSize = clothingSize;
        }

        /// <summary>
        /// Для dapper.
        /// </summary>
        public Employee(long id) : base(id)
        {
        }

        public Name Name { get; }

        public Email Email { get; }

        public ClothingSize ClothingSize { get; }
    }
}
