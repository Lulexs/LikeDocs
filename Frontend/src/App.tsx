import { MantineProvider } from "@mantine/core";
import "./App.css";
import "@mantine/core/styles.css";

function App() {
  return (
    <MantineProvider>
      <h1>Hello world</h1>
    </MantineProvider>
  );
}

export default App;
