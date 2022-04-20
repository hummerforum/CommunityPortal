import React, { Component } from "react";
import Typography from "@mui/material/Typography";
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import "./messages.css";
import { Grid } from "@mui/material";
import { withRouter } from "../../withRouter";


class ViewMessage extends Component {

    constructor(props) {
        super(props);
    }

    async deleteMessage(event, message) {
        if (message.type === "received") {
            this.props.deleteReceivedMessage(event, message);
        } else {
            this.props.deleteSentMessage(event, message);
        }
    }

    answerMessage(event, viewMessage) {
        const state = {
                receiverid: viewMessage.senderid,
                subject: `Answer to subject: "${viewMessage.subject}"`,
                message: `Reply to message: "${viewMessage.message}"\n\n`
        }

        this.props.setStateState(state);
        this.props.setCreateMessage(true);
    }

    render() {
        return (
            <Card sx={{ minWidth: 275 }}>
                <CardContent>
                    {this.props.viewMessage.type === "received" ? (
                        <Typography variant="h5" component="div">
                            Received Message
                        </Typography>
                    ) : (
                        <Typography variant="h5" component="div">
                            Sent Message
                        </Typography>
                    )}
                    <Typography variant="body2" component="div">
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
                    <Grid container >
                        <Grid item xs={6}>
                            {this.props.viewMessage.type === "received" ? (
                                <Button
                                    size="small"
                                    onClick={(event) => this.answerMessage(event, this.props.viewMessage)}
                                >
                                    Answer Message
                                </Button>
                            ) : (
                                null
                            )}
                            <Button
                                size="small"
                                onClick={(event) => this.deleteMessage(event, this.props.viewMessage)}
                            >
                                Delete Message
                            </Button>
                        </Grid>
                        <Grid item xs={6} sx={{ display: 'flex', justifyContent: 'flex-end' }} >
                            <Button
                                size="small"
                                onClick={() => this.props.setViewMessage(this.props.viewMessage)}
                            >
                                Close Message
                            </Button>
                        </Grid>
                    </Grid>
                </CardActions>
            </Card >
        );
    }
}

export default withRouter(ViewMessage);