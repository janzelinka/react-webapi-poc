import * as React from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Paper from '@mui/material/Paper';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';
import { createTheme, ThemeProvider } from '@mui/material/styles';
import { LoginApi } from '../../api';
import { AxiosError, AxiosResponse } from 'axios';
import { useAuth } from '../../contexts/AuthContext';
import { Link, useNavigate } from 'react-router-dom';
import { FormHelperText } from '@mui/material';

// TODO remove, this demo shouldn't need to reset the theme.
const defaultTheme = createTheme();

interface SignInSideProps {
  loginApi: LoginApi;
}

export default function SignInSide({ loginApi }: SignInSideProps) {
  const navigate = useNavigate();
  const { setIsAuthenticated } = useAuth();

  const [validationMessage,setValidationMessage] = React.useState("");

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    const data = new FormData(event.currentTarget);
    loginApi
      .login(data.get('email')?.toString(), data.get('password')?.toString())
      .then(handleLoginCallback())
      .catch(handleLoginError());
  };

  return (
    <ThemeProvider theme={defaultTheme}>
      <Grid container component="main" sx={{ height: '100vh' }}>
        <CssBaseline />
        <Grid
        sx={{
          backgroundImage:
            'url(https://source.unsplash.com/random?wallpapers)',
          backgroundRepeat: 'no-repeat',
          backgroundColor: (t) =>
            t.palette.mode === 'light'
              ? t.palette.grey[50]
              : t.palette.grey[900],
          backgroundSize: 'cover',
          backgroundPosition: 'center',
        }}
        size={{xs:false, sm: 4, md:7}}
          
        />
        <Grid size={{xs: 12, sm: 8, md: 5}} component={Paper} elevation={6} square>
          <Box
            sx={{
              my: 8,
              mx: 4,
              display: 'flex',
              flexDirection: 'column',
              alignItems: 'center',
            }}
          >
            <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }}>
              <LockOutlinedIcon />
            </Avatar>
            <Typography component="h1" variant="h5">
              Sign in
            </Typography>
            <Box
              component="form"
              noValidate
              onSubmit={handleSubmit}
              sx={{ mt: 1 }}
            >
              <TextField
                margin="normal"
                required
                fullWidth
                id="email"
                label="Email Address"
                name="email"
                autoComplete="email"
                autoFocus
                value={'zelo'}
              />
              <TextField
                margin="normal"
                required
                fullWidth
                name="password"
                label="Password"
                type="password"
                id="password"
                autoComplete="current-password"
                value={'12000'}
              />
              <FormHelperText style={{color: 'red'}}>{validationMessage}</FormHelperText>
              <FormControlLabel
                control={<Checkbox value="remember" color="primary" />}
                label="Remember me"
              />
              <Button
                type="submit"
                fullWidth
                variant="contained"
                sx={{ mt: 3, mb: 2 }}
              >
                Sign In
              </Button>
              <Grid container>
                <Grid>
                  {/* <Link href="#" variant="body2">
                    Forgot password?
                  </Link> */}
                </Grid>
                <Grid>
                  <Link to="/register">{"Don't have an account? Sign Up"}</Link>
                </Grid>
              </Grid>
            </Box>
          </Box>
        </Grid>
      </Grid>
    </ThemeProvider>
  );

  function handleLoginError():
    | ((reason: any) => void | PromiseLike<void>)
    | null
    | undefined {
    return (e: AxiosError) => {
      if (e.response?.status == 401) {
        setIsAuthenticated(false);
      }
    };
  }

  function handleLoginCallback():
    | ((value: AxiosResponse<void, any>) => void | PromiseLike<void>)
    | null
    | undefined {
    return (response: AxiosResponse) => {
      
      if (response?.status == 200) {
        setIsAuthenticated(true);
        navigate('/dashboard');
      }

      if (response?.status == 401) {
        setValidationMessage("Problem with auth");
      }
    };
  }
}
