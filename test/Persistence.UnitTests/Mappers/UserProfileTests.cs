using AutoMapper;
using Persistence.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Persistence.UnitTests.Mappers
{
    public class UserProfileTests
    {
        [Fact]
        public void MapperConfigurationIsValid()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<UserProfile>();
            });
            configuration.AssertConfigurationIsValid();
        }

    }
}
