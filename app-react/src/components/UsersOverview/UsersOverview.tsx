import {
  Button,
  Paper,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
} from "@mui/material";
import { useEffect, useState } from "react";
import { GetAllUsersViewModel, LoginApi } from "../../api";
import { CustomModal } from "../Modal/Modal";
import {
  AddNewUserForm,
  AddNewUserFormProps,
} from "../../forms/AddNewUserForm";
import { useAppSelector } from "../../stores/store";

export const UsersOverview = ({ loginApi }: { loginApi: LoginApi }) => {
  const [users, setUsers] = useState<GetAllUsersViewModel[]>([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [errorListForm, setErrorListForm] = useState<AddNewUserFormProps>({
    errorList: {},
  });
  const register = useAppSelector((state) => state.register);

  useEffect(() => {
    loginApi
      .loginLoginGetAllUsersGet()
      .then((result) => {
        setUsers(result.data);
      })
      .catch();
  }, [loginApi]);

  const addNewUser = async () => {
    const result = await loginApi.loginLoginCreateUserPost({
      email: register.Email,
      firstName: register.FirstName,
      lastName: register.LastName,
      password: register.Password,
    });

    if (result.data?.errorList) {
      setErrorListForm((prevState) => ({
        errorList: { ...prevState.errorList, ...result.data?.errorList },
      }));

      return false;
    }
    return true;
  };

  return (
    <>
      <Button variant="contained" onClick={() => setIsModalVisible(true)}>
        Add new Userssszzz
      </Button>
      <TableContainer component={Paper}>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>Email</TableCell>
              <TableCell align="right">First name</TableCell>
              <TableCell align="right">Last name</TableCell>
            </TableRow>
          </TableHead>
          <TableBody>
            {users.map((user) => (
              <TableRow
                key={user.id}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {user.email}
                </TableCell>
                <TableCell align="right">{user.firstName}</TableCell>
                <TableCell align="right">{user.lastName}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </TableContainer>
      <CustomModal
        form={<AddNewUserForm errorList={errorListForm.errorList} />}
        onCancel={() => {
          setIsModalVisible(false);
        }}
        onOk={addNewUser}
        open={isModalVisible}
        title={"Add User"}
      />
    </>
  );
};
