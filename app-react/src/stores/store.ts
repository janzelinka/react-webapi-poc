import { configureStore } from "@reduxjs/toolkit";
import login from "../features/login";

export const store = configureStore({
  reducer: {
    login: login,
  },
});

export type RootState = ReturnType<typeof store.getState>;
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch;
