import { createContext, useContext, useState, useMemo, useEffect } from 'react';
import { authApi } from '../App';


const AuthContext = createContext({
  setIsAuthenticated: (val: boolean) => { console.log(val); },
  isAuthenticated: false,
});

export function AuthProvider({ children }: { children: JSX.Element }) {
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  const value = useMemo(
    () => ({ isAuthenticated, setIsAuthenticated }),
    [isAuthenticated, setIsAuthenticated],
  );

  useEffect(() => {
    authApi
    .authIsValidGet()
    .then((res) => setIsAuthenticated(res.data))
  }, [])
  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  return useContext(AuthContext);
}
