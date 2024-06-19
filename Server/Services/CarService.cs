using VehiclePriceCalculator.Server.Constants;
using VehiclePriceCalculator.Server.Interface;
using VehiclePriceCalculator.Server.Model;

namespace VehiclePriceCalculator.Server.Services
{
    public class CarService : ICarService
    {
        public Vehicle CalculateCost(decimal basePrice, VehicleType carType)
        {

            if(basePrice < 0)
            {
                throw new ArgumentException("Base price cannot be negative");
            }
            var car = new Vehicle()
            {
                BasePrice = basePrice,
                Type = carType
            };

            car.BasicFee = Math.Min(Params.BasicFeeMax[carType], Math.Max(Params.BasicFeeMin[carType], Params.BasicFeeRate * car.BasePrice));
            car.SpecialFee = Params.SpecialFeeRate[carType] * car.BasePrice;

            car.AddedCost = Params.AddedCostFloorFee.OrderByDescending(x => x.Key).FirstOrDefault(x => car.BasePrice > x.Key).Value;         
           
            car.Price = car.BasePrice + car.BasicFee + car.SpecialFee + car.AddedCost + car.StorageFee;

            return car;
        }
    }
}
