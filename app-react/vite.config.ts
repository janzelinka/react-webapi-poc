import { defineConfig } from "vite";
import react from "@vitejs/plugin-react";

export const PORT = 5000;

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      "^/[A-Z]{1}[a-z]+": {
        target: `http://0.0.0.0:${PORT}`,
        secure: false,
      }, // Your API server address
    },
  },
});
