import { makeAutoObservable, runInAction } from "mobx";
import { User, UserLoginValues, UserRegisterValues } from "../models/User";
import agent from "../api/agent";
import { store } from "./store";
import { router } from "../routes/routes";

export default class UserStore {
  user: User | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  login = async (creds: UserLoginValues) => {
    const user = await agent.Account.login(creds);
    store.commonStore.setToken(user.token);
    runInAction(() => (this.user = user));
  };

  register = async (creds: UserRegisterValues) => {
    const user = await agent.Account.register(creds);
    store.commonStore.setToken(user.token);
    runInAction(() => (this.user = user));
  };

  logout = () => {
    store.commonStore.setToken(null);
    runInAction(() => (this.user = null));
    router.navigate("/");
  };
}