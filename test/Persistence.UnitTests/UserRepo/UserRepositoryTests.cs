using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using Persistence.UserRepo;
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
    }
}
