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
import authService from "../api-authorization/AuthorizeService";
import Button from "@mui/material/Button";
import NewsPostForm from "./NewsPostForm";
import DeleteNewsPost from "./DeleteNewsPost";

export class ShowNewsPost extends Component {
  static displayName = ShowNewsPost.name;

  constructor(props) {
    super(props);
    this.state = {
      userRole: null,
      isLoaded: false,
      newsPost: []
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
    this.getNewsPost();
  }

  async getNewsPost() {
    const id = this.props.id;
    const response = await fetch("/api/newspost/" + id, { 
      method: 'GET'
    });
    const newsPostData = await response.json();
    this.setState({ newsPost: newsPostData, isLoaded: true });
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
    const {userRole, isLoaded, newsPost} = this.state;
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
          {newsPost.length > 0 ?
            [
              newsPost.map((post) => (
                <TableContainer component={Paper} key={post.NewsPostId}>
                  <Table>
                    <TableHead>
                      <TableRow>
                        <TableCell></TableCell>
                        <TableCell>{post.Heading}</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      <TableRow>
                        <TableCell>{post.UserName}</TableCell>
                        {post.UpdatedDate != null ?
                          <TableCell>{post.UpdatedDate}</TableCell>
                        :
                          <TableCell>{post.CreatedDate}</TableCell>
                        }
                      </TableRow>
                      <TableRow>
                        <TableCell></TableCell>
                        <TableCell>{post.Information}</TableCell>
                      </TableRow>
                    </TableBody>
                  </Table>
                </TableContainer>,
                <div>
                {(userRole === "Admin") || (userRole === "Moderator") ?
                [
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
                ]
              : {}}
              </div>
            ))
            ]
          :
            <Typography variant="h2" component="div" gutterBottom>
              <div>News post does not exist!</div>
            </Typography>
          }
          </Grid>
        </Container>
      )
    }
  }
}

export default ShowNewsPost;
