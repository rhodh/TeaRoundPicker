using System;
using System.Collections.Generic;

namespace Persistence.DBModels
{
    public class DrinkOrderDbModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public IDictionary<string, string> AdditionalSpecification { get; set; }
        public virtual UserDbModel User { get; set; }
        public virtual IEnumerable<DrinkRunDbModel> DrinkRuns { get; set; }
    }
}
