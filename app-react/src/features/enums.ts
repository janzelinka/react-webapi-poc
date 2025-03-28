import { createSlice } from "@reduxjs/toolkit";
import { Country } from '../api/api'

export const enumSlice = createSlice({
  name: "enum",
  initialState: {
    Countries: [] as Country[]
  },
  reducers: {
    setCountries: (state, action) => {
        state.Countries = [...action.payload];
    },
  },
});

// Action creators are generated for each case reducer function
export const { setCountries } = enumSlice.actions;


export const enumReducer = enumSlice.reducer;
