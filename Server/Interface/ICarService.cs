using VehiclePriceCalculator.Server.Constants;
using VehiclePriceCalculator.Server.Model;

namespace VehiclePriceCalculator.Server.Interface
{
    public interface ICarService
    {
        public Vehicle CalculateCost(decimal basePrice, VehicleType carType);
    }
}
