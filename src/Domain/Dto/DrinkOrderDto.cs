using System.Collections.Generic;

namespace Domain.Dto
{
    public class DrinkOrderDto
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public IDictionary<string, string> AdditionalSpecification { get; set; }
    }
}
