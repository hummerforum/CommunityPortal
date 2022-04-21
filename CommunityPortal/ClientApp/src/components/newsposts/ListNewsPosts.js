import React, { Component } from "react";
import ReactDOM from "react-dom";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";
import Typography from "@mui/material/Typography";
import Table from '@mui/material/Table';
import TableBody from '@mui/material/TableBody';
import TableCell from '@mui/material/TableCell';
import TableContainer from '@mui/material/TableContainer';
import TableHead from '@mui/material/TableHead';
import TableRow from '@mui/material/TableRow';
import Paper from '@mui/material/Paper';
import authService from "../../components/api-authorization/AuthorizeService";
import Button from "@mui/material/Button";
import News from "../../pages/News";
import ShowNewsPost from "./ShowNewsPost";
import NewsPostForm from "./NewsPostForm";
import DeleteNewsPost from "./DeleteNewsPost";
import { formatRelative } from "date-fns";

export class ListNewsPosts extends Component {
    static displayName = ListNewsPosts.name;

    constructor(props) {
        super(props);
        this.state = {
            userRole: null,
            isLoaded: false,
            category: null,
            isCategoryLoaded: false,
            newsPosts: []
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

    async getUserRole() {
        this.setState({ userRole: await authService.getRole() });
    }

    componentDidMount() {
        this.getUserRole();
        if (this.props.categoryId != null) {
            this.getCategory(this.props.categoryId);
        } else {
            this.setState({ isCategoryLoaded: true });
        }
        this.listNewsPosts();
    }

    async getCategory(id) {
        const response = await fetch("/api/category/" + id, {
            method: 'GET'
        });
        const categoryData = await response.json();
        this.setState({ category: categoryData, isCategoryLoaded: true });
    }

    async listNewsPosts() {
        let URL = "/api/newspost";
        if (this.props.categoryId != null) {
            URL += "/GetByCategoryId/" + this.props.categoryId;
        }
        const response = await fetch(URL, {
            method: 'GET'
        });
        let newsPostData = await response.json();
        if (this.props.selectedDate != null) {
            var selectedPosts = new Array();
            for (var i = 0; i < newsPostData.length; i++) {
                var selectedDate = this.props.selectedDate.toISOString().substring(0, 10);
                var newsPostDate = newsPostData[i].CreatedDate.substring(0, 10);
                if (newsPostDate === selectedDate) {
                    selectedPosts.push(newsPostData[i]);
                }
            }
            this.setState({ newsPosts: selectedPosts, isLoaded: true });
            
        } else {
            this.setState({ newsPosts: newsPostData, isLoaded: true });
        }
    }

    clickCategories = () => {
        ReactDOM.unmountComponentAtNode(document.getElementById('NewsPostView'));
        ReactDOM.render(<News />, document.getElementById('NewsPostView'));
    }

    clickAdd = () => {
        ReactDOM.unmountComponentAtNode(document.getElementById('NewsPostView'));
        ReactDOM.render(<NewsPostForm categoryId={this.props.categoryId} />, document.getElementById('NewsPostView'));
    }

    clickView = (id) => {
        ReactDOM.unmountComponentAtNode(document.getElementById('NewsPostView'));
        ReactDOM.render(<ShowNewsPost id={id} categoryId={this.props.categoryId} />, document.getElementById('NewsPostView'));
    }

    clickEdit = (id) => {
        ReactDOM.unmountComponentAtNode(document.getElementById('NewsPostView'));
        ReactDOM.render(<NewsPostForm id={id} categoryId={this.props.categoryId} />, document.getElementById('NewsPostView'));
    }

    clickDelete = (id) => {
        ReactDOM.unmountComponentAtNode(document.getElementById('NewsPostView'));
        ReactDOM.render(<DeleteNewsPost id={id} categoryId={this.props.categoryId} />, document.getElementById('NewsPostView'));
    }

    render() {
        let { userRole, isLoaded, newsPosts, isCategoryLoaded, category, selectedDate } = this.state;
        if (!isLoaded) {
            return <div>Loading news post...</div>;
        } else if (!isCategoryLoaded) {
            return <div>Loading category...</div>;
        } else {
            let sortedNewsPosts = newsPosts.sort((a, b) => a.CreatedDate < b.CreatedDate ? 1 : -1);
            newsPosts = sortedNewsPosts;
            return [
                <Container>
                    <Grid
                        container
                        direction="column"
                        justifyContent="space-evenly"
                        alignItems="center"
                    >
                        <Typography variant="h2" component="div" gutterBottom>
                            {category != null ? (category.Title) : ("News posts")}
                        </Typography>

                        <Button size="small" color="primary" onClick={() => this.clickCategories()}>
                            {category != null ? ("Select another category") : ("Select a category")}
                        </Button>

                        {(((userRole === "Admin") || (userRole === "Moderator")) && (category != null)) ? [
                            (" "),
                            <Button sx={{ mt: 2.5 }} variant="contained" color="primary" onClick={() => this.clickAdd()}>
                                Add news post
                            </Button>
                        ] : ("")}

                        <TableContainer sx={{ mt: 1.5 }} component={Paper}>
                            <Table className='table table-bordered'>
                                <TableHead>
                                    <TableRow>
                                        <TableCell>Header</TableCell>
                                        <TableCell>Event?</TableCell>
                                        <TableCell>Date</TableCell>
                                        <TableCell>View</TableCell>
                                        {(userRole === "Admin") || (userRole === "Moderator") ? [
                                            <TableCell>Edit</TableCell>,
                                            <TableCell>Delete</TableCell>
                                        ] : ("")}
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {newsPosts.length > 0 ?
                                        newsPosts.map((newsPost) => (
                                            <TableRow key={newsPost.NewsPostId}>
                                                <TableCell>{newsPost.Heading}</TableCell>
                                                <TableCell>{newsPost.IsEvent ? ("Yes") : ("No")}</TableCell>
                                                <TableCell>{newsPost.UpdatedDate != null ? (formatRelative(Date.parse(newsPost.UpdatedDate), Date.now())) : (formatRelative(Date.parse(newsPost.CreatedDate), Date.now()))}</TableCell>
                                                <TableCell>
                                                    <Button variant="contained" color="primary" value={newsPost.NewsPostId} onClick={e => this.clickView(e.target.value)}>
                                                        View
                                                    </Button>
                                                </TableCell>
                                                {(userRole === "Admin") || (userRole === "Moderator") ? [
                                                    <TableCell>
                                                        <Button variant="contained" color="primary" value={newsPost.NewsPostId} onClick={e => this.clickEdit(e.target.value)}>
                                                            Edit
                                                        </Button>
                                                    </TableCell>,
                                                    <TableCell>
                                                        <Button variant="contained" color="primary" value={newsPost.NewsPostId} onClick={e => this.clickDelete(e.target.value)}>
                                                            Delete
                                                        </Button>
                                                    </TableCell>
                                                ] : ("")}
                                            </TableRow>
                                        ))
                                        :
                                        <TableRow>
                                            <TableCell>There are no news posts</TableCell>
                                        </TableRow>
                                    }
                                </TableBody>

                            </Table>
                        </TableContainer>
                    </Grid>
                </Container>
            ]
        }
    }
}

export default ListNewsPosts;
