import { AxiosError } from 'axios';

export function handleRedirectWhenNotAuthenticated(error: AxiosError) {
  
  if (error?.response?.status == 401 && window.location.hash !== '#/') {
    window.location.href = '/#/';
  }
}
