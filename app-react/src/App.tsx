import React from "react";
import { Provider } from "react-redux";
import { Routes, Route } from "react-router-dom";
import SignInSide from "./components/LoginPage/SignInSide";
import { UsersOverview } from "./components/UsersOverview/UsersOverview";
import { AuthProvider } from "./contexts/AuthContext";
import Dashboard from "./Dashboard";
import { RegisterForm } from "./forms/RegisterForm";
import { DashboardLayout } from "./layouts/DashboardLayout";
import { PrivateRoute } from "./routes/Routes";
import { store } from "./stores/store";
import { handleRedirectWhenNotAuthenticated } from "./helpers/helpers";
import axios, { AxiosError } from "axios";
import { LoginApi, AuthApi, UsersApi, /*EnumApi */} from "./api";

/* API CLIENTS */

const apiClient = axios.create({
  baseURL: "http://localhost:5173",
  withCredentials: true,
});

const loginApi = new LoginApi(
  {
    isJsonMime: () => true,
  },
  "http://localhost:5173",
  apiClient
);

export const authApi = new AuthApi(
  {
    isJsonMime: () => true,
  },
  "http://localhost:5173",
  apiClient
);

export const usersApi = new UsersApi(
  {
    isJsonMime: () => true,
  },
  "http://localhost:5173",
  apiClient
);
// const enumApi = new EnumApi(
//   {
//     isJsonMime: () => true,
//   },
//   "http://localhost:5173",
//   apiClient
// );

/* API CLIENTS END */

// INTERCEPTORS
apiClient.interceptors.response.use(
  (response: any) => response,
  (error: AxiosError) => {
    handleRedirectWhenNotAuthenticated(error);
    return error.response;
  }
);

export default class App extends React.Component {

  componentDidMount(): void {
    // enumApi.enumEnumGetAllCountriesGet().then((result) => {
    //   console.log(result)
    // })
  }

  render() {
    return (
      <Provider store={store}>
        <AuthProvider>
          <Routes>
            <Route path="/" element={<SignInSide loginApi={loginApi} />} />
            <Route
              path="/dashboard"
              element={
                <PrivateRoute>
                  <DashboardLayout authApi={authApi}>
                    <Dashboard />
                  </DashboardLayout>
                </PrivateRoute>
              }
            />
            <Route
              path="/users"
              element={
                <PrivateRoute>
                  <DashboardLayout authApi={authApi}>
                    <UsersOverview usersApi={usersApi} />
                  </DashboardLayout>
                </PrivateRoute>
              }
            />
            <Route path="/register" element={<RegisterForm usersApi={usersApi}/>} />
          </Routes>
        </AuthProvider>
      </Provider>
    );
  }
}
