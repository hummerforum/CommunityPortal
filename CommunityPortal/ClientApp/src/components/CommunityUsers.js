import React, { Component } from "react";
import Container from "@mui/material/Container";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import TableContainer from "@mui/material/TableContainer";
import Typography from "@mui/material/Typography";
import Grid from "@mui/material/Grid";
import { Button } from "@mui/material";
import authService from "./api-authorization/AuthorizeService";
import axios from "axios";
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import Select, { SelectChangeEvent } from '@mui/material/Select';



export class CommunityUsers extends Component {
    static displayName = CommunityUsers.name;

    constructor() {
        super();
        this.state = {
            messages: []
        };
        this.RemoveUser = this.RemoveUser.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }


    async authHeader() {
        const currentUser = await authService.getUser();
        if (currentUser && currentUser.access_token) {
            return { "Authorization": `Bearer ${currentUser.access_token}` };
        } else {
            return {};
        }
    }

    async readMessages() {
        const header = await authService.getUser();
        const response = await axios.get(
            "https://localhost:5001/api/User/GetAllUsers/",
            {
                headers: header
            }
        );
        if (response.status === 200 && response.data !== null) {
            let responseData = response.data;
            this.setState({ messages: responseData })
        }
    }


    async UpdateMess(id,str) {
        const header = await authService.getUser();
        const response = await axios.post(
            "https://localhost:5001/api/User/UpdateRole/",
            {
                "UserId": id,
                "RoleId": str
            },
            { headers: header}
        );
    }

    async RemoveUser(id) {
        const header = await authService.getUser();
        const response = await axios.post(
            "https://localhost:5001/api/User/DeleteUser/",
            {
                "userid": id,
            },
            { headers: header }
        );
        if (response.data) {
            var i = this.state.messages.findIndex(m => m.UserName == id);
            let x = this.state.messages;
            x.splice(i, 1);
            this.setState({ messages: x });
        }
        else
            alert("Cant delete logged in user");
            
    }


    handleChange(str, event, id)
    {
        this.UpdateMess(str, event.target.value);
        var i = this.state.messages.findIndex(m => m.UserId == id);
        let x = this.state.messages;
        x[i].RoleName = event.target.value;
        this.setState({ messages: x });
    }


    componentDidMount() {
        this.readMessages();
    }


    render() {

        return (
            <Container>
                <Grid
                    container
                    direction="column"
                    justifyContent="space-evenly"
                    alignItems="center"
                >
                <Typography variant="h2" component="div" gutterBottom>
                    Users
                </Typography>


                <TableContainer>
                    <Table aria-label="simple table">
                        <TableHead>
                            <TableRow >
                                <TableCell>User</TableCell>
                                <TableCell align="right">Id</TableCell>
                                <TableCell align="right">Email</TableCell>
                                <TableCell align="right">Role</TableCell>
                                <TableCell align="right"> </TableCell>
                            </TableRow>
                        </TableHead>
                            <TableBody>
                                {this.state.messages.map((message) => (
                                    <TableRow key={message.UserName}>
                                        <TableCell component="th" scope="row"> { message.UserName}</TableCell>
                                        <TableCell align="right">{message.UserId}</TableCell>
                                        <TableCell align="right">{message.Email}</TableCell>
                                        <TableCell align="right">
                                            <InputLabel id="demo-simple-select-label">User role</InputLabel>
                                            <Select
                                                labelId="demo-simple-select-label"
                                                id="demo-simple-select"
                                                value={message.RoleName}
                                                label="Roll"
                                                onChange={(event) => this.handleChange(message.UserId, event, message.UserId)}
                                            >
                                                <MenuItem value={"Admin"}>Administrator</MenuItem>
                                                {message.UserName !== "admin@b.com" ?
                                                    <MenuItem value={"User"}>User</MenuItem> : ""}
                                                {message.UserName !== "admin@b.com" ?
                                                    <MenuItem value={"Moderator"}>Moderator</MenuItem> : ""}
                                                    
                                            </Select>
                                        </TableCell>

                                        <TableCell align="right">
                                            {
                                                message.UserName !== "admin@b.com" ?
                                            <Button onClick={(event) => this.RemoveUser(message.UserId)} align="right">Delete</Button>
                                                    : " "
                                            }
                                        </TableCell>
                                    </TableRow>
                                ))}
                        </TableBody>
                    </Table>
                    </TableContainer>
                </Grid>
            </Container>
        );
    }
}
