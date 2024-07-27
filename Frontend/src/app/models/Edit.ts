export interface Edit {
  n: number;
  m: number;
  diff: { operation: number; text: string }[];
}
