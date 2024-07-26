import { createContext, useContext } from "react";
import CommonStore from "./commonStore";
import EditStore from "./editStore";
import UserStore from "./userStore";
import WorkspaceStore from "./workspaceStore";

interface Store {
  userStore: UserStore;
  commonStore: CommonStore;
  editStore: EditStore;
  workspaceStore: WorkspaceStore;
}

export const store: Store = {
  commonStore: new CommonStore(),
  userStore: new UserStore(),
  editStore: new EditStore(),
  workspaceStore: new WorkspaceStore(),
};

export function useStore() {
  return useContext(createContext(store));
}
