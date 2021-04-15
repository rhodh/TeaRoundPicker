﻿using AutoMapper;
using Domain.Mappers;
using Xunit;

namespace Persistence.UnitTests.Mappers
{
    public class DrinkOrderDomainProfileTests
    {
        [Fact]
        public void MapperConfigurationIsValid()
        {
            var configuration = new MapperConfiguration(config =>
            {
                config.AddProfile<DrinkOrderDomainProfile>();
            });
            configuration.AssertConfigurationIsValid();
        }

    }
}
