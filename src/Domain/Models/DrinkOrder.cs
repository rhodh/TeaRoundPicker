using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class DrinkOrder
    {
        public DrinkOrder(Guid id, Guid userId, string name, string type, IDictionary<string, string> additionalSpecification)
        {
            Id = id;
            UserId = userId;
            Name = name;
            Type = type;
            AdditionalSpecification = additionalSpecification;
        }

        public Guid Id { get; }
        public Guid UserId { get; }
        public string Name { get; }
        public string Type { get; }
        public IDictionary<string, string> AdditionalSpecification { get; }
    }
}
