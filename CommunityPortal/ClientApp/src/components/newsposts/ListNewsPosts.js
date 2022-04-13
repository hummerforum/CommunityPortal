import React, { Component } from "react";
import ReactDOM from "react-dom";
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
import authService from "../../components/api-authorization/AuthorizeService";
import Link from "@mui/material/Link";
import Button from "@mui/material/Button";
import ShowNewsPost from "./ShowNewsPost";
import NewsPostForm from "./NewsPostForm";
import DeleteNewsPost from "./DeleteNewsPost";

export class ListNewsPosts extends Component {
    static displayName = ListNewsPosts.name;

    constructor(props) {
        super(props);
        this.state = {
            userRole: null,
            isLoaded: false,
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
        this.listNewsPosts();
    }

    async listNewsPosts() {
        let URL = "/api/newspost";
        if (this.props.categoryId != null) {
            URL += "/GetByCategoryId/" + this.props.categoryId;
        }
        const response = await fetch(URL, {
            method: 'GET'
        });
        const newsPostData = await response.json();
        this.setState({ newsPosts: newsPostData, isLoaded: true });
    }

    clickView = (id) => {
        ReactDOM.unmountComponentAtNode(document.getElementById('NewsPostView'));
        ReactDOM.render(<ShowNewsPost id={id} categoryId={this.props.categoryId} />, document.getElementById('NewsPostView'));
    }

    clickAdd = () => {
        ReactDOM.unmountComponentAtNode(document.getElementById('NewsPostView'));
        ReactDOM.render(<NewsPostForm categoryId={this.props.categoryId} />, document.getElementById('NewsPostView'));
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
        let { userRole, isLoaded, newsPosts } = this.state;
        if (!isLoaded) {
            return <div>Loading news posts...</div>
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
                            News posts
                        </Typography>
                        {((userRole === "Admin") || (userRole === "Moderator")) ? (
                            <Button variant="contained" color="primary" onClick={() => this.clickAdd()}>
                                Add news post
                            </Button>
                        )
                            :
                            (
                                null
                            )}

                        // Kalender kommer...
                        <TableContainer component={Paper}>
                            <Table className='table table-bordered'>
                                <TableHead>
                  <TableRow>
                    <TableCell>Header</TableCell>
                    <TableCell>Date</TableCell>
                    {(userRole === "Admin") || (userRole === "Moderator") ? [
                      <TableCell>Edit</TableCell>,
                      <TableCell>Delete</TableCell>
                    ]
                    : {}}
                  </TableRow>
                </TableHead>
                <TableBody>
                  {newsPosts.length > 0 ?
                    newsPosts.map((newsPost) => (
                      <TableRow key={newsPost.NewsPostId}>
                        <TableCell><Link href="#" onClick={e => this.clickView(e.target.value)}>{newsPost.Heading}</Link></TableCell>
                        <TableCell>{newsPost.CreatedDate}
                        {newsPost.UpdatedDate != null ?
                          <span>Updated: {newsPost.UpdatedDate}</span>
                        :{}}
                        </TableCell>
                        {(userRole === "Admin") || (userRole === "Moderator") ? [
                          <TableCell>
                            <Button variant="contained" color="primary" value={newsPost.NewsPostId} onClick={e => this.clickEdit(e.target.value)}>,
                              Edit
                            </Button>
                          </TableCell>,
                          <TableCell>
                            <Button variant="contained" color="primary" value={newsPost.NewsPostId} onClick={e => this.clickDelete(e.target.value)}>
                              Delete
                            </Button>
                          </TableCell>
                        ]: {}}
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
