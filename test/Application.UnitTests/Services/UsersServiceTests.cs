using Application.Services;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using AutoMapper;
using Moq;
using Persistence.UserRepo;
using Xunit;

namespace WebAPI.UnitTests.Controllers
{
    public class UsersServiceTests
    {
        [Fact]
        public void SutThrowsWhenConstructureArgumentsAreNull()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var assertion = new GuardClauseAssertion(fixture);

            assertion.Verify(typeof(UserService).GetConstructors());
        }

        [Fact]
        public void SutConstructed()
        {
            var sut = new UserService(Mock.Of<IUserWriter>(), Mock.Of<IUserReader>(), Mock.Of<IMapper>());
            Assert.NotNull(sut);
            Assert.IsAssignableFrom<IUserService>(sut);
        }
    }
}
