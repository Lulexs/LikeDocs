import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";
import { makeAutoObservable } from "mobx";
import { Edit } from "../models/Edit";
import diff_match_patch from "diff-match-patch";

export default class EditStore {
  hubConnection: HubConnection | null | undefined = null;
  clientText: string | null = null;
  clientShadow: string | null = null;
  backupShadow: string | null = null;
  n: number | null = null;
  m: number | null = null;
  backupShadowN: number | null = null;
  dmp: diff_match_patch = new diff_match_patch();

  constructor() {
    makeAutoObservable(this);
  }

  createHubConnection = (
    workspaceId: string = "abc",
    docId: string = "123"
  ) => {
    // TODO: check if user is inside some workspace
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(
        `${
          import.meta.env.VITE_SERVER_HUB_URL
        }?$workspaceId=${workspaceId}&docId=${docId}`
      )
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .catch((error) =>
        console.error("Error establishing connection: ", error)
      );

    this.hubConnection.on("GetInitialState", (initialContent: string) => {
      this.clientText = initialContent;
      this.clientShadow = initialContent;
      this.backupShadow = initialContent;
      this.n = 0;
      this.m = 0;
      this.backupShadowN = 0;
    });

    this.hubConnection.on("RecieveEdit", (edits: Edit[]) => {
      
    });
  };
}
