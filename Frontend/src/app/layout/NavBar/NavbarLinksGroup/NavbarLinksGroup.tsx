import { useState } from "react";
import {
  Group,
  Box,
  Collapse,
  ThemeIcon,
  Text,
  UnstyledButton,
  rem,
} from "@mantine/core";
import {
  IconChevronRight,
  IconCode,
  IconFileMinus,
  IconFilePlus,
  IconFolderMinus,
} from "@tabler/icons-react";
import classes from "./NavbarLinksGroup.module.css";
import { useContextMenu } from "mantine-contextmenu";
import { useStore } from "../../../stores/store";
import { notifications } from "@mantine/notifications";
import agent from "../../../api/agent";
import useCreateDocumentDialog from "../Dialogs/useCreateDocumentDialog";

interface LinksGroupProps {
  icon: React.FC<any>;
  label: string;
  initiallyOpened?: boolean;
  links?: { label: string; link: string; docId: string }[];
  workspaceId: string;
}

export function LinksGroup({
  icon: Icon,
  workspaceId,
  label,
  initiallyOpened,
  links,
}: LinksGroupProps) {
  const hasLinks = Array.isArray(links);
  const [opened, setOpened] = useState(initiallyOpened || false);
  const { showContextMenu } = useContextMenu();
  const { workspaceStore } = useStore();
  const { toggle, setWorkspaceId, dialog } = useCreateDocumentDialog();

  const items = (hasLinks ? links : []).map((link) => (
    <Text<"a">
      component="a"
      className={classes.link}
      href={link.link}
      key={link.label}
      onClick={(event) => {
        event.preventDefault();
        workspaceStore.selectDocument(workspaceId, link.docId);
      }}
      onContextMenu={showContextMenu([
        {
          key: "delete-document",
          icon: <IconFileMinus size={25} />,
          title: "Delete document",
          onClick: () => {
            agent.Documents.delete(link.docId).then(() =>
              workspaceStore.removeDocument(workspaceId, link.docId)
            );
          },
        },
      ])}
    >
      {link.label}.py
    </Text>
  ));

  return (
    <>
      <UnstyledButton
        onClick={() => setOpened((o) => !o)}
        onContextMenu={showContextMenu([
          {
            key: "new-document",
            icon: <IconFilePlus size={25} />,
            title: "New document",
            onClick: () => {
              setWorkspaceId(workspaceId);
              toggle();
            },
          },
          {
            key: "delete-workspace",
            icon: <IconFolderMinus size={25} />,
            title: "Delete workspace",
            onClick: () => {
              agent.Workspaces.delete(workspaceId).then(() =>
                workspaceStore.removeWorkspace(workspaceId)
              );
            },
          },
          {
            key: "show-code",
            icon: <IconCode size={25} />,
            title: "Show code",
            onClick: () => {
              notifications.show({
                title: "Code",
                message: `This workspaces code is ${workspaceId}`,
              });
            },
          },
        ])}
        className={classes.control}
      >
        <Group justify="space-between" gap={0}>
          <Box style={{ display: "flex", alignItems: "center" }}>
            <ThemeIcon variant="light" size={30}>
              <Icon style={{ width: rem(18), height: rem(18) }} />
            </ThemeIcon>
            <Box ml="md">{label}</Box>
          </Box>
          {hasLinks && (
            <IconChevronRight
              className={classes.chevron}
              stroke={1.5}
              style={{
                width: rem(16),
                height: rem(16),
                transform: opened ? "rotate(-90deg)" : "none",
              }}
            />
          )}
        </Group>
      </UnstyledButton>
      {hasLinks ? <Collapse in={opened}>{items}</Collapse> : null}
      {dialog}
    </>
  );
}
