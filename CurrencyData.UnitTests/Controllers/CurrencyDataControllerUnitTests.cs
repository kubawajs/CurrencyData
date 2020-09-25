using System;
using CurrencyData.Api.Controllers;
using CurrencyData.Api.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace CurrencyData.UnitTests.Controllers
{
    public class CurrencyDataControllerUnitTests
    {
        //[Fact]
        //public void Get_return_404_if_date_in_the_future()
        //{
        //    // Arrange
        //    var logger = new Mock<ILogger<CurrencyDataController>>();
        //    var service = new Mock<ICurrencyDataService>();
        //    var controller = new CurrencyDataController(logger.Object, service.Object);

        //    // Act
        //    var result = await controller.Get("EUR", "USD", DateTime.MinValue, DateTime.MaxValue, "key");

        //    // Assert
        //    var expected = new JsonResult("hello");
        //    Assert.Equal(result, expected);
        //}
    }
}
