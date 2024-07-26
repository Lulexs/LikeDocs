import { MantineProvider } from "@mantine/core";
import "@mantine/core/styles.css";
import { RouterProvider } from "react-router-dom";
import { router } from "./app/routes/routes";
import "@mantine/core/styles.layer.css";
import "mantine-contextmenu/styles.layer.css";
import "./layout.css";
import { ContextMenuProvider } from "mantine-contextmenu";

function App() {
  return (
    <MantineProvider
      theme={{
        fontFamily: "Verdana, sans-serif",
        fontFamilyMonospace: "Monaco, Courier, monospace",
        headings: { fontFamily: "Greycliff CF, sans-serif" },
      }}
    >
      <ContextMenuProvider>
        <RouterProvider router={router} />
      </ContextMenuProvider>
    </MantineProvider>
  );
}

export default App;
