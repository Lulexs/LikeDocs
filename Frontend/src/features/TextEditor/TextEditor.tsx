import { Flex, Textarea, TextInput } from "@mantine/core";
import { useState } from "react";

export default function TextEditor() {
  const [value, setValue] = useState("");

  const [username, setUsername] = useState("");

  const generateLineNumbers = (text: string) => {
    const lines = text.split("\n").length;
    return Array.from({ length: lines }, (_, i) => i + 1).join("\n");
  };

  const numbering = generateLineNumbers(value);

  return (
    <Flex m="20px" w="98%">
      <TextInput
        value={username}
        onChange={(event) => setUsername(event.currentTarget.value)}
        label="Username"
        mr="10px"
      ></TextInput>
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
        ml="0"
        flex={1}
      />
    </Flex>
  );
}
