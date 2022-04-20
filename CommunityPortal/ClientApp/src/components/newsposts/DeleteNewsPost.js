import React, { Component } from "react";
import ReactDOM from "react-dom";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";
import Typography from "@mui/material/Typography";
import authService from "../../components/api-authorization/AuthorizeService";
import Button from "@mui/material/Button";
import ListNewsPosts from "./ListNewsPosts";

export class DeleteNewsPost extends Component {
    static displayName = DeleteNewsPost.name;

    constructor(props) {
        super(props);
        this.state = {
            userRole: null
        };
    }

    async authHeader() {
        const currentUser = await authService.getUser();
        if (currentUser && currentUser.access_token) {
            return { Authorization: `Bearer ${currentUser.access_token}` };
        } else {
            return {};
        }
    }

    async getUserRole() {
        this.setState({ userRole: await authService.getRole() });
    }

    componentDidMount() {
        this.getUserRole();
    }

    async deleteNewsPost() {
        const header = await authService.getUser();
        const id = this.props.id;
        await fetch("/api/newspost/" + id, {
            method: "DELETE",
            headers: header
        });
        this.showNewsPostList();
    }

    showNewsPostList = () => {
        ReactDOM.unmountComponentAtNode(document.getElementById("NewsPostView"));
        ReactDOM.render(<ListNewsPosts categoryId={this.props.categoryId} />, document.getElementById("NewsPostView"));
    }

    render() {
        const { userRole } = this.state;
        return (
            <Container>
                <Grid
                    container
                    direction="column"
                    justifyContent="space-evenly"
                    alignItems="center"
                >
                    <Typography variant="h5" component="div" gutterBottom>
                        Do you want to delete the news post?
                    </Typography>
                    <div>
                        {((userRole === "Admin") || (userRole === "Moderator"))
                            ? [
                                <Button
                                    variant="contained"
                                    color="primary"
                                    onClick={e => this.deleteNewsPost()}
                                >
                                    Yes
                                </Button>,
                                (" "),
                                <Button
                                    variant="outlined"
                                    color="primary"
                                    onClick={e => this.showNewsPostList()}
                                >
                                    No
                                </Button>
                            ]
                            : ("")}
                    </div>
                </Grid>
            </Container>
        );
    }
}

export default DeleteNewsPost;
