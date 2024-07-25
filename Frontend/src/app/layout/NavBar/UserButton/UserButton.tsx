import { UnstyledButton, Group, Avatar, Text, rem } from "@mantine/core";
import { IconChevronRight } from "@tabler/icons-react";
import classes from "./UserButton.module.css";
import { observer } from "mobx-react-lite";
import { useStore } from "../../../stores/store";

export default observer(function UserButton() {
  const { userStore } = useStore();

  return (
    <UnstyledButton className={classes.user}>
      <Group>
        <Avatar radius="xl" />

        {!userStore.isLoggedIn ? (
          <div
            style={{ flex: 1 }}
            onClick={(event) => {
              event.stopPropagation();
            }}
          >
            <Text size="md">Sign in / Sign up</Text>
          </div>
        ) : (
          <div style={{ flex: 1 }}>
            <Text size="sm" fw={500}>
              {userStore.user?.username}
            </Text>

            <Text c="dimmed" size="xs">
              {userStore.user?.email}
            </Text>
          </div>
        )}

        <IconChevronRight
          style={{ width: rem(14), height: rem(14) }}
          stroke={1.5}
        />
      </Group>
    </UnstyledButton>
  );
});
