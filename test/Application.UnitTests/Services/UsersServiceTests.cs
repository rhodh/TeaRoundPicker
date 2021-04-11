using Application.Services;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
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
    }
}
