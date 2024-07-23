import { MantineProvider } from "@mantine/core";
import "@mantine/core/styles.css";
import TextEditor from "./features/TextEditor/TextEditor";

function App() {
  return (
    <MantineProvider
      theme={{
        fontFamily: "Verdana, sans-serif",
        fontFamilyMonospace: "Monaco, Courier, monospace",
        headings: { fontFamily: "Greycliff CF, sans-serif" },
      }}
    >
      <TextEditor />
    </MantineProvider>
  );
}

export default App;
