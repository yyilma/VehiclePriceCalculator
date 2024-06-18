using VehiclePriceCalculator.Server.Constants;
using VehiclePriceCalculator.Server.Controllers;
using VehiclePriceCalculator.Server.Interface;
using VehiclePriceCalculator.Server.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace VehiclePriceCalculator.Server.Tests.Controllers
{
    [TestFixture]
    public class VehicleControllerTests
    {
        private Mock<ICarService> _mockCarService;
        private VehicleController _controller;

        [SetUp]
        public void SetUp()
        {
            _mockCarService = new Mock<ICarService>();
            _controller = new VehicleController(_mockCarService.Object);
        }

        [Test]
        public void GetCarTypes_ShouldReturnCarTypesWithDefault()
        {
            // Act
            var result = _controller.GetCarTypes() as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));

            var response = result.Value as VehicleTypesWithDefault;
            Assert.That(response, Is.Not.Null);
            Assert.That(Enum.GetNames(typeof(VehicleType)).ToList(), Is.EqualTo(response.CarTypes));
            Assert.That(VehicleType.Common.ToString(), Is.EqualTo(response.DefaultCarType));
        }

        [Test]
        public void GetVehiclePrice_ShouldReturnBadRequest_ForNegativeBasePrice()
        {
            // Arrange
            decimal basePrice = -1;
            string carTypeStr = "Common";

            // Act
            var result = _controller.GetVehiclePrice(basePrice, carTypeStr).Result as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(400));
            Assert.That(result.Value, Is.EqualTo("Invalid vehicle price"));
        }

        [Test]
        public void GetVehiclePrice_ShouldReturnBadRequest_ForInvalidCarType()
        {
            // Arrange
            decimal basePrice = 10000;
            string carTypeStr = "InvalidType";

            // Act
            var result = _controller.GetVehiclePrice(basePrice, carTypeStr).Result as BadRequestObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(400));
            Assert.That(result.Value, Is.EqualTo("Invalid vehicle type"));
        }

        [Test]
        public void GetVehiclePrice_ShouldReturnCar_ForValidInputs()
        {
            // Arrange
            decimal basePrice = 10000;
            string carTypeStr = "Common";
            var carType = VehicleType.Common;
            var expectedCar = new Vehicle(); // Assume a car instance

            _mockCarService.Setup(service => service.CalculateCost(basePrice, carType)).Returns(expectedCar);

            // Act
            var result = _controller.GetVehiclePrice(basePrice, carTypeStr).Result as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.StatusCode, Is.EqualTo(200));
            Assert.That(result.Value, Is.EqualTo(expectedCar));
        }
    }
}
