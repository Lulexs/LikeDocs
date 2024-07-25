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

export function LoginPage() {
  const navigate = useNavigate();

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

            <form>
              <TextInput
                label="Email"
                placeholder="example@gmail.com"
                size="md"
              />
              <PasswordInput
                label="Password"
                placeholder="Your password"
                mt="md"
                size="md"
              />
              <Button
                type="submit"
                fullWidth
                mt="xl"
                size="md"
                onClick={() => {}}
              >
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
