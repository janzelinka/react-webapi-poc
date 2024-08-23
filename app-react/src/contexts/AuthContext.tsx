import React, { createContext, useContext, useState, useMemo } from 'react';

const AuthContext = createContext({
  setIsAuthenticated: (val: boolean) => {},
  isAuthenticated: false,
});

export function AuthProvider({ children }: { children: JSX.Element }) {
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  const value = useMemo(
    () => ({ isAuthenticated, setIsAuthenticated }),
    [isAuthenticated, setIsAuthenticated],
  );

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
}

export function useAuth() {
  return useContext(AuthContext);
}
