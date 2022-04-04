import React, { Component } from "react";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";
import Typography from "@mui/material/Typography";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";

export class NewsPostForm extends Component {
  static displayName = NewsPostForm.name;

  constructor(props) {
    super(props);
    this.state = {
      isLoaded: false,
      newsPost: null
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
    if (this.props.id != null) {
      this.getNewsPost();
    } else {
      this.setState({ isLoaded: true });
    }
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
            <Form>
              <TextField
                id="heading"
                style={{ width: "200px", margin: "5px" }}
                type="text"
                required
                label="Header"
                defaultValue={newsPost != null ? newsPost.Heading : {}}
                variant="outlined"
              />
              <br />
              <TextField
                id="information"
                style={{ width: "400px", margin: "5px" }}
                type="text"
                required
                label="Information"
                defaultValue={newsPost != null ? newsPost.Information : {}}
                variant="outlined"
                multiline
                rows={10}
              />
              <hr />
              <TextField
                id="tag"
                style={{ width: "200px", margin: "5px" }}
                type="text"
                required
                label="Tag"
                defaultValue={newsPost != null ? newsPost.Tag : {}}
                variant="outlined"
              />
              <br />
              <TextField
                id="description"
                style={{ width: "400px", margin: "5px" }}
                type="text"
                required
                label="RSS description"
                defaultValue={newsPost != null ? newsPost.Description : {}}
                variant="outlined"
                multiline
                rows={5}
              />
              <br />
              <Button variant="contained" color="primary">
                Save
              </Button>
            </Form>
          </Grid>
        </Container>
      );
    }
  }
}
