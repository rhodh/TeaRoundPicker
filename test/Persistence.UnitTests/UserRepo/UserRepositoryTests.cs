using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;
using Persistence.UserRepo;
using System;
using Xunit;

namespace WebAPI.UnitTests.Controllers
{
    public class UserRepositoryTests
    {
        [Fact]
        public void SutThrowsWhenConstructureArgumentsAreNull()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var assertion = new GuardClauseAssertion(fixture);

            assertion.Verify(typeof(UserRepository).GetConstructors());
        }

        [Fact]
        public void SutConstructed()
        {
            var contextOption = new DbContextOptionsBuilder<TeaRoundPickerContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            using var context = new TeaRoundPickerContext(contextOption);

            var sut = new UserRepository(context, Mock.Of<IMapper>());
            Assert.NotNull(sut);
            Assert.IsAssignableFrom<IUserReader>(sut);
            Assert.IsAssignableFrom<IUserWriter>(sut);
        }
    }
}
