import React, { Component } from "react";
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
import authService from "./api-authorization/AuthorizeService";

export class ShowNewsPost extends Component {
  static displayName = ShowNewsPost.name;

  constructor(props) {
    super(props);
    this.state = {
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

  componentDidMount() {
    this.getNewsPost();
  }

  async getNewsPost()
  {
    const header = await authService.getUser();
    const id = this.props.id;
    const response = await fetch("/api/newspost/" + id, { 
      method: 'GET',
      headers: header,
    });
    const newsPostData = await response.json();
    this.setState({ newsPost: newsPostData, isLoaded: true });
  }

  render() {
    const {isLoaded, newsPost} = this.state;
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
              </TableContainer>
            ))
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
