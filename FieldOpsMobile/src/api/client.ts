import axios from "axios";
import AsyncStorage from "@react-native-async-storage/async-storage";

const API_URL = "http://10.0.2.2:5000";

const api = axios.create({
  baseURL: API_URL,
  headers: { "Content-Type": "application/json" },
});

api.interceptors.request.use(async (config) => {
  const token = await AsyncStorage.getItem("token");
  if (token) {
    config.headers.Authorization = `Bearer ${token}`;
  }
  return config;
});

export const authApi = {
  login: (email: string, password: string) =>
    api.post("/api/account/login", { email, password }),
};

export const workOrdersApi = {
  getAll: () => api.get("/api/workorders"),
  getById: (id: string) => api.get(`/api/workorders/${id}`),
  create: (data: Record<string, unknown>) => api.post("/api/workorders", data),
  assignTechnician: (id: string, technicianId: string) =>
    api.put(`/api/workorders/${id}/assign`, { technicianId }),
  updateStatus: (id: string, status: string) =>
    api.put(`/api/workorders/${id}/status`, { status }),
};

export default api;
