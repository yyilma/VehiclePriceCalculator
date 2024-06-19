import axiosInstance from '@/utils/axiosInstance';
import type { CarTypeResponse, VehiclePriceResponse } from '@/types/vehicle.ts';

export async function fetchCarTypes(): Promise<CarTypeResponse> {
    const response = await axiosInstance.get<CarTypeResponse>('/Vehicle/GetCarTypes');
    return response.data;
}

export async function fetchVehiclePrice(carType: string, basePrice: number): Promise<VehiclePriceResponse> {
    const response = await axiosInstance.get<VehiclePriceResponse>('/Vehicle/VehiclePrice', {
        params: {
            carType: carType,
            basePrice: basePrice,
        },
    });
    return response.data;
}
