import React, { Component } from "react";
import axios from "axios";
import Typography from "@mui/material/Typography";
import TextField from '@mui/material/TextField';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import ReceiversDropdown from "./dropdowns/ReceiversDropdown"
import FormControl from '@mui/material/FormControl';



export default class CreateMessage extends Component {

    constructor(props) {
        super(props);
        this.state = {
            receiverId: "",
            receiverUserName: "",
            subject: "",
            subjectLabel: "Subject",
            subjectHelperText: "",
            subjectError: false,
            message: "",
            messageLabel: "Message",
            messageHelperText: "",
            messageError: false,
            validationToggle: false
        };
    }

    async sendMessage(event) {
        event.preventDefault();
        if (this.state.subject === "" || this.state.message === "" || this.state.receiverId === "") {
            if (this.state.subject === "") {
                this.validateSubject(this.state.subject);
            }
            if (this.state.message === "") {
                this.validateMessage(this.state.message);
            }
            if (this.state.receiverId === "") {
                this.setState({
                    validationToggle: this.state.validationToggle ? false : true
                });
            }
        } else {
            this.props.setCreateMessage(false);
            let messageToPost = {
                "subject": this.state.subject,
                "message": this.state.message,
                "receiverid": this.state.receiverId,
                "receiverusername": this.state.receiverUserName,
                "senderid": this.props.senderid,
                "senderusername": this.props.senderUserName
            }
            await axios.post('https://localhost:5001/api/PrivateMessages/CreatePrivateMessage', messageToPost);
            this.props.readReceivedMessages();
            this.props.readSentMessages();
        }
    }

    getReceiverData(receiverId, receiverUserName) {
        this.setState({
            receiverId: receiverId,
            receiverUserName: receiverUserName
        });
    }

    handleInputChange(event) {
        if (event.target.name === "subject") {
            this.setState({
                subject: event.target.value
            });
            this.validateSubject(event.target.value);
        }

        if (event.target.name === "message") {
            this.setState({
                message: event.target.value
            });
            this.validateMessage(event.target.value);
        }
    }

    validateSubject(subject) {
        if (subject === "") {
            this.setState({
                subjectLabel: "Error",
                subjectHelperText: "The message must contain a subject!",
                subjectError: true
            });
        } else {
            this.setState({
                subjectLabel: "Subject",
                subjectHelperText: "",
                subjectError: false
            });
        }
    }

    validateMessage(message) {
        if (message === "") {
            this.setState({
                messageLabel: "Error",
                messageHelperText: "The message must contain a message!",
                messageError: true
            });
        } else {
            this.setState({
                messageLabel: "Subject",
                messageHelperText: "",
                messageError: false
            });
        }
    }

    render() {
        return (
            <Card sx={{ minWidth: 275 }}>
                <CardContent>
                    <Typography variant="h5" component="div" gutterBottom >
                        Create Message
                    </Typography>
                    <form onSubmit={async event => { this.sendMessage(event) }}>
                        <ReceiversDropdown
                            getReceiverData={this.getReceiverData.bind(this)}
                            validationToggle={this.state.validationToggle}
                        />
                        <FormControl fullWidth sx={{ mb: 1.5 }}>
                            <TextField
                                name="subject"
                                label={this.state.subjectLabel}
                                error={this.state.subjectError}
                                helperText={this.state.subjectHelperText}
                                onChange={this.handleInputChange.bind(this)}
                                onBlur={this.handleInputChange.bind(this)}
                            />
                        </FormControl>
                        <FormControl fullWidth>
                            <TextField
                                name="message"
                                label={this.state.messageLabel}
                                error={this.state.messageError}
                                helperText={this.state.messageHelperText}
                                multiline
                                rows={10}
                                onChange={this.handleInputChange.bind(this)}
                                onBlur={this.handleInputChange.bind(this)}
                            />
                        </FormControl>
                        <CardActions>
                            <Button size="small" type="submit" >Send Message</Button>
                        </CardActions>
                        </form>
                </CardContent>
            </Card>
        );
    }
}
