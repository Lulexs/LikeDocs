import { Group, Code, ScrollArea, Paper, Flex } from "@mantine/core";
import {
  IconFolder,
  IconFile,
  IconFolderStar,
  IconFolderPlus,
  IconJoinBevel,
} from "@tabler/icons-react";

import classes from "./NavBar.module.css";
import UserButton from "./UserButton/UserButton";
import { LinksGroup } from "./NavbarLinksGroup/NavbarLinksGroup";
import { Logo } from "./Logo/Logo";
import { observer } from "mobx-react-lite";
import { useStore } from "../../stores/store";
import { useLocation } from "react-router-dom";
import { LoginPage } from "./Auth/LoginPage";
import { RegisterPage } from "./Auth/RegisterPage";
import { useContextMenu } from "mantine-contextmenu";

export default observer(function NavbarNested() {
  const { userStore, workspaceStore } = useStore();
  const location = useLocation();

  const { showContextMenu } = useContextMenu();

  const links = [...workspaceStore.workspaces.values()].map((item) => (
    <LinksGroup
      {...{
        workspaceId: item.id,
        label: item.name,
        icon: item.ownsWorkspace ? IconFolderStar : IconFolder,
        links: item.documents.map((d) => {
          return {
            label: d.name,
            link: "/",
            icon: IconFile,
            docId: d.id,
          };
        }),
      }}
      key={item.id}
    />
  ));

  return (
    <nav className={classes.navbar}>
      <div className={classes.header}>
        <Group justify="space-between">
          <Logo />
          <Code fw={700}>v1.0.0</Code>
        </Group>
      </div>

      {userStore.isLoggedIn ? (
        <ScrollArea
          onContextMenu={showContextMenu([
            {
              key: "new-workspace",
              icon: <IconFolderPlus size={25} />,
              title: "New workspace",
              onClick: () => console.log("Hi"),
            },
            {
              key: "join-workspace",
              icon: <IconJoinBevel size={25} />,
              title: "Join workspace",
              onClick: () => console.log("Hi"),
            },
          ])}
          className={classes.links}
        >
          <div className={classes.linksInner}>{links}</div>
        </ScrollArea>
      ) : (
        <Flex flex="1" justify="center" align="center">
          {location.pathname == "/" && (
            <Paper ta="center" flex="1">
              Sign in to continue
            </Paper>
          )}
          {location.pathname == "/signin" && <LoginPage />}
          {location.pathname == "/signup" && <RegisterPage />}
        </Flex>
      )}

      <div className={classes.footer}>
        <UserButton />
      </div>
    </nav>
  );
});
