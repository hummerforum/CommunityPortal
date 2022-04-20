import React, { Component } from "react";
import ReactDOM from "react-dom";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";
import Typography from "@mui/material/Typography";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import TableContainer from "@mui/material/TableContainer";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import Paper from "@mui/material/Paper";
import authService from "../../components/api-authorization/AuthorizeService";
import Button from "@mui/material/Button";
import ListNewsPosts from "./ListNewsPosts";
import NewsPostForm from "./NewsPostForm";
import DeleteNewsPost from "./DeleteNewsPost";
import { formatRelative } from "date-fns";

export class ShowNewsPost extends Component {
    static displayName = ShowNewsPost.name;

    constructor(props) {
        super(props);
        this.state = {
            userRole: null,
            isLoaded: false,
            newsPost: null
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
        if (this.props.id != null) {
            this.getNewsPost();
        } else {
            this.setState({ isLoaded: true });
        }
    }

    async getNewsPost() {
        const id = this.props.id;
        const response = await fetch("/api/newspost/" + id, {
            method: "GET"
        });
        const newsPostData = await response.json();
        this.setState({ newsPost: newsPostData, isLoaded: true });
    }

    clickBackToList = () => {
        ReactDOM.unmountComponentAtNode(document.getElementById("NewsPostView"));
        ReactDOM.render(
            <ListNewsPosts categoryId={this.props.categoryId} />,
            document.getElementById("NewsPostView")
        );
    }

    clickEdit = (id) => {
        ReactDOM.unmountComponentAtNode(document.getElementById("NewsPostView"));
        ReactDOM.render(
            <NewsPostForm id={id} categoryId={this.props.categoryId} />,
            document.getElementById("NewsPostView")
        );
    };

    clickDelete = (id) => {
        ReactDOM.unmountComponentAtNode(document.getElementById("NewsPostView"));
        ReactDOM.render(
            <DeleteNewsPost id={id} categoryId={this.props.categoryId} />,
            document.getElementById("NewsPostView")
        );
    };

    render() {
        const { userRole, isLoaded, newsPost } = this.state;
        if (!isLoaded) {
            return <div>Loading news post...</div>
        } else {
            return (
                <Container>
                    <Grid
                        container
                        direction="column"
                        justifyContent="space-evenly"
                        alignItems="center"
                    >
                        {newsPost != null ? [
                            <Typography variant="h2" component="div" gutterBottom>
                                {newsPost.Heading}
                            </Typography>,

                            <Button size="small" color="primary" onClick={() => this.clickBackToList()}>
                                Back to news list
                            </Button>,

                            <TableContainer sx={{ mt: 2.5, mb: 1.5 }} component={Paper} key={newsPost.NewsPostId}>
                                <Table>
                                    <TableHead>
                                        <TableRow>
                                            <TableCell style={{ width: "25%" }}>{newsPost.UpdatedDate != null ? (formatRelative(Date.parse(newsPost.UpdatedDate), Date.now())) : (formatRelative(Date.parse(newsPost.CreatedDate), Date.now()))}</TableCell>
                                            <TableCell>{newsPost.IsEvent ? ("[EVENT]: ") : ("")} {newsPost.Heading}</TableCell>
                                        </TableRow>
                                    </TableHead>
                                    <TableBody>
                                        <TableRow>
                                            <TableCell style={{ width: "25%" }}>{newsPost.UserName}</TableCell>
                                            <TableCell>{newsPost.Information}</TableCell>
                                        </TableRow>
                                    </TableBody>
                                </Table>
                            </TableContainer>,

                            <div>
                                {((userRole === "Admin") || (userRole === "Moderator"))
                                    ? [
                                        <Button
                                            variant="contained"
                                            color="primary"
                                            value={newsPost.NewsPostId}
                                            onClick={e => this.clickEdit(e.target.value)}
                                        >
                                            Edit
                                        </Button>,
                                        (" "),
                                        <Button
                                            variant="contained"
                                            color="primary"
                                            value={newsPost.NewsPostId}
                                            onClick={e => this.clickDelete(e.target.value)}
                                        >
                                            Delete
                                        </Button>
                                    ]
                                    : ("")}
                            </div>
                        ] : (
                            <Typography variant="h5" component="div" gutterBottom>
                                <div>News post does not exist!</div>
                            </Typography>
                        )}
                    </Grid>
                </Container>
            );
        }
    }
}

export default ShowNewsPost;
