import { Diff } from "diff-match-patch";

export interface Edit {
  n: number;
  m: number;
  diff: Diff;
}
