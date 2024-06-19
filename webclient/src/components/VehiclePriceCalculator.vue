<script lang="ts">
    import { defineComponent } from 'vue';
    import { fetchCarTypes, fetchVehiclePrice } from '@/services/vehicle';
    import type { CarTypeResponse, VehiclePriceResponse } from '@/types/vehicle';

    export default defineComponent({
        data() {
            return {
                carType: '',
                basePrice: 0,
                basicFee: 0,
                specialFee: 0,
                addedCost: 0,
                storageFee: 0,
                price: 0,
                carTypes: [] as string[],
                loading: false,
            };
        },
        watch: {
            carType: 'fetchVehiclePrice',
            basePrice: 'fetchVehiclePrice',
        },
        methods: {
            async fetchCarTypes(): Promise<void> {
              
                await fetchCarTypes().
                        then((response: CarTypeResponse) => {
                            this.carTypes = response.carTypes;
                            this.carType = response.defaultCarType; // Set the default car type
                        })
                        .catch ((error) => {
                            console.error('Error fetching car types:', error);
                        });
            },
            async fetchVehiclePrice() {
                if (!this.carType || this.basePrice === 0) return; // Ensure values are set before fetching

                this.loading = true;
           
                await fetchVehiclePrice(this.carType, this.basePrice)
                        .then((response: VehiclePriceResponse) => {
                            this.basicFee = response.basicFee;
                            this.specialFee = response.specialFee;
                            this.addedCost = response.addedCost;
                            this.storageFee = response.storageFee;
                            this.price = response.price;
                        })
                        .catch((error) => {
                            console.error('Error fetching vehicle price:', error);
                        })
                        .finally(() => {
                            this.loading = false;
                        });
            },
            formatCurrency(value: number): string {
                return new Intl.NumberFormat('en-US', {
                    style: 'currency',
                    currency: 'USD',
                }).format(value);
            },
        },
        created() {
            this.fetchCarTypes(); // Fetch car types when the component is created
        },
    });
</script>

<template>
    <div class="heading">
        <h2>Vehicle Price Calculator</h2>     
    </div>
    <div class="car-price-calculator">
        <table class="custom-table">
            <tr>
                <td><label for="carType">Car Type:</label></td>
                <td>
                    <div class="custom-select-wrapper">
                        <select id="carType" v-model="carType" class="custom-select">
                            <option v-for="type in carTypes" :key="type" :value="type">{{ type }}</option>
                        </select>
                    </div>
                </td>
            </tr>
            <tr>
                <td><label for="basePrice">Car Base Price:</label></td>
                <td>
                    <input type="number"
                           id="basePrice"
                           v-model.number="basePrice"
                           class="custom-input"
                           min="0"
                           required />
                </td>
            </tr>
            <tr>
                <td><label>Basic Fee:</label></td>
                <td><span>{{ formatCurrency(basicFee) }}</span></td>
            </tr>
            <tr>
                <td><label>Special Fee:</label></td>
                <td><span>{{ formatCurrency(specialFee) }}</span></td>
            </tr>
            <tr>
                <td><label>Added Cost:</label></td>
                <td><span>{{ formatCurrency(addedCost) }}</span></td>
            </tr>
            <tr>
                <td><label>Storage Fee:</label></td>
                <td><span>{{ formatCurrency(storageFee) }}</span></td>
            </tr>
            <tr>
                <td><label>Grand Total:</label></td>
                <td><span>{{ formatCurrency(price) }}</span></td>
            </tr>
        </table>
    </div>
</template>

<style scoped>
    .heading {
        text-align:center;
        font-weight:400;
        margin-bottom:20px;
    }

    .custom-table {
        width: 100%;
        border-collapse: separate;
        border-spacing: 10px;
    }

    .car-price-calculator {
        max-width: 500px;
        margin: 0 auto;
        padding: 20px;
        border: 1px solid #ccc;
        border-radius: 10px;
        gap: 10px;
    }

    .custom-select-wrapper {
        position: relative;
    }

    .custom-select,
    .custom-input {
        width: 100%;
        height: 40px;
        padding: 0 10px;
        font-size: 16px;
        line-height: 1.5;
        color: #444;
        background-color: #fff;
        border: 1px solid #ccc;
        border-radius: 4px;
        appearance: none;
        -webkit-appearance: none;
        -moz-appearance: none;
    }

    .custom-select:focus,
    .custom-input:focus {
        border-color: #007bff;
        outline: 0;
    }

    .custom-select-wrapper::after {
        content: '\25BC';
        position: absolute;
        top: 50%;
        right: 10px;
        pointer-events: none;
        transform: translateY(-50%);
        color: #999;
    }
</style>