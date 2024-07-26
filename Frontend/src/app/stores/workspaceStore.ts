import { makeAutoObservable, runInAction } from "mobx";
import { Workspace } from "../models/Workspace";
import agent from "../api/agent";

export default class WorkspaceStore {
  workspaces: Map<string, Workspace> = new Map();
  selectedWorkspace: Workspace | null = null;
  selectedDocument: Workspace | null = null;

  constructor() {
    makeAutoObservable(this);
  }

  loadWorkspaces = async () => {
    var result = await agent.Workspaces.list();
    runInAction(() =>
      result.forEach((res) => this.workspaces.set(res.id, res))
    );
  };

  clearWorkspaces = () => {
    this.workspaces = new Map();
  };
}
