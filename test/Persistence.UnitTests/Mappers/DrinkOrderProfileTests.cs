using AutoMapper;
using Persistence.Mappers;
using Xunit;

namespace Persistence.UnitTests.Mappers
{
    public class DrinkOrderProfileTests
    {
        [Fact]
        public void MapperConfigurationIsValid()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<DrinkOrderProfile>();
            });
            configuration.AssertConfigurationIsValid();
        }

    }
}
