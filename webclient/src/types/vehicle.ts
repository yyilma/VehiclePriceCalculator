export type CarTypeResponse = {
    carTypes: string[];
    defaultCarType: string;
};

export type VehiclePriceResponse = {
    basicFee: number;
    specialFee: number;
    addedCost: number;
    storageFee: number;
    price: number;
};
