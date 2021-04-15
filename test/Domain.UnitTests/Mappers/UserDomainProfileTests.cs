using AutoMapper;
using Domain.Mappers;
using Xunit;

namespace Persistence.UnitTests.Mappers
{
    public class UserDomainProfileTests
    {
        [Fact]
        public void MapperConfigurationIsValid()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<UserDomainProfile>();
            });
            configuration.AssertConfigurationIsValid();
        }

    }
}
