import axios from "axios";
const API = axios.create({ baseURL: "https://localhost:7176/api/users" });

export const fetchUsers = () => API.get("/fetch-users");
export const createUser = (data) => API.post("/create-user", data);
