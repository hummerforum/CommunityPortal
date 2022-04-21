import React, { Component } from "react"
import Grid from "@mui/material/Grid";
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Typography from "@mui/material/Typography";
import authService from "../../components/api-authorization/AuthorizeService";
import axios from "axios";
import ViewMessage from "./ViewMessage";
import CreateMessage from "./CreateMessage";
import Box from '@mui/material/Box';
import Button from '@mui/material/Button';
import Paper from '@mui/material/Paper';
import "./messages.css";
import { withRouter } from "../../withRouter";

class Messages extends Component {
    constructor(props) {
        super(props);
        this.state = {
            receivedMessages: [],
            sentMessages: [],
            viewMessage: null,
            isViewMessageVisible: false,
            isCreateMessageVisible: false,
            isCreateMessageButtonVisible: true,
            userId: null,
            senderUserName: null,
            state: null
        };
    }

    async setViewMessage(viewMessage) {
        if (this.state.viewMessage !== null) {
            if ((this.state.viewMessage.id !== viewMessage.id) ||
                ((this.state.viewMessage.id === viewMessage.id) && (this.state.viewMessage.type !== viewMessage.type))) {
                this.setState({
                    viewMessage: viewMessage
                })
                this.setState({
                    isViewMessageVisible: true
                })

                await this.setMessageAsRead(viewMessage);

                this.setCreateMessage(false);
            } else {
                if (this.state.isViewMessageVisible) {
                    this.setState({
                        isViewMessageVisible: false
                    })
                } else {
                    this.setState({
                        isViewMessageVisible: true
                    })

                    await this.setMessageAsRead(viewMessage);

                    this.setCreateMessage(false);
                }
            }
        } else {
            this.setState({
                viewMessage: viewMessage
            })
            this.state.isViewMessageVisible = true;

            await this.setMessageAsRead(viewMessage);

            this.setCreateMessage(false);
        }
    }

    async setMessageAsRead(message) {
        if (message.type == "received") {
            if (!message.isread) {
                const header = await authService.getUser();
                const response = await axios.get(
                    `https://localhost:5001/api/PrivateMessages/SetReceivedPrivateMessageAsRead/${message.id}`,
                    {
                        headers: header
                    }
                );
            }
        }

        await this.readReceivedMessages();
    }

    setCreateMessage(isCreateMessageVisible) {
        this.setState({ isCreateMessageVisible: isCreateMessageVisible });
        this.setState({ isCreateMessageButtonVisible: !isCreateMessageVisible });

        if (isCreateMessageVisible) {
            this.setState({
                isViewMessageVisible: false
            })
        }
    }

    async authHeader() {
        const currentUser = await authService.getUser();
        if (currentUser && currentUser.access_token) {
            return { "Authorization": `Bearer ${currentUser.access_token}` };
        } else {
            return {};
        }
    }

    async readReceivedMessages() {
        const header = await authService.getUser();
        this.setState({ userId: header.sub })
        this.setState({ senderUserName: header.name })
        const response = await axios.get(
            `https://localhost:5001/api/PrivateMessages/GetReceivedPrivateMessages/${this.state.userId}`,
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
            responseData.forEach((message) => {
                message.type = "received";
            });
            this.setState({ receivedMessages: responseData });
        }
    }

    async readSentMessages() {
        const header = await authService.getUser();
        this.setState({ userId: header.sub })
        const response = await axios.get(
            `https://localhost:5001/api/PrivateMessages/GetSentPrivateMessages/${this.state.userId}`,
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
            responseData.forEach((message) => {
                message.type = "sent";
            });
            this.setState({ sentMessages: responseData });
        }
    }

    async deleteReceivedMessage(event, receivedPrivateMessage) {
        event.stopPropagation();
        const header = await authService.getUser();
        await axios.get(
            `https://localhost:5001/api/PrivateMessages/DeleteReceivedPrivateMessage/${receivedPrivateMessage.id}`,
            {
                headers: header
            }
        );
        this.readReceivedMessages();
        if (this.state.viewMessage) {
            if ((this.state.viewMessage.id === receivedPrivateMessage.id) && (this.state.viewMessage.type === receivedPrivateMessage.type)) {
                this.setState({
                    isViewMessageVisible: false
                });
            }
        }
    }

    async deleteSentMessage(event, sentPrivateMessage) {
        event.stopPropagation();
        const header = await authService.getUser();
        await axios.get(
            `https://localhost:5001/api/PrivateMessages/DeleteSentPrivateMessage/${sentPrivateMessage.id}`,
            {
                headers: header
            }
        );
        this.readSentMessages();
        if (this.state.viewMessage) {
            if ((this.state.viewMessage.id === sentPrivateMessage.id) && (this.state.viewMessage.type === sentPrivateMessage.type)) {
                this.setViewMessage(sentPrivateMessage);
            }
        }
    }

    setStateState(state) {
        this.setState({
            state: state
        });
    }

    componentDidMount() {
        this.readReceivedMessages();
        this.readSentMessages();

        if (this.props.state) {
            this.setStateState(this.props.state);
            this.setCreateMessage(true);
        }
    }

    render() {
        return (
            <Grid
                container
            >
                <Grid item xs={12} md={6}>
                    <Box sx={{ width: "100%", padding: "20px" }}>
                        <Typography variant="h2" component="div" gutterBottom>
                            Messages
                        </Typography>
                        {this.state.isCreateMessageButtonVisible ? (
                            <Button variant="contained" onClick={() => this.setCreateMessage(true)}>Create Message</Button>
                        ) : (
                            null
                        )
                        }
                        {this.state.isViewMessageVisible ? (
                            <ViewMessage
                                viewMessage={this.state.viewMessage}
                                setViewMessage={this.setViewMessage.bind(this)}
                                deleteReceivedMessage={this.deleteReceivedMessage.bind(this)}
                                deleteSentMessage={this.deleteSentMessage.bind(this)}
                                setCreateMessage={this.setCreateMessage.bind(this)}
                                setStateState={this.setStateState.bind(this)}
                            />
                        ) : (
                            null
                        )}
                        {this.state.isCreateMessageVisible ? (
                            <CreateMessage
                                senderid={this.state.userId}
                                senderUserName={this.state.senderUserName}
                                setCreateMessage={this.setCreateMessage.bind(this)}
                                readReceivedMessages={this.readReceivedMessages.bind(this)}
                                readSentMessages={this.readSentMessages.bind(this)}
                                state={this.state.state}
                            />
                        ) : (
                            null
                        )}
                    </Box>
                </Grid>
                <Grid
                    item xs={12} md={6}
                >
                    <Box sx={{ width: "100%", padding: "20px" }}>
                        <Typography variant="h5" component="div" gutterBottom>
                            Received Messages
                        </Typography>
                        <TableContainer component={Paper}>
                            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                                <TableHead>
                                    <TableRow key={1}>
                                        <TableCell>Subject</TableCell>
                                        <TableCell>From</TableCell>
                                        <TableCell>Sent</TableCell>
                                        <TableCell />
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {this.state.receivedMessages.length > 0 ?
                                        this.state.receivedMessages.map((message) => (
                                            <TableRow key={message.privatemessageid} onClick={() => this.setViewMessage(message)}>
                                                <TableCell>
                                                    {message.isread ? (
                                                        message.subject
                                                    ) : (
                                                        <strong>{message.subject}</strong>
                                                    )}
                                                </TableCell>
                                                <TableCell>
                                                    {message.isread ? (
                                                        message.senderusername
                                                    ) : (
                                                        <strong>{message.senderusername}</strong>
                                                    )}
                                                </TableCell>
                                                <TableCell>
                                                    {message.isread ? (
                                                        message.timesent
                                                    ) : (
                                                        <strong>{message.timesent}</strong>
                                                    )}
                                                </TableCell>
                                                <TableCell>
                                                    <Button
                                                        variant="outlined"
                                                        onClick={(event) => this.deleteReceivedMessage(event, message)}
                                                    >
                                                        Delete Message
                                                    </Button>
                                                </TableCell>
                                            </TableRow>
                                        ))
                                        :
                                        < TableRow key={1} >
                                            <TableCell >There are no messages</TableCell>
                                        </TableRow>
                                    }
                                </TableBody>
                            </Table>
                        </TableContainer>
                        <Typography variant="h5" component="div" gutterBottom>
                            Sent Messages
                        </Typography>
                        <TableContainer component={Paper}>
                            <Table sx={{ minWidth: 650 }} aria-label="simple table">
                                <TableHead>
                                    <TableRow key={1}>
                                        <TableCell>Subject</TableCell>
                                        <TableCell>To</TableCell>
                                        <TableCell>Sent</TableCell>
                                        <TableCell />
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {this.state.sentMessages.length > 0 ?
                                        this.state.sentMessages.map((message) => (
                                            <TableRow key={message.privatemessageid} onClick={() => this.setViewMessage(message)}>
                                                <TableCell>{message.subject}</TableCell>
                                                <TableCell>{message.receiverusername}</TableCell>
                                                <TableCell>{message.timesent}</TableCell>
                                                <TableCell>
                                                    <Button
                                                        variant="outlined"
                                                        onClick={(event) => this.deleteSentMessage(event, message)}
                                                    >
                                                        Delete Message
                                                    </Button>
                                                </TableCell>
                                            </TableRow>
                                        ))
                                        :
                                        < TableRow key={1} >
                                            <TableCell>There are no messages</TableCell>
                                        </TableRow>
                                    }
                                </TableBody>
                            </Table>
                        </TableContainer>
                    </Box>
                </Grid>
            </Grid>
        );
    }
}

export default withRouter(Messages);