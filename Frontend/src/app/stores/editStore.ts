import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";
import { makeAutoObservable } from "mobx";

export default class EditStore {
  hubConnection: HubConnection | null | undefined = null;

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
  };
}
