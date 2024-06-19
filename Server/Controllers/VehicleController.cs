using VehiclePriceCalculator.Server.Constants;
using VehiclePriceCalculator.Server.Interface;
using VehiclePriceCalculator.Server.Model;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace VehiclePriceCalculator.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        private readonly ICarService _carService;

        public VehicleController(ICarService carService)
        {
            _carService = carService;
        }      

        // GET /Vehicle/GetVehiclePrice
        [HttpGet]
        [Route("GetCarTypes")]
        public IActionResult GetCarTypes()
        {
            var carTypes = Enum.GetNames(typeof(VehicleType)).ToList();
            var defaultCarType = VehicleType.Common.ToString(); // Specify the default car type
            var response = new VehicleTypesWithDefault
            {
                CarTypes = carTypes,
                DefaultCarType = defaultCarType
            };
            return Ok(response);
        }

        // GET /Vehicle/GetVehiclePrice
        [HttpGet]
        [Route("VehiclePrice")]
        public ActionResult<Vehicle> GetVehiclePrice([FromQuery] decimal basePrice, [FromQuery] string carType)
        {
            try
            {
                if (basePrice < 0)
                {
                    throw new Exception("Invalid vehicle price");
                }
                if (carType == null || !Enum.TryParse(carType, true, out VehicleType vehicleType))
                {
                    throw new InvalidEnumArgumentException("Invalid vehicle type");
                }

                return Ok(_carService.CalculateCost(basePrice, vehicleType));
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
