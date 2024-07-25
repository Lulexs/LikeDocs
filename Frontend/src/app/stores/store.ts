import { createContext, useContext } from "react";
import CommonStore from "./commonStore";
import EditStore from "./editStore";
import UserStore from "./userStore";

interface Store {
    userStore: UserStore;
    commonStore: CommonStore;
    editStore: EditStore;
}

export const store: Store = {
    commonStore: new CommonStore(),
    userStore: new UserStore(),
    editStore: new EditStore()
}

export function useStore() {
    return useContext(createContext(store));
}