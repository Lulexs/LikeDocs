import { createBrowserRouter, RouteObject } from "react-router-dom";
import MainContainer from "../layout/MainContainer";

export const routes: RouteObject[] = [
  {
    path: "",
    element: <MainContainer />,
  },
  {
    path: "/signin",
    element: <MainContainer />,
  },
  { path: "/signup", element: <MainContainer /> },
];

export const router = createBrowserRouter(routes);
