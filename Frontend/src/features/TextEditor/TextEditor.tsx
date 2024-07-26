import { Flex, Textarea } from "@mantine/core";
import { useState } from "react";
import { selectToEdit, signinToEdit } from "../../app/layout/constants";
import { useStore } from "../../app/stores/store";
import { observer } from "mobx-react-lite";

export default observer(function TextEditor() {
  const {userStore, workspaceStore } = useStore();
  const [value, setValue] = useState('');

  const generateLineNumbers = (text: string) => {
    const lines = text.split("\n").length;
    return Array.from({ length: lines }, (_, i) => i + 1).join("\n");
  };

  const numbering = generateLineNumbers(value);

  return (
    <Flex m="20px" w="98%">
      <Textarea
        minRows={50}
        maxRows={500}
        value={numbering}
        disabled
        autosize
        w="50px"
        mr="0"
        styles={{ input: { textAlign: "right" } }}
      />
      <Textarea
        value={value}
        onChange={(event) => setValue(event.currentTarget.value)}
        minRows={50}
        autosize
        maxRows={500}
        disabled={!userStore.isLoggedIn || !workspaceStore.selectedDocument}
        placeholder={!userStore.isLoggedIn ? signinToEdit : selectToEdit}
        ml="0"
        flex={1}
      />
    </Flex>
  );
})
