import { Flex } from "@mantine/core";
import NavbarNested from "./NavBar/NavBar";
import { useEffect } from "react";
import { useStore } from "../stores/store";
import TextEditor from "../../features/TextEditor/TextEditor";

export default function MainContainer() {
  const { commonStore, userStore } = useStore();

  useEffect(() => {
    if (commonStore.token) {
      userStore.getUser();
    }
  }, [commonStore, userStore]);

  return (
    <Flex>
      <NavbarNested />
      <TextEditor />
    </Flex>
  );
}
