using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence.DrinkRunRepo;
using Persistence.UserRepo;
using System;
using Xunit;

namespace Persistence.UnitTests.DrinkRunRepo
{
    public class DrinkRunRepositoryTests
    {
        [Fact]
        public void SutThrowsWhenConstructureArgumentsAreNull()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var assertion = new GuardClauseAssertion(fixture);

            assertion.Verify(typeof(DrinkRunRepository).GetConstructors());
        }

        [Fact]
        public void SutConstructed()
        {
            var contextOption = new DbContextOptionsBuilder<TeaRoundPickerContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var context = new TeaRoundPickerContext(contextOption);

            var sut = new DrinkRunRepository(context, Mock.Of<IMapper>());
            Assert.NotNull(sut);
            Assert.IsAssignableFrom<IDrinkRunWriter>(sut);
        }
    }
}
