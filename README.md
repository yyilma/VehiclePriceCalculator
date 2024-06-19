# Overview

This application allows a buyer to calculate the total price of a vehicle (common or luxury) at a car auction. The buyer must pay various fees for the transaction, all of which are calculated on the base price amount. 

This SPA is implemented using .NET, C# and Vue.js. The server side consists of an API with two endpoints:

`GET
/Vehicle/GetCarTypes
`
which returns the list of vehicle types along with the default type:
```{
  "carTypes": [
    "Common",
    "Luxury"
  ],
  "defaultCarType": "Common"
}
```

`GET
/Vehicle/VehiclePrice
`
takes 2 parameters in the query string:

`basePrice (number)` and  `carType (string)` and returns the following json objet:

```
{
  "type": 0,
  "basePrice": 0,
  "basicFee": 0,
  "specialFee": 0,
  "addedCost": 0,
  "storageFee": 0,
  "price": 0
}
```

 Client side uses a single component `VehiclePriceCalculator` that encapsulates the fees structure. Fees are dynamically computed when either vehicle type or base price change.
