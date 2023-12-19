import { createSlice } from '@reduxjs/toolkit';

export const loginSlice = createSlice({
  name: 'login',
  initialState: {
    userName: '',
    password: '',
  },
  reducers: {
    login: (state, action) => {
      state.userName = action.payload.userName;
      state.password = action.payload.password;
    },
  },
});

// Action creators are generated for each case reducer function
export const { login } = loginSlice.actions;

export default loginSlice.reducer;
