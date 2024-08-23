import React from 'react';
import {
  List,
  ListItemButton,
  ListItemIcon,
  ListItemText,
} from '@mui/material';
import DashboardIcon from '@mui/icons-material/Dashboard';
import PeopleIcon from '@mui/icons-material/People';
import { AuthApi } from '../../api';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../../contexts/AuthContext';

export const MainMenu = ({ authApi }: { authApi: AuthApi }) => {
  const navigate = useNavigate();
  const { setIsAuthenticated } = useAuth();

  const handleLogout = async () => {
    try {
      const result = await authApi.authLogoutGet();
      setIsAuthenticated(false);
      result.data && navigate({ pathname: '/' });
    } catch (error) {
      setIsAuthenticated(false);
    }
  };

  return (
    <List component="nav">
      {' '}
      <React.Fragment>
        <ListItemButton
          onClick={() => {
            navigate('/dashboard');
          }}
        >
          <ListItemIcon>
            <DashboardIcon />
          </ListItemIcon>
          <ListItemText primary="Dashboard" />
        </ListItemButton>

        <ListItemButton
          onClick={() => {
            navigate('/users');
          }}
        >
          <ListItemIcon>
            <PeopleIcon />
          </ListItemIcon>
          <ListItemText primary="Userz" />
        </ListItemButton>

        <ListItemButton onClick={handleLogout}>
          <strong>Logout</strong>
        </ListItemButton>
      </React.Fragment>
    </List>
  );
};
