import { createBrowserRouter } from "react-router-dom";
import MainContainer from "../layout/MainContainer";

export const routes = [{ path: "", element: <MainContainer /> }];

export const router = createBrowserRouter(routes);