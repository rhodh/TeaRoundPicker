using System;
using System.Collections.Generic;

namespace Domain.Models
{
    public class DrinkOrder
    {
        public DrinkOrder(Guid id, string name, string type, IDictionary<string, string> additionalSpecification)
        {
            Id = id;
            Name = name;
            Type = type;
            AdditionalSpecification = additionalSpecification;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string Type { get; }
        public IDictionary<string, string> AdditionalSpecification { get; }
    }
}
