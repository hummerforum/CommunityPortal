import React, { Component } from "react";
import ReactDOM from "react-dom";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";
import Typography from "@mui/material/Typography";
import authService from "../../components/api-authorization/AuthorizeService";
import ListNewsPosts from "./ListNewsPosts";

export class UpdateNewsPost extends Component {
  static displayName = UpdateNewsPost.name;

  constructor(props) {
    super(props);
    this.state = {
      isUpdated: false,
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
    this.updateNewsPost();
  }

  async updateNewsPost() {
    const header = await authService.getUser();
    const data = this.props.data;
    const response = await fetch("/api/newspost", { 
      method: 'PUT',
      headers: header,
      body: JSON.stringify({
        NewsPostId: parseInt(data.NewsPostId),
        PostType: parseInt(data.PostType),
        Heading: data.Heading,
        Information: data.Information,
        CategoryId: data.CategoryId,
        UserName: data.UserName, // ???
        UpdatedDate: data.Date, // to do: get current date and time
        Tag: data.Tag
      })
    });
    const newsPostData = await response.json();
    this.setState({ newsPost: newsPostData, isUpdated: true });
  }

  render() {
    const {isUpdated, newsPost} = this.state;
    if (!isUpdated) {
      return <div>Updating news post...</div>
    } else {
      return (
        <Container>
          <Grid
            container
            direction="column"
            justifyContent="space-evenly"
            alignItems="center"
          >
            {newsPost != null ?
              [
                ReactDOM.unmountComponentAtNode(document.getElementById('NewsPostView')),
                ReactDOM.render(<ListNewsPosts categoryId={this.props.categoryId} />, document.getElementById('NewsPostView'))
              ]
            :
              <Typography variant="h2" component="div" gutterBottom>
                Failed to update news post!
              </Typography>
            }
          </Grid>
        </Container>
      )
    }
  }
}

export default UpdateNewsPost;
