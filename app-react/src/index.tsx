import { Button } from "@mui/material";
import React from "react";
import ReactDOM from "react-dom";
import { createRoot } from "react-dom/client";
import Dashboard from "./Dashboard";

const App: React.FC = () => {
  return <Dashboard />;
};

const root = createRoot(document.getElementById("root")!);
root.render(
  <React.StrictMode>
    <Dashboard />
  </React.StrictMode>
);
