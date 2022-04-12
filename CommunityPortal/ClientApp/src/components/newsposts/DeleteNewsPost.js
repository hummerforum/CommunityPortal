import React, { Component } from "react";
import ReactDOM from "react-dom";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";
import Typography from "@mui/material/Typography";
import authService from "../../components/api-authorization/AuthorizeService";
import ListNewsPosts from "./ListNewsPosts";

export class DeleteNewsPost extends Component {
  static displayName = DeleteNewsPost.name;

  constructor(props) {
    super(props);
    this.state = {
      isDeleted: false,
      newsPost: [],
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

  componentDidMount() {
    this.deleteNewsPost();
  }

  async deleteNewsPost() {
    const header = await authService.getUser();
    const id = this.props.id;
    const response = await fetch("/api/newspost/" + id, {
      method: "DELETE",
      headers: header,
    });
    const newsPostData = await response.json();
    this.setState({ newsPost: newsPostData, isDeleted: true });
  }

  render() {
    const { isDeleted, newsPost } = this.state;
    if (!isDeleted) {
      return <div>Deleting news post...</div>;
    } else {
      return (
        <Container>
          <Grid
            container
            direction="column"
            justifyContent="space-evenly"
            alignItems="center"
          >
            {newsPost != null ? (
              [
                ReactDOM.unmountComponentAtNode(
                  document.getElementById("NewsPostView")
                ),
                ReactDOM.render(
                  <ListNewsPosts categoryId={this.props.categoryId} />,
                  document.getElementById("NewsPostView")
                ),
              ]
            ) : (
              <Typography variant="h2" component="div" gutterBottom>
                Failed to delete news post!
              </Typography>
            )}
          </Grid>
        </Container>
      );
    }
  }
}

export default DeleteNewsPost;
