import * as React from "react";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import FormControlLabel from "@mui/material/FormControlLabel";
import Checkbox from "@mui/material/Checkbox";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { useAppDispatch, useAppSelector } from "../stores/store";
import { signUp as signUpAction } from "../features/login";
import { UsersApi } from "../api";

export const RegisterForm = ({ usersApi }: { usersApi: UsersApi }) => {
  const dispatch = useAppDispatch();
  const signUp = useAppSelector((state) => state.signUp);

  const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();
    // event.
    usersApi.usersCreatePost({
      Email: signUp.Email,
      FirstName: signUp.FirstName,
      LastName: signUp.LastName,
      Password: signUp.Password
    }).then((response) => {
      if (response.status == 200) {
        alert("success")
      }
    });
    console.log(signUp);
  };

  return (
    <Container component="main" maxWidth="xs">
      <CssBaseline />
      <Box
        sx={{
          marginTop: 8,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
          <LockOutlinedIcon />
        </Avatar>
        <Typography component="h1" variant="h5">
          Sign up
        </Typography>
        <Box component="form" noValidate onSubmit={handleSubmit} sx={{ mt: 3 }}>
          <Grid container spacing={2}>
            <Grid  size={{xs:12, sm:6}}>
              <TextField
                autoComplete="given-name"
                name="firstName"
                required
                fullWidth
                id="firstName"
                label="First Name"
                autoFocus
                defaultValue={signUp.FirstName}
                onChange={(e) =>
                  dispatch(
                    signUpAction({ ...signUp, FirstName: e.target.value })
                  )
                }
              />
            </Grid>
            <Grid size={{xs:12, sm:6}}>
              <TextField
                required
                fullWidth
                id="lastName"
                label="Last Name"
                name="lastName"
                autoComplete="family-name"
                defaultValue={signUp.LastName}
                onChange={(e) =>
                  dispatch(
                    signUpAction({ ...signUp, LastName: e.target.value })
                  )
                }
              />
            </Grid>
            <Grid  size={{xs:12, sm:6}}>
              <TextField
                required
                fullWidth
                id="email"
                label="Email Address"
                name="email"
                autoComplete="email"
                defaultValue={signUp.Email}
                onChange={(e) =>
                  dispatch(
                    signUpAction({ ...signUp, UserName: e.target.value })
                  )
                }
              />
            </Grid>
            <Grid  size={{xs:12, sm:6}}>
              <TextField
                required
                fullWidth
                name="password"
                label="Password"
                type="password"
                id="password"
                autoComplete="new-password"
                defaultValue={signUp.Password}
                onChange={(e) =>
                  dispatch(
                    signUpAction({ ...signUp, Password: e.target.value })
                  )
                }
              />
            </Grid>
            <Grid  size={{xs:12, sm:6}}>
              <FormControlLabel
                control={<Checkbox value="allowExtraEmails" color="primary" />}
                label="I want to receive inspiration, marketing promotions and updates via email."
              />
            </Grid>
          </Grid>
          <Button
            type="submit"
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
          >
            Sign Up
          </Button>
          <Grid container justifyContent="flex-end">
            <Grid>
              <Link href="#" variant="body2">
                Already have an account? Sign in
              </Link>
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Container>
  );
};
