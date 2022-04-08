import React, { Component } from "react";
import Typography from "@mui/material/Typography";
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';


export default class ViewMessage extends Component {

    constructor(prop) {
        super(prop);
    }

    async deleteMessage(event, message) {
        if (message.type === "received") {
            this.props.deleteReceivedMessage(event, message);
        } else {
            this.props.deleteSentMessage(event, message);
        }
    }

    render() {
        return (
            <Card sx={{ minWidth: 275 }}>
                <CardContent>
                    <Typography variant="h5" component="div">
                        Subject: {this.props.viewMessage.subject}
                    </Typography>
                    {this.props.viewMessage.type === "received" ? (
                        <Typography variant="caption" component="div" color="text.secondary">
                            From: {this.props.viewMessage.senderusername}
                        </Typography>
                    ) : (
                        <Typography variant="caption" component="div" color="text.secondary">
                            To: {this.props.viewMessage.receiverusername}
                        </Typography>
                    )}
                    <Typography variant="caption" component="div" sx={{ mb: 1.5 }} color="text.secondary">
                        Sent: {this.props.viewMessage.timesent}
                    </Typography>
                    <Typography variant="body2">
                        {this.props.viewMessage.message}
                    </Typography>
                </CardContent>
                <CardActions>
                    <Button size="small">Answer Message</Button>
                    <Button
                        size="small"
                        onClick={(event) => this.deleteMessage(event, this.props.viewMessage)}
                    >
                        Delete Message
                    </Button>
                </CardActions>
            </Card>
        );
    }
}
