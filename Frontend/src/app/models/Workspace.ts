import { Document } from "./Document";

export interface Workspace {
  id: string;
  name: string;
  ownsWorkspace: boolean;
  documents: Document[];
}
