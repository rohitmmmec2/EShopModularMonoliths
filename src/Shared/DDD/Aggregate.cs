﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DDD
{
    public abstract class Aggregate<TId> : Entity<TId>, IAggregate<TId>
    {
        private readonly List<IDomainEvent> _domainEvents = [];
        public ReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        
        public void AddDomianEvent(IDomainEvent domianEvent)
        {
            _domainEvents.Add(domianEvent);
        }

        public IDomainEvent[] ClearDomainEvents()
        {
            IDomainEvent[] domainEvents = _domainEvents.ToArray();
            _domainEvents.Clear();
            return domainEvents;
        }
    }
}
