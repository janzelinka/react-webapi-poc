import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export const PORT = 5000;

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      "/Login": {
        target: `http://0.0.0.0:${PORT}`,
        secure: false,
      }, // Your API server address
      "/Auth": {
        target: `http://0.0.0.0:${PORT}`,
        secure: false,
      }, // Your A
      "/Enum": {
        target: `http://0.0.0.0:${PORT}`,
        secure: false,
      }, // Your A
    },
  },
});
