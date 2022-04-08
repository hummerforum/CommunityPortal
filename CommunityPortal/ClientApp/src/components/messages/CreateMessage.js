import React, { Component } from "react";
import axios from "axios";
import Typography from "@mui/material/Typography";
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import InputLabel from '@mui/material/InputLabel';

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
            receiverId: null,
            receiverUserName: null,
            subject: null,
            message: null
        };
    }

    async sendMessage(event) {
        event.preventDefault();
        if (false/*!person.name || cityId <= 0*/) {
            /*if (!person.name && cityId <= 0) {
                SetValidateInformation(validateInformation = "You must provide a name and a city!")
            } else if (cityId <= 0) {
                SetValidateInformation(validateInformation = "You must provide a city!")
            } else if (!person.name) {
                SetValidateInformation(validateInformation = "You must provide a name!")
            }*/
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
        }

        if (event.target.name === "message") {
            this.setState({
                message: event.target.value
            });
        }
    }

    render() {
        return (
            <Card sx={{ minWidth: 275 }}>
                <CardContent>
                    <Typography variant="h5" component="div">
                        Create Message
                    </Typography>
                    <form onSubmit={async event => { this.sendMessage(event) }}>
                        <ReceiversDropdown getReceiverData={this.getReceiverData.bind(this)} />
                        <FormControl fullWidth>
                            <TextField
                                name="subject"
                                label="Subject"
                                onChange={this.handleInputChange.bind(this)}
                            />
                        </FormControl>
                        <FormControl fullWidth>
                            <TextField
                                name="message"
                                label="Message"
                                multiline
                                rows={10}
                                onChange={this.handleInputChange.bind(this)}
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
