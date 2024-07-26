import { defineConfig } from "vite";
import react from "@vitejs/plugin-react-swc";
import { trackDerivedFunction } from "mobx/dist/internal";

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
});
