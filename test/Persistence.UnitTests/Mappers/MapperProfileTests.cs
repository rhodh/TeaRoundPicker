using AutoMapper;
using Persistence.Mappers;
using Xunit;

namespace Persistence.UnitTests.Mappers
{
    public class MapperProfileTests
    {
        [Fact]
        public void MapperConfigurationIsValid()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<UserProfile>();
                config.AddProfile<DrinkOrderProfile>();
                config.AddProfile<DrinkRunProfile>();
            });
            configuration.AssertConfigurationIsValid();
        }

    }
}
