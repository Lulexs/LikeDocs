import {
  Paper,
  TextInput,
  PasswordInput,
  Button,
  Title,
  Text,
  Anchor,
} from "@mantine/core";
import classes from "./LoginPage.module.css";
import { useNavigate } from "react-router-dom";
import { useForm } from "@mantine/form";
import { useStore } from "../../../stores/store";

export function LoginPage() {
  const navigate = useNavigate();
  const { userStore } = useStore();

  const form = useForm({
    mode: "uncontrolled",
    initialValues: {
      email: "",
      password: "",
    },

    validate: {
      email: (value) => (/^\S+@\S+$/.test(value) ? null : "Invalid email"),
      password: (value) => (value.length >= 8 ? null : "Password too short"),
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
              WelcomeBack
            </Title>

            <form onSubmit={form.onSubmit((values) => userStore.login(values))}>
              <TextInput
                label="Email"
                placeholder="example@gmail.com"
                size="md"
                key={form.key("email")}
                {...form.getInputProps("email")}
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
              Don't have an account yet?{" "}
              <Anchor<"a">
                href="#"
                fw={700}
                onClick={(event) => {
                  event.preventDefault();
                  navigate("/signup");
                }}
              >
                Signup
              </Anchor>
            </Text>
          </>
        </Paper>
      </div>
    </>
  );
}
