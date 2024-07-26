import { Flex, Textarea } from "@mantine/core";
import { selectToEdit, signinToEdit } from "../../app/layout/constants";
import { useStore } from "../../app/stores/store";
import { observer } from "mobx-react-lite";
import { useEffect, useState } from "react";

export default observer(function TextEditor() {
  const { userStore, workspaceStore, editStore } = useStore();
  const [debounceTimeout, setDebounceTimeout] = useState<number | null>(null);

  const generateLineNumbers = (text: string) => {
    const lines = text.split("\n").length;
    return Array.from({ length: lines }, (_, i) => i + 1).join("\n");
  };

  const numbering = generateLineNumbers(editStore.clientText ?? "");

  useEffect(() => {
    if (debounceTimeout) {
      clearTimeout(debounceTimeout);
    }
    const newTimeout = setTimeout(() => {
      editStore.syncClientShadow();
    }, 3000);

    setDebounceTimeout(newTimeout);

    return () => clearTimeout(newTimeout);
  }, [editStore.clientText]);

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
        value={editStore.clientText !== null ? editStore.clientText : ""}
        onChange={(event) => {
          editStore.clientText !== null
            ? editStore.setClientTextFromEditor(event.currentTarget.value)
            : () => {};
        }}
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
});
