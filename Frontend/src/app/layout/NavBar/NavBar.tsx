import { Group, Code, ScrollArea, Paper, Flex } from "@mantine/core";
import {
  IconNotes,
  IconCalendarStats,
  IconGauge,
  IconPresentationAnalytics,
  IconFileAnalytics,
  IconAdjustments,
  IconLock,
} from "@tabler/icons-react";

import classes from "./NavBar.module.css";
import UserButton from "./UserButton/UserButton";
import { LinksGroup } from "./NavbarLinksGroup/NavbarLinksGroup";
import { Logo } from "./Logo/Logo";
import { observer } from "mobx-react-lite";
import { useStore } from "../../stores/store";

const mockdata = [
  { label: "Dashboard", icon: IconGauge },
  {
    label: "Market news",
    icon: IconNotes,
    initiallyOpened: true,
    links: [
      { label: "Overview", link: "/" },
      { label: "Forecasts", link: "/" },
      { label: "Outlook", link: "/" },
      { label: "Real time", link: "/" },
    ],
  },
  {
    label: "Releases",
    icon: IconCalendarStats,
    links: [
      { label: "Upcoming releases", link: "/" },
      { label: "Previous releases", link: "/" },
      { label: "Releases schedule", link: "/" },
    ],
  },
  { label: "Analytics", icon: IconPresentationAnalytics },
  { label: "Contracts", icon: IconFileAnalytics },
  { label: "Settings", icon: IconAdjustments },
  {
    label: "Security",
    icon: IconLock,
    links: [
      { label: "Enable 2FA", link: "/" },
      { label: "Change password", link: "/" },
      { label: "Recovery codes", link: "/" },
    ],
  },
];

export default observer(function NavbarNested() {
  const { userStore } = useStore();

  const links = mockdata.map((item) => (
    <LinksGroup {...item} key={item.label} />
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
        <ScrollArea className={classes.links}>
          <div className={classes.linksInner}>{links}</div>
        </ScrollArea>
      ) : (
        <Flex flex="1" justify="center" align="center">
          <Paper ta="center" flex="1">
            Sign in to continue
          </Paper>
        </Flex>
      )}

      <div className={classes.footer}>
        <UserButton />
      </div>
    </nav>
  );
});
