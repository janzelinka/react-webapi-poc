import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export const PORT = 7152;

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      "/Login": {
        target: `https://localhost:${PORT}`,
        secure: false,
      }, // Your API server address
      "/Auth": {
        target: `https://localhost:${PORT}`,
        secure: false,
      }, // Your A
    },
  },
});
