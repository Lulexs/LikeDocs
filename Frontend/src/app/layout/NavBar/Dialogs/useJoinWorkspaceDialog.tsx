import { Dialog, Group, TextInput, Button, Text } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import { useState } from "react";

export default function useJoinWorkspaceDialog() {
  const [opened, { toggle, close }] = useDisclosure();
  const [value, setValue] = useState("");

  return {
    toggle: toggle,
    dialog: (
      <Dialog
        opened={opened}
        withCloseButton
        onClose={close}
        size="xl"
        radius="md"
      >
        <Text size="sm" mb="xs" fw={500}>
          Enter code to join workspace
        </Text>

        <Group align="flex-end">
          <TextInput
            placeholder="5aff2817-119c-4760-b9e2-cf622749de78"
            style={{ flex: 1 }}
            value={value}
            onChange={(event) => setValue(event.currentTarget.value)}
          />
          <Button
            onClick={() => {
              console.log("Hi from custom hook");
              close();
            }}
          >
            Join
          </Button>
        </Group>
      </Dialog>
    ),
  };
}
