using System.Collections.Generic;
using MediatR;

namespace OzonEdu.MerchandiseApi.Domain.Common
{
    public abstract class Entity
    {
        private List<INotification> _domainEvents;

        public virtual long Id { get; protected set; }

        public IReadOnlyCollection<INotification> DomainEvents
            => _domainEvents?.AsReadOnly();

        protected Entity()
        {
        }

        protected Entity(long id)
        {
            Id = id;
        }

        public bool IsTransient()
        {
            return Id == default;
        }

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents ??= new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem)
        {
            _domainEvents?.Remove(eventItem);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }

        public override bool Equals(object obj)
        {
            if (obj is not Entity other)
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            if (GetType() != other.GetType())
                return false;

            if (IsTransient() || other.IsTransient())
            {
                return false;
            }

            return Id.Equals(other.Id);
        }

        public static bool operator ==(Entity a, Entity b)
            => a is null && b is null || a is not null && b is not null && a.Equals(b);

        public static bool operator !=(Entity a, Entity b)
            => !(a == b);

        public override int GetHashCode()
            => (GetType().ToString() + Id).GetHashCode();
    }
}