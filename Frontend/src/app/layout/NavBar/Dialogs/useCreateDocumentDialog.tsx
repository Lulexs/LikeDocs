import { Dialog, Group, TextInput, Button, Text } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import { useState } from "react";
import agent from "../../../api/agent";
import { useStore } from "../../../stores/store";

export default function useCreateDocumentDialog() {
  const [opened, { toggle, close }] = useDisclosure();
  const [value, setValue] = useState("");
  const { workspaceStore } = useStore();
  const [workspaceId, setWorkspaceId] = useState("");

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
          Create document information
        </Text>

        <Group align="flex-end">
          <TextInput
            placeholder="Name: ex MyDocument"
            style={{ flex: 1 }}
            value={value}
            onChange={(event) => setValue(event.currentTarget.value)}
          />
          <Button
            onClick={() => {
              close();
              agent.Documents.create(workspaceId, value).then((value) =>
                workspaceStore.addDocument(workspaceId, value)
              );
            }}
          >
            Create
          </Button>
        </Group>
      </Dialog>
    ),
    setWorkspaceId: setWorkspaceId,
  };
}
