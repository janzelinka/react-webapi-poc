import { createSlice } from "@reduxjs/toolkit";

export const loginSlice = createSlice({
  name: "login",
  initialState: {
    userName: "",
    password: "",
  },
  reducers: {
    login: (state, action) => {
      state.userName = action.payload.userName;
      state.password = action.payload.password;
    },
  },
});

export const addNewUserSlice = createSlice({
  name: "addNewUser",
  initialState: {
    Email: "",
    Password: "",
    FirstName: "",
    LastName: "",
  },
  reducers: {
    register: (state, action) => {
      state.Email = action.payload.userName;
      state.Password = action.payload.password;
      state.FirstName = action.payload.firstName;
      state.LastName = action.payload.lastName;
    },
  },
});

export const signUpSlice = createSlice({
  name: "signUp",
  initialState: {
    Email: "",
    Password: "",
    FirstName: "",
    LastName: "",
  },
  reducers: {
    signUp: (state, action) => {
      state.Email = action.payload.UserName;
      state.Password = action.payload.Password;
      state.FirstName = action.payload.FirstName;
      state.LastName = action.payload.LastName;
    },
  },
});

// Action creators are generated for each case reducer function
export const { login } = loginSlice.actions;
export const { register } = addNewUserSlice.actions;
export const { signUp } = signUpSlice.actions;

export const loginReducer = loginSlice.reducer;
export const registerReducer = addNewUserSlice.reducer;
export const signUpReducer = signUpSlice.reducer;
