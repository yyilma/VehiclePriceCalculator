import { mount } from '@vue/test-utils';
import { describe, it, expect } from 'vitest';
import VehiclePriceCalculator from '@/components/VehiclePriceCalculator.vue';

describe('VehiclePriceCalculator.vue', () => {
    it('renders without errors', () => {
        const wrapper = mount(VehiclePriceCalculator);
        expect(wrapper.exists()).toBe(true);
    });
});