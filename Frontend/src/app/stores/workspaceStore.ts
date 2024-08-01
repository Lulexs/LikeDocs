import { makeAutoObservable, runInAction } from "mobx";
import { Workspace } from "../models/Workspace";
import agent from "../api/agent";
import { Document } from "../models/Document";
import { store } from "./store";

export default class WorkspaceStore {
  workspaces: Map<string, Workspace> = new Map();
  selectedWorkspace: Workspace | undefined;
  selectedDocument: Document | undefined;

  constructor() {
    makeAutoObservable(this);
  }

  loadWorkspaces = async () => {
    var result = await agent.Workspaces.list();
    runInAction(() =>
      result.forEach((res) => this.workspaces.set(res.id, res))
    );
  };

  selectDocument = (workspaceId: string, docId: string) => {
    this.selectedWorkspace = this.workspaces.get(workspaceId);
    this.selectedDocument = this.selectedWorkspace?.documents.find(
      (x) => x.id == docId
    );
    if (this.selectedDocument) {
      store.editStore.createHubConnection(docId);
    }
  };

  clearWorkspaces = () => {
    this.workspaces = new Map();
  };

  addWorkspacce = (workspace: Workspace) => {
    runInAction(() => this.workspaces.set(workspace.id, workspace));
  }

  removeWorkspace = (id: string) => {
    runInAction(() => this.workspaces.delete(id));
  }
}
