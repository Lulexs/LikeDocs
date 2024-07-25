import {
  Paper,
  TextInput,
  PasswordInput,
  Button,
  Title,
  Text,
  Anchor,
} from "@mantine/core";
import classes from "./RegisterPage.module.css";
import { useNavigate } from "react-router-dom";
import { useForm } from "@mantine/form";
import { useStore } from "../../../stores/store";

export function RegisterPage() {
  const navigate = useNavigate();
  const { userStore } = useStore();

  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      email: "",
      username: "",
      password: "",
    },

    validate: {
      email: (value) => (/^\S+@\S+$/.test(value) ? null : "Invalid email"),
      username: (value) =>
        value.length >= 4 ? null : "Username must be at least 4 chars longs",
      password: (value) =>
        value.length >= 8 ? null : "Password must be at least 8 chars long",
    },
  });

  return (
    <>
      <div className={classes.wrapper}>
        <Paper className={classes.form} radius={0} p={30}>
          <>
            <Title
              order={2}
              className={classes.title}
              ta="center"
              mt="md"
              mb={50}
            >
              Sign up
            </Title>

            <form
              onSubmit={form.onSubmit((value) => userStore.register(value))}
            >
              <TextInput
                label="Email"
                placeholder="example@gmail.com"
                size="md"
                key={form.key("email")}
                {...form.getInputProps("email")}
              />
              <TextInput
                label="Username"
                placeholder="StrongBoi"
                size="md"
                mt="md"
                key={form.key("username")}
                {...form.getInputProps("username")}
              />
              <PasswordInput
                label="Password"
                placeholder="Your password"
                mt="md"
                size="md"
                key={form.key("password")}
                {...form.getInputProps("password")}
              />
              <Button type="submit" fullWidth mt="xl" size="md">
                Login
              </Button>
            </form>

            <Text ta="center" mt="md">
              Have an account yet?
              <Anchor<"a">
                href="#"
                fw={700}
                onClick={(event) => {
                  event.preventDefault();
                  navigate("/signin");
                }}
              >
                Sign in
              </Anchor>
            </Text>
          </>
        </Paper>
      </div>
    </>
  );
}
