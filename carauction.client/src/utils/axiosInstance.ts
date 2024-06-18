// src/utils/axiosInstance.ts

import axios from 'axios';

const axiosInstance = axios.create({
    baseURL: 'https://localhost:7214', 
});

export default axiosInstance;