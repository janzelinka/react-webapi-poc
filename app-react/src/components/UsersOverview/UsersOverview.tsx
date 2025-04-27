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
import { UsersViewModel, UsersApi } from "../../api";
import { CustomModal } from "../Modal/Modal";
import {
  AddNewUserForm,
  AddNewUserFormProps,
} from "../../forms/AddNewUserForm";
import { useAppSelector } from "../../stores/store";

export const UsersOverview = ({ usersApi }: { usersApi: UsersApi }) => {
  const [users, setUsers] = useState<UsersViewModel[]>([]);
  const [isModalVisible, setIsModalVisible] = useState(false);
  const [errorListForm, setErrorListForm] = useState<AddNewUserFormProps>({
    errorList: {},
  });
  const register = useAppSelector((state) => state.register);

  useEffect(() => {
    usersApi
      .usersGetAllGet()
      .then((result) => {
        setUsers(result.data);
      })
      .catch();
  }, [usersApi]);

  const addNewUser = async () => {
    const result = await usersApi.usersCreatePost({
      Email: register.Email,
      FirstName: register.FirstName,
      LastName: register.LastName,
      Password: register.Password,
    });

    if (result.data?.Errors) {
      setErrorListForm((prevState) => ({
        errorList: { ...prevState.errorList, ...result.data?.Errors },
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
            {(users ?? []).map((user) => (
              <TableRow
                key={user.Id}
                sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
              >
                <TableCell component="th" scope="row">
                  {user.Email}
                </TableCell>
                <TableCell align="right">{user.FirstName}</TableCell>
                <TableCell align="right">{user.LastName}</TableCell>
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
