using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using WebAPI.Controllers;
using Xunit;

namespace WebAPI.UnitTests.Controllers
{
    public class UsersControllersTests
    {
        [Fact]
        public void SutThrowsWhenConstructureArgumentsAreNull()
        {
            var fixture = new Fixture().Customize(new AutoMoqCustomization());
            var assertion = new GuardClauseAssertion(fixture);

            assertion.Verify(typeof(UsersController).GetConstructors());
        }
    }
}
