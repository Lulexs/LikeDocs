import {
  HubConnection,
  HubConnectionBuilder,
  LogLevel,
} from "@microsoft/signalr";
import { makeAutoObservable, runInAction } from "mobx";
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
  edits: Edit[] = [];
  numOfEdits: number = 0;

  constructor() {
    makeAutoObservable(this);
  }

  createHubConnection = (docId: string) => {
    // TODO: check if user is inside some workspace
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${import.meta.env.VITE_SERVER_HUB_URL}?docId=${docId}`)
      .withAutomaticReconnect()
      .configureLogging(LogLevel.Information)
      .build();

    this.hubConnection
      .start()
      .catch((error) =>
        console.error("Error establishing connection: ", error)
      );

    this.hubConnection.on("GetInitialState", (initialContent: string) => {
      console.log(initialContent);
      runInAction(() => {
        this.clientText = initialContent;
        this.clientShadow = initialContent;
        this.backupShadow = initialContent;
        this.n = 0;
        this.m = 0;
        this.backupShadowN = 0;
      });
    });

    this.hubConnection.on("RecieveEdit", (edits: Edit[]) => {
      edits.forEach((x) => console.log(x));
    });

    this.hubConnection.on("ErrorEstablishingConnection", () => {
      this.stopHubConnection();
    });
  };

  stopHubConnection = () => {
    this.hubConnection
      ?.stop()
      .catch((error) => console.log("Error stopping connection: ", error));
  };

  setClientTextFromEditor = (txt: string) => {
    this.clientText = txt;
  };

  syncClientShadow = () => {
    const diffs = this.dmp.diff_main(this.clientText!, this.clientShadow!);
    this.edits.push({
      n: this.n!,
      m: this.m!,
      diff: diffs,
    });
    this.n! += 1;
    this.clientShadow = this.clientText;

    this.hubConnection?.invoke("NewEdits", this.edits);
  };
}
