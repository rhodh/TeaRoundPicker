using System;
using System.Collections.Generic;

namespace WebAPI.IntegrationTests.ExpectedModels
{
    //This schema is fixed do not accept breaking changes
    internal class DrinkOrdersV1
    {
        public Guid UserId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public IDictionary<string, string> AdditionalSpecification { get; set; }
    }
}
