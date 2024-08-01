import { Dialog, Group, TextInput, Button, Text } from "@mantine/core";
import { useDisclosure } from "@mantine/hooks";
import { useState } from "react";
import agent from "../../../api/agent";
import { useStore } from "../../../stores/store";

export default function useCreateWorkspaceDialog() {
  const [opened, { toggle, close }] = useDisclosure();
  const [value, setValue] = useState("");
  const {workspaceStore} = useStore();

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
          Create workspace information
        </Text>

        <Group align="flex-end">
          <TextInput
            placeholder="Name: ex MyWorkspace"
            style={{ flex: 1 }}
            value={value}
            onChange={(event) => setValue(event.currentTarget.value)}
          />
          <Button
            onClick={async () => {
              close();
              workspaceStore.addWorkspacce(await agent.Workspaces.create(value));
            }}
          >
            Create
          </Button>
        </Group>
      </Dialog>
    ),
  };
}
