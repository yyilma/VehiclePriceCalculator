using VehiclePriceCalculator.Server.Constants;

namespace VehiclePriceCalculator.Server.Model
{
    public class Vehicle
    {
        public VehicleType Type { get; set; }

        public decimal BasePrice { get; set; }

        public decimal BasicFee { get; set; } 

        public decimal SpecialFee { get; set; }

        public decimal AddedCost { get; set; }

        public decimal StorageFee => 100;

        public decimal Price { get; set; }
    }
}
