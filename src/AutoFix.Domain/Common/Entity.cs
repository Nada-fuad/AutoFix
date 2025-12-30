using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoFix.Domain.Common
{
   public abstract class Entity
    {
        public Guid Id { get;  }
        private readonly List<DomainEvent> _domainEvents=[];
        public IReadOnlyCollection<DomainEvent> DomainEvents=>_domainEvents;


        protected Entity() { }
        protected Entity(Guid id)
        {
            Id = (id == Guid.Empty) ? Guid.NewGuid() : id;
        }


        public void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents() {
            _domainEvents.Clear();
        }
    }
}
