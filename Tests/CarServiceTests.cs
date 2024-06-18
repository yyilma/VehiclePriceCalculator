using VehiclePriceCalculator.Server.Constants;
using VehiclePriceCalculator.Server.Model;
using VehiclePriceCalculator.Server.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace VehiclePriceCalculator.Server.Tests.Services
{
    [TestFixture]
    public class CarServiceTests
    {
        private CarService _carService;

        [SetUp]
        public void SetUp()
        {
            _carService = new CarService();
        }

        [Test]
        public void CalculateCost_ShouldThrowArgumentException_ForNegativeBasePrice()
        {
            // Arrange
            decimal basePrice = -1000;
            VehicleType carType = VehicleType.Common;

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _carService.CalculateCost(basePrice, carType));
            Assert.That(ex.Message, Is.EqualTo("Base price cannot be negative"));
        }

        [Test]
        public void CalculateCost_ShouldReturnCorrectCar_ForValidBasePriceAndType_Common()
        {
            // Arrange
            decimal basePrice = 10000;
            VehicleType carType = VehicleType.Common;

            // Act
            var car = _carService.CalculateCost(basePrice, carType);

            // Assert
            Assert.That(car.BasePrice, Is.EqualTo(basePrice));
            Assert.That(car.Type, Is.EqualTo(carType));
            Assert.That(car.BasicFee, Is.EqualTo(Math.Min(Params.BasicFeeMax[carType], Math.Max(Params.BasicFeeMin[carType], Params.BasicFeeRate * car.BasePrice))));
            Assert.That(car.SpecialFee, Is.EqualTo(Params.SpecialFeeRate[carType] * car.BasePrice));
            Assert.That(car.AddedCost, Is.EqualTo(Params.AddedCostFloorFee.OrderByDescending(x => x.Key).FirstOrDefault(x => car.BasePrice > x.Key).Value));
            Assert.That(car.Price, Is.EqualTo(car.BasePrice + car.BasicFee + car.SpecialFee + car.AddedCost + Params.StorageFee));
        }

        [Test]
        public void CalculateCost_ShouldReturnCorrectCar_ForValidBasePriceAndType_Luxury()
        {
            // Arrange
            decimal basePrice = 10000;
            VehicleType carType = VehicleType.Luxury;

            // Act
            var car = _carService.CalculateCost(basePrice, carType);

            // Assert
            Assert.That(car.BasePrice, Is.EqualTo(basePrice));
            Assert.That(car.Type, Is.EqualTo(carType));
            Assert.That(car.BasicFee, Is.EqualTo(Math.Min(Params.BasicFeeMax[carType], Math.Max(Params.BasicFeeMin[carType], Params.BasicFeeRate * car.BasePrice))));
            Assert.That(car.SpecialFee, Is.EqualTo(Params.SpecialFeeRate[carType] * car.BasePrice));
            Assert.That(car.AddedCost, Is.EqualTo(Params.AddedCostFloorFee.OrderByDescending(x => x.Key).FirstOrDefault(x => car.BasePrice > x.Key).Value));
            Assert.That(car.Price, Is.EqualTo(car.BasePrice + car.BasicFee + car.SpecialFee + car.AddedCost + Params.StorageFee));
        }

        [Test]
        public void CalculateCost_ShouldReturnCorrectCar_ForEdgeCaseBasePrice()
        {
            // Arrange
            decimal basePrice = 3000;
            VehicleType carType = VehicleType.Common;

            // Act
            var car = _carService.CalculateCost(basePrice, carType);

            // Assert
            Assert.That(car.BasePrice, Is.EqualTo(basePrice));
            Assert.That(car.Type, Is.EqualTo(carType));
            Assert.That(car.BasicFee, Is.EqualTo(Math.Min(Params.BasicFeeMax[carType], Math.Max(Params.BasicFeeMin[carType], Params.BasicFeeRate * car.BasePrice))));
            Assert.That(car.SpecialFee, Is.EqualTo(Params.SpecialFeeRate[carType] * car.BasePrice));
            Assert.That(car.AddedCost, Is.EqualTo(Params.AddedCostFloorFee.OrderByDescending(x => x.Key).FirstOrDefault(x => car.BasePrice > x.Key).Value));
            Assert.That(car.Price, Is.EqualTo(car.BasePrice + car.BasicFee + car.SpecialFee + car.AddedCost + Params.StorageFee));
        }
    }
}
