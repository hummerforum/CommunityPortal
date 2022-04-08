import React, { Component } from "react";
import ReactDOM from "react-dom";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";
import FormControl from "@mui/material/FormControl";
import TextField from "@mui/material/TextField";
import Select from "@mui/material/Select";
import MenuItem from "@mui/material/MenuItem";
import Button from "@mui/material/Button";
import AddNewsPost from "./AddNewsPost";
import UpdateNewsPost from "./UpdateNewsPost";
import authService from "../../components/api-authorization/AuthorizeService";

export class NewsPostForm extends Component {
  static displayName = NewsPostForm.name;

  constructor(props) {
    super(props);
    this.state = {
      userRole: null,
      isLoaded: false,
      newsPost: null,
      isCategoriesLoaded: false,
      categories: []
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
    if (this.props.id != null) {
      this.getNewsPost();
    } else {
      this.setState({ isLoaded: true });
    }
    this.getCategories();
  }

  async getNewsPost() {
    const header = await authService.getUser();
    const id = this.props.id;
    const response = await fetch("/api/newspost/" + id, { 
      method: 'GET',
      headers: header
    });
    const newsPostData = await response.json();
    this.setState({ newsPost: newsPostData, isLoaded: true });
  }

  async getCategories() {
    const response = await fetch("/api/category", { 
      method: 'GET'
    });
    const categoryData = await response.json();
    this.setState({ categories: categoryData, isCategoriesLoaded: true });
  }

  changeField = (event) => {
    this.setState({[event.target.name]: event.target.value});
  };

  submitForm = (event) => {
    event.preventDefault();
    ReactDOM.unmountComponentAtNode(document.getElementById('NewsPostView'));
    if (this.newsPost == null) {
      ReactDOM.render(<AddNewsPost data={this.state} categoryId={this.props.categoryId} />, document.getElementById('NewsPostView'));
    } else {
      ReactDOM.render(<UpdateNewsPost data={this.state} categoryId={this.props.categoryId} />, document.getElementById('NewsPostView'));
    }
  };

  render() {
    const {userRole, isLoaded, newsPost, isCategoriesLoaded, categories} = this.state;
    if (!isLoaded) {
      return <div>Loading news post...</div>
    } else if (!isCategoriesLoaded) {
      return <div>Loading categories...</div>
    } else {
      const mapCategories = categories.map((row, index) => {
        return (
          <MenuItem key={index} value={row.CategoryId}>{row.Title}</MenuItem>
        );
      });
      return (
        <Container>
          <Grid
            container
            direction="column"
            justifyContent="space-evenly"
            alignItems="center"
          >
            <FormControl onSubmit={this.submitForm}>
              {newsPost != null ?
                <TextField
                  id="NewsPostId"
                  style={{ width: "400px", margin: "5px" }}
                  type="text"
                  hidden
                  value={newsPost.NewsPostId}
                  onChange={this.changeField}
                />
              : {}}
              <TextField
                id="Heading"
                style={{ width: "400px", margin: "5px" }}
                type="text"
                required
                label="Header"
                value={this.state.value}
                defaultValue={newsPost != null ? newsPost.Heading : {}}
                variant="outlined"
                onChange={this.changeField}
              />
              <br />
              <TextField
                id="Information"
                style={{ width: "400px", margin: "5px" }}
                type="text"
                required
                label="Information"
                value={this.state.value}
                defaultValue={newsPost != null ? newsPost.Information : {}}
                variant="outlined"
                multiline
                rows={10}
                onChange={this.changeField}
              />
              <br />
              <Select
                id="CategoryId"
                value={this.props.categoryId != null ? this.props.categoryId : 0}
                label="Category"
                onChange={this.changeField}
              >
                {mapCategories}
              </Select>
              <hr />
              <TextField
                id="Tag"
                style={{ width: "400px", margin: "5px" }}
                type="text"
                required
                label="Tag"
                value={this.state.value}
                defaultValue={newsPost != null ? newsPost.Tag : {}}
                variant="outlined"
                onChange={this.changeField}
              />
              <br />
              <TextField
                id="Description"
                style={{ width: "400px", margin: "5px" }}
                type="text"
                required
                label="RSS description"
                value={this.state.value}
                defaultValue={newsPost != null ? newsPost.Description : {}}
                variant="outlined"
                multiline
                rows={5}
                onChange={this.changeField}
              />
              <br />
              {(userRole === "Admin") || (userRole === "Moderator") ?
                <Button variant="contained" color="primary">
                  Save
                </Button>
              : {}}
            </FormControl>
          </Grid>
        </Container>
      );
    }
  }
}

export default NewsPostForm;
