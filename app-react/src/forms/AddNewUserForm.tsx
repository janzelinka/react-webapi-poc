import * as React from "react";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import Container from "@mui/material/Container";
import { useAppDispatch, useAppSelector } from "../stores/store";
import { register as registerAction } from "../features/login";
import { FormHelperText } from "@mui/material";
import { ValidationMessage } from "../components/FormHelperText/FormHelperText";

export interface AddNewUserFormProps {
  errorList: {
    Email?: string[];
    LastName?: string[];
    Password?: string[];
    FirstName?: string[];
  };
}

export const AddNewUserForm = ({ errorList }: AddNewUserFormProps) => {
  const dispatch = useAppDispatch();
  const register = useAppSelector((state) => state.register);
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
        <Box component="form" noValidate sx={{ mt: 3 }}>
          <Grid container spacing={2}>
            <Grid item xs={12} sm={6}>
              <TextField
                autoComplete="given-name"
                name="FirstName"
                required
                fullWidth
                id="firstName"
                label="First Name"
                autoFocus
                defaultValue={register.FirstName}
                onChange={(e) =>
                  dispatch(
                    registerAction({ ...register, firstName: e.target.value })
                  )
                }
              />
              <ValidationMessage text={errorList.FirstName?.[0]} />
            </Grid>
            <Grid item xs={12} sm={6}>
              <TextField
                required
                fullWidth
                id="lastName"
                label="Last Name"
                name="LastName"
                autoComplete="family-name"
                defaultValue={register.LastName}
                onChange={(e) =>
                  dispatch(
                    registerAction({ ...register, lastName: e.target.value })
                  )
                }
              />
              <ValidationMessage text={errorList.LastName?.[0]} />
            </Grid>
            <Grid item xs={12}>
              <TextField
                required
                fullWidth
                id="email"
                label="Email Address"
                name="Email"
                autoComplete="email"
                defaultValue={register.Email}
                onChange={(e) =>
                  dispatch(
                    registerAction({ ...register, email: e.target.value })
                  )
                }
              />
              <ValidationMessage text={errorList.Email?.[0]} />
            </Grid>
            <Grid item xs={12}>
              <TextField
                required
                fullWidth
                name="Password"
                label="Passwordsss"
                type="password"
                id="password"
                autoComplete="new-password"
                defaultValue={register.Password}
                onChange={(e) =>
                  dispatch(
                    registerAction({ ...register, password: e.target.value })
                  )
                }
              />
              <ValidationMessage text={errorList.Password?.[0]} />
            </Grid>
          </Grid>
        </Box>
      </Box>
    </Container>
  );
};
