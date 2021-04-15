using Application.Calculators;
using Domain.Models;
using System;
using System.Collections.Generic;
using Xunit;

namespace Application.UnitTests.Calculators
{
    public class FairDrinkPickerTests
    {
        [Fact]
        public void SutImplementsIDrinkPicker()
        {
            var sut = new FairDrinkPicker();
            Assert.IsAssignableFrom<IDrinkPicker>(sut);
        }

        [Fact]
        public void SutOneUserReturnsUser()
        {
            var sut = new FairDrinkPicker();
            User user = new(Guid.NewGuid(), "", "", Array.Empty<DrinkOrder>());
            var drinkMaker = sut.CalculateDrinkUser(new User[] { user });

            Assert.Equal(user, drinkMaker);
        }

        [Fact]
        public void SutUserWithMoreComplicatedRequirmentsMakesDrink()
        {
            var sut = new FairDrinkPicker();
            User user = new(Guid.NewGuid(), "", "", Array.Empty<DrinkOrder>());
            var drinkMaker = sut.CalculateDrinkUser(new User[] { user });

            Assert.Equal(user, drinkMaker);
        }

        public static IEnumerable<object[]> SimilarOrders => new[]
        {
            new object[] { CreateDrinkOrder(new Dictionary<string, string>()), CreateDrinkOrder(new Dictionary<string, string>()) },
            new object[] { CreateDrinkOrder(new Dictionary<string, string> { { "one", "item" } }), CreateDrinkOrder(new Dictionary<string, string> { { "one", "item" } }) },
            new object[] { CreateDrinkOrder(new Dictionary<string, string> { { "one", "item" }, { "two", "item" } }), CreateDrinkOrder(new Dictionary<string, string> { { "one", "item" }, { "two", "item" } })},
        };

        private static DrinkOrder CreateDrinkOrder(Dictionary<string, string> dictionary)
        {
            return new DrinkOrder(Guid.NewGuid(), Guid.NewGuid(), "", "", dictionary);
        }

        [Theory]
        [MemberData(nameof(SimilarOrders))]
        public void SutSimilarOrdersFirstUserIsReturned(DrinkOrder order, DrinkOrder otherOrder)
        {
            var sut = new FairDrinkPicker();
            User user = new(Guid.NewGuid(), "", "", new DrinkOrder[] { order });
            User otherUser = new(Guid.NewGuid(), "", "", new DrinkOrder[] { otherOrder });
            var drinkMaker = sut.CalculateDrinkUser(new User[] { user, otherUser });

            Assert.Equal(user, drinkMaker);
        }


        public static IEnumerable<object[]> DifferentUserSecond => new[]
        {
            new object[] { CreateDrinkOrder(new Dictionary<string, string>()), CreateDrinkOrder(new Dictionary<string, string> { { "one", "item" } }) },
            new object[] { CreateDrinkOrder(new Dictionary<string, string> { { "one", "item" } }), CreateDrinkOrder(new Dictionary<string, string> { { "one", "item" }, { "two", "item" } })},
        };


        [Theory]
        [MemberData(nameof(DifferentUserSecond))]
        public void SutDifferentOrdersSecondUserIsReturned(DrinkOrder order, DrinkOrder otherOrder)
        {
            var sut = new FairDrinkPicker();
            User user = new(Guid.NewGuid(), "", "", new DrinkOrder[] { order });
            User otherUser = new(Guid.NewGuid(), "", "", new DrinkOrder[] { otherOrder });
            var drinkMaker = sut.CalculateDrinkUser(new User[] { user, otherUser });

            Assert.Equal(otherUser, drinkMaker);
        }


    }
}
