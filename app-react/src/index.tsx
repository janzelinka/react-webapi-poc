// import { Button } from '@mui/material';
import React /*, { useEffect }*/ from 'react';
// import ReactDOM from 'react-dom';
import { createRoot } from 'react-dom/client';
import SignInSide from './components/LoginPage/SignInSide';
// import Dashboard from './Dashboard';
import { Provider } from 'react-redux';
import { store } from './stores/store';
// import { LoginService } from "./services/LoginService";
// import { LoginApi, WeatherForecastApi } from './api';
// import axios, { Axios, AxiosResponse } from 'axios';

const App: React.FC = () => {
  // const loginApi = new LoginApi(
  //   { isJsonMime: () => true },
  //   "http://localhost:3000"
  // );
  // const weatherForecastApi = new WeatherForecastApi(
  //   { isJsonMime: () => true },
  //   "http://localhost:3000"
  // );

  // useEffect(() => {
  //   // loginApi.loginLoginPost("abc", "def").then((response) => response.data)
  //   weatherForecastApi.getWeatherForecast().then((test) => test.data.)
  // }, []);

  // axios.interceptors.request.use(
  //   function (config) {
  //     // Do something before request is sent
  //     console.log("before request");
  //     return config;
  //   },
  //   function (error) {
  //     // Do something with request error
  //     return Promise.reject(error);
  //   }
  // );

  // // Add a response interceptor
  // axios.interceptors.response.use(
  //   function (response) {
  //     // Any status code that lie within the range of 2xx cause this function to trigger
  //     // Do something with response data
  //     console.log("after request");
  //     return response;
  //   },
  //   function (error) {
  //     // Any status codes that falls outside the range of 2xx cause this function to trigger
  //     // Do something with response error
  //     return Promise.reject(error);
  //   }
  // );
  return (
    <Provider store={store}>
      <SignInSide />
    </Provider>
  );
};

const root = createRoot(document.getElementById('root')!);

root.render(
  <React.StrictMode>
    <App />
  </React.StrictMode>,
);
