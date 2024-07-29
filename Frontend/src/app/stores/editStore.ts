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

    this.hubConnection.on("RecieveEdit", (edit: Edit) => {
      this.edits = this.edits.filter((x) => x.n >= edit.n);
      edit.diff = edit.diff.map((x) => {
        return { ...x, operation: toJSVersion(x.operation)! };
      });
      console.log(edit);
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
    if (this.clientText == null || this.clientShadow == null) return;
    const diffs = this.dmp.diff_main(this.clientShadow!, this.clientText!);
    this.edits.push({
      n: this.n!,
      m: this.m!,
      diff: diffs.map((x) => {
        return {
          operation: toCSharpVersion(x[0])!,
          text: x[1],
        };
      }),
    });
    this.n! += 1;
    this.clientShadow = this.clientText;

    this.hubConnection?.invoke("NewEdits", this.edits);
  };
}

function toJSVersion(arg0: number): number | undefined {
  switch (arg0) {
    case 0:
      return -1;
    case 1:
      return 1;
    case 2:
      return 0;
  }
}

function toCSharpVersion(arg0: number): number | undefined {
  switch (arg0) {
    case -1:
      return 0;
    case 1:
      return 1;
    case 0:
      return 2;
  }
}
