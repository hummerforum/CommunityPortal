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

export class ListNewsPosts extends Component {
  static displayName = ListNewsPosts.name;

  constructor(props) {
    super(props);
    this.state = {
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

  componentDidMount() {
    this.listNewsPosts();
  }

  async listNewsPosts()
  {
    const header = await authService.getUser();
    const response = await fetch("/api/newspost", { 
      method: 'GET',
      headers: header
    });
    const newsPostData = await response.json();
    this.setState({ newsPosts: newsPostData, isLoaded: true });
  }

  render() {
    const {isLoaded, newsPosts} = this.state;
    if (!isLoaded) {
      return <div>Loading news posts...</div>
    } else {
      return (
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
            <TableContainer component={Paper}>
              <Table className='table table-bordered'>
                <TableHead>
                  <TableRow>
                    <TableCell>Header</TableCell>
                    <TableCell>Date</TableCell>
                  </TableRow>
                </TableHead>
                <TableBody>
                  {newsPosts.length > 0 ?
                    newsPosts.map((newsPost) => (
                      <TableRow key={newsPost.NewsPostId}>
                        <TableCell>{newsPost.Heading}</TableCell>
                        <TableCell>{newsPost.CreatedDate}
                        {newsPost.UpdatedDate != null ?
                          <span>Updated: {newsPost.UpdatedDate}</span>
                        :{}}
                        </TableCell>
                      </TableRow>
                    ))
                  :
                    < TableRow>
                      <TableCell>There are no news posts</TableCell>
                    </TableRow>
                  }
                </TableBody>
              </Table>
            </TableContainer>
          </Grid>
        </Container>
      )
    }
  }
}
