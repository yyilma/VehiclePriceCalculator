using VehiclePriceCalculator.Server.Constants;
using VehiclePriceCalculator.Server.Model;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace VehiclePriceCalculator.Server.Constants
{
    public enum VehicleType
    {
        Common,
        Luxury
    }
    public static class Params
    {
        public static Dictionary<VehicleType, decimal> BasicFeeMin = new() { { VehicleType.Common, 10 }, { VehicleType.Luxury, 25 } };
        
        public static Dictionary<VehicleType, decimal> BasicFeeMax = new() { { VehicleType.Common, 50 }, { VehicleType.Luxury, 200 } };
        
        public static decimal BasicFeeRate = 0.1m;

        public static Dictionary<VehicleType, decimal> SpecialFeeRate = new() { { VehicleType.Common, 0.02m }, { VehicleType.Luxury, 0.04m } };

        public static Dictionary<decimal, decimal> AddedCostFloorFee = new() { { 3000, 20 }, { 1000, 15 }, { 500, 10 }, { 1, 5 } };

        public static decimal StorageFee = 100;
       
    }
}
