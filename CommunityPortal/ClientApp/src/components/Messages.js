import React, { Component } from "react";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import Typography from "@mui/material/Typography";
import authService from "./api-authorization/AuthorizeService";
import axios from "axios";

export class Messages extends Component {

    constructor() {
        super();
        this.state = {
            messages: []
        };
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
            "https://localhost:5001/api/PrivateMessages/",
            {
                headers: header
            }
        );
        if (response.status === 200 && response.data !== null) {
            let responseData = response.data;
            for (let index = 0; index < responseData.length; index++) {
                responseData[index] = Object.keys(responseData[index]).reduce((accumulator, key) => {
                    accumulator[key.toLowerCase()] = responseData[index][key];
                    return accumulator;
                }, {});
            }
            this.setState({ messages: responseData })
        }
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
                        Messages
                    </Typography>
                    <TableContainer component={Paper}>
                        <Table className='table table-bordered'>
                            <TableHead>
                                <TableRow>
                                    <TableCell>Subject</TableCell>
                                    <TableCell>From</TableCell>
                                    <TableCell>Sent</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {this.state.messages.length > 0 ?
                                    this.state.messages.map((message) => (
                                        <TableRow key={message.privatemessageid}>
                                            <TableCell>{message.subject}</TableCell>
                                            <TableCell>{message.sender}</TableCell>
                                            <TableCell>{message.timesent.replace("T", " ")}</TableCell>
                                        </TableRow>
                                    ))
                                    :
                                    < TableRow >
                                        <TableCell>There are no messages</TableCell>
                                    </TableRow>
                                }
                            </TableBody>
                        </Table>
                    </TableContainer>

                </Grid>
            </Container>
        );
    }
}
