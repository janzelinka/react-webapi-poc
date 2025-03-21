// vite.config.ts
import { defineConfig } from "file:///home/zelo/Desktop/react-webapi-poc/app-react/node_modules/vite/dist/node/index.js";
import react from "file:///home/zelo/Desktop/react-webapi-poc/app-react/node_modules/@vitejs/plugin-react/dist/index.mjs";
var PORT = 7152;
var vite_config_default = defineConfig({
  plugins: [react()],
  server: {
    proxy: {
      "/Login": {
        target: `https://localhost:${PORT}`,
        secure: false
      },
      // Your API server address
      "/Auth": {
        target: `https://localhost:${PORT}`,
        secure: false
      }
      // Your A
    }
  }
});
export {
  PORT,
  vite_config_default as default
};
//# sourceMappingURL=data:application/json;base64,ewogICJ2ZXJzaW9uIjogMywKICAic291cmNlcyI6IFsidml0ZS5jb25maWcudHMiXSwKICAic291cmNlc0NvbnRlbnQiOiBbImNvbnN0IF9fdml0ZV9pbmplY3RlZF9vcmlnaW5hbF9kaXJuYW1lID0gXCIvaG9tZS96ZWxvL0Rlc2t0b3AvcmVhY3Qtd2ViYXBpLXBvYy9hcHAtcmVhY3RcIjtjb25zdCBfX3ZpdGVfaW5qZWN0ZWRfb3JpZ2luYWxfZmlsZW5hbWUgPSBcIi9ob21lL3plbG8vRGVza3RvcC9yZWFjdC13ZWJhcGktcG9jL2FwcC1yZWFjdC92aXRlLmNvbmZpZy50c1wiO2NvbnN0IF9fdml0ZV9pbmplY3RlZF9vcmlnaW5hbF9pbXBvcnRfbWV0YV91cmwgPSBcImZpbGU6Ly8vaG9tZS96ZWxvL0Rlc2t0b3AvcmVhY3Qtd2ViYXBpLXBvYy9hcHAtcmVhY3Qvdml0ZS5jb25maWcudHNcIjtpbXBvcnQgeyBkZWZpbmVDb25maWcgfSBmcm9tIFwidml0ZVwiO1xuaW1wb3J0IHJlYWN0IGZyb20gXCJAdml0ZWpzL3BsdWdpbi1yZWFjdFwiO1xuXG5leHBvcnQgY29uc3QgUE9SVCA9IDcxNTI7XG5cbi8vIGh0dHBzOi8vdml0ZWpzLmRldi9jb25maWcvXG5leHBvcnQgZGVmYXVsdCBkZWZpbmVDb25maWcoe1xuICBwbHVnaW5zOiBbcmVhY3QoKV0sXG4gIHNlcnZlcjoge1xuICAgIHByb3h5OiB7XG4gICAgICBcIi9Mb2dpblwiOiB7XG4gICAgICAgIHRhcmdldDogYGh0dHBzOi8vbG9jYWxob3N0OiR7UE9SVH1gLFxuICAgICAgICBzZWN1cmU6IGZhbHNlLFxuICAgICAgfSwgLy8gWW91ciBBUEkgc2VydmVyIGFkZHJlc3NcbiAgICAgIFwiL0F1dGhcIjoge1xuICAgICAgICB0YXJnZXQ6IGBodHRwczovL2xvY2FsaG9zdDoke1BPUlR9YCxcbiAgICAgICAgc2VjdXJlOiBmYWxzZSxcbiAgICAgIH0sIC8vIFlvdXIgQVxuICAgIH0sXG4gIH0sXG59KTtcbiJdLAogICJtYXBwaW5ncyI6ICI7QUFBeVQsU0FBUyxvQkFBb0I7QUFDdFYsT0FBTyxXQUFXO0FBRVgsSUFBTSxPQUFPO0FBR3BCLElBQU8sc0JBQVEsYUFBYTtBQUFBLEVBQzFCLFNBQVMsQ0FBQyxNQUFNLENBQUM7QUFBQSxFQUNqQixRQUFRO0FBQUEsSUFDTixPQUFPO0FBQUEsTUFDTCxVQUFVO0FBQUEsUUFDUixRQUFRLHFCQUFxQixJQUFJO0FBQUEsUUFDakMsUUFBUTtBQUFBLE1BQ1Y7QUFBQTtBQUFBLE1BQ0EsU0FBUztBQUFBLFFBQ1AsUUFBUSxxQkFBcUIsSUFBSTtBQUFBLFFBQ2pDLFFBQVE7QUFBQSxNQUNWO0FBQUE7QUFBQSxJQUNGO0FBQUEsRUFDRjtBQUNGLENBQUM7IiwKICAibmFtZXMiOiBbXQp9Cg==
