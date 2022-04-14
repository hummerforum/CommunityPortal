import React, { Component } from "react";
import ReactDOM from "react-dom";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";
import Typography from "@mui/material/Typography";
import authService from "../../components/api-authorization/AuthorizeService";
import ListNewsPosts from "./ListNewsPosts";

export class AddNewsPost extends Component {
  static displayName = AddNewsPost.name;

  constructor(props) {
    super(props);
    this.state = {
      isAdded: false,
      newsPost: null,
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
    this.addNewsPost();
  }

  async addNewsPost() {
    const header = await authService.getUser();
      const data = this.props.data;
      alert(JSON.stringify(this.props.data));
    const response = await fetch("/api/newspost", {
      method: "POST",
      headers: header,
      body: JSON.stringify({
        IsEvent: parseInt(data.IsEvent),
        Heading: data.Heading,
        Information: data.Information,
        CategoryId: data.CategoryId,
        UserName: data.UserName, // ???
        CreatedDate: data.Date, // to do: get current date and time
        UpdatedDate: data.Date, // to do: get current date and time (or null?)
        Tag: data.Tag,
      }),
    });
    const newsPostData = await response.json();
    this.setState({ newsPost: newsPostData, isAdded: true });
  }

  render() {
    const { isAdded, newsPost } = this.state;
    if (!isAdded) {
      return <div>Adding news post...</div>;
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
              <Typography variant="h5" component="div" gutterBottom>
                Failed to add news post!
              </Typography>
            )}
          </Grid>
        </Container>
      );
    }
  }
}

export default AddNewsPost;
