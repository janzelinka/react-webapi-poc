// import { Button } from '@mui/material';
import React, { useEffect /*, { useEffect }*/ } from 'react';
// import ReactDOM from 'react-dom';
import { createRoot } from 'react-dom/client';
import SignInSide from './components/LoginPage/SignInSide';
// import Dashboard from './Dashboard';
import { Provider } from 'react-redux';
import { store } from './stores/store';
import { LoginApi } from './api';
// eslint-disable-next-line @typescript-eslint/no-unused-vars
// import axios from './interceptors/axios';
import { Routes, Route, HashRouter, useNavigate } from 'react-router-dom';
// import { LoginService } from "./services/LoginService";
// import { LoginApi, WeatherForecastApi } from './api';
import axios from 'axios';
import Dashboard from './Dashboard';

const apiClient = axios.create({ baseURL: 'http://localhost:8081' });

const App: React.FC = () => {
  const loginApi = new LoginApi(
    {
      isJsonMime: () => true,
      accessToken: window.localStorage.getItem('token') ?? '',
    },
    'http://localhost:8081',
    apiClient,
  );

  const navigate = useNavigate();

  useEffect(() => {
    apiClient.interceptors.request.use(
      function (config) {
        // Do something before request is sent
        console.log('before request');
        const localStorageToken = window.localStorage.getItem('token');
        // eslint-disable-next-line no-debugger
        if (localStorageToken) {
          config.headers.Authorization = 'bearer ' + localStorageToken;
        }
        return config;
      },
      function (error) {
        // Do something with request error
        return Promise.reject(error);
      },
    );

    // Add a response interceptor
    apiClient.interceptors.response.use(
      (response) => {
        // eslint-disable-next-line no-debugger
        return response;
      },
      (error) => {
        // eslint-disable-next-line no-debugger
        if (error.response.status === 401) {
          navigate('/');
        }
        return error;
      },
    );
  }, [navigate]);

  return (
    <Provider store={store}>
      <Routes>
        <Route path="/" element={<SignInSide loginApi={loginApi} />}></Route>
        <Route
          path="/dashboard"
          element={<Dashboard loginApi={loginApi} />}
        ></Route>
      </Routes>
    </Provider>
  );
};

const root = createRoot(document.getElementById('root')!);

root.render(
  <React.StrictMode>
    <HashRouter basename="/">
      <App />
    </HashRouter>
  </React.StrictMode>,
);
