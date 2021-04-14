using Application.Calculators;
using Application.Services;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoMapper;
using Moq;
using Persistence.DrinkRunRepo;
using Persistence.UserRepo;
using Xunit;

namespace WebAPI.UnitTests.Controllers
{
    public class DrinkRunServiceTests
    {
        [Fact]
        public void SutThrowsWhenConstructureArgumentsAreNull()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var assertion = new GuardClauseAssertion(fixture);

            assertion.Verify(typeof(DrinkRunService).GetConstructors());
        }

        [Fact]
        public void SutConstructed()
        {
            var sut = new DrinkRunService(Mock.Of<IUserReader>(), Mock.Of<IDrinkPicker>(), Mock.Of<IDrinkRunWriter>());
            Assert.NotNull(sut);
            Assert.IsAssignableFrom<IDrinkRunService>(sut);
        }
    }
}
