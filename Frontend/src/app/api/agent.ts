import axios, { AxiosError, AxiosResponse } from "axios";
import { store } from "../stores/store";
import { User, UserLoginValues, UserRegisterValues } from "../models/User";
import { Workspace } from "../models/Workspace";

axios.defaults.baseURL = import.meta.env.VITE_SERVER_API_URL;

const responseBody = <T>(response: AxiosResponse<T>) => response.data;

axios.interceptors.request.use((config) => {
  const token = store.commonStore.token;
  if (token && config.headers) config.headers.Authorization = `Bearer ${token}`;
  return config;
});

axios.interceptors.response.use(
  (value) => value,
  (error: AxiosError) => {
    console.log(error);
  }
);

const requests = {
  get: <T>(url: string) => axios.get<T>(url).then(responseBody),
  post: <T>(url: string, body: {}) =>
    axios.post<T>(url, body).then(responseBody),
  put: <T>(url: string, body: {}) => axios.put<T>(url, body).then(responseBody),
  del: <T>(url: string) => axios.delete<T>(url).then(responseBody),
};

const Account = {
  login: (user: UserLoginValues) => requests.post<User>(`/account/login`, user),
  register: (user: UserRegisterValues) =>
    requests.post<User>(`/account/register`, user),
  getUser: () => requests.get<User>("/account/getUser"),
};

const Workspaces = {
  list: () => requests.get<Workspace[]>("/workspaces"),
};

const agent = {
  Account,
  Workspaces,
};

export default agent;
