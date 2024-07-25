export interface User {
  username: string;
  token: string;
  email: string;
}

export interface UserLoginValues {
  email: string;
  password: string;
}

export interface UserRegisterValues {
  email: string;
  username: string;
  password: string;
}
