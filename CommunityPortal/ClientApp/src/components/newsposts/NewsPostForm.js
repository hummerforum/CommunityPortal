import React, { Component } from "react";
import ReactDOM from "react-dom";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";
import FormControl from "@mui/material/FormControl";
import FormControlLabel from "@mui/material/FormControlLabel";
import Checkbox from "@mui/material/Checkbox";
import TextField from "@mui/material/TextField";
import InputLabel from "@mui/material/InputLabel";
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
      return { Authorization: `Bearer ${currentUser.access_token}` };
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
    const id = this.props.id;
    const response = await fetch("/api/newspost/" + id, {
      method: "GET"
    });
    const newsPostData = await response.json();
    this.setState({ newsPost: newsPostData, isLoaded: true });
  }

  async getCategories() {
    const response = await fetch("/api/category", {
      method: "GET"
    });
    const categoryData = await response.json();
    this.setState({ categories: categoryData, isCategoriesLoaded: true });
  }

  changeField = (event) => {
    this.setState({ [event.target.name]: event.target.value });
  };

  submitForm = (event) => {
    event.preventDefault();
    ReactDOM.unmountComponentAtNode(document.getElementById("NewsPostView"));
    if (this.newsPost == null) {
      ReactDOM.render(
        <AddNewsPost data={this.state} categoryId={this.props.categoryId} />,
        document.getElementById("NewsPostView")
      );
    } else {
      ReactDOM.render(
        <UpdateNewsPost data={this.state} categoryId={this.props.categoryId} />,
        document.getElementById("NewsPostView")
      );
    }
  };

  render() {
    const { userRole, isLoaded, newsPost, isCategoriesLoaded, categories } =
      this.state;
    if (!isLoaded) {
      return <div>Loading news post...</div>;
    } else if (!isCategoriesLoaded) {
      return <div>Loading categories...</div>;
    } else {
      const mapCategories = categories.map((row, index) => {
        return (
          <MenuItem key={index} value={row.CategoryId}>
            {row.Title}
          </MenuItem>
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
                    <form onSubmit={async event => { this.submitForm(event) }}>
                        {newsPost != null ? [
                            <FormControl sx= {{ mt: 1.5 }}>
                            <TextField
                              id="NewsPostId"
                              style={{ width: "400px", margin: "5px" }}
                              type="text"
                              value={newsPost.NewsPostId}
                              onChange={this.changeField}
                             />
                            </FormControl>,
                          <br />
                        ]:
                        ("")}
                        <FormControlLabel
                            sx={{ mt: 1.5 }}
                            id="IsEvent"
                            control={<Checkbox />}
                            label="Event" />
                        <br />
                        <FormControl sx={{ mt: 1.5 }}>
                            <TextField
                                id="Heading"
                                style={{ width: "400px", margin: "5px" }}
                                type="text"
                                required
                                label="Header"
                                value={this.state.value}
                                defaultValue={newsPost != null ? newsPost.Heading : ("")}
                                variant="outlined"
                                onChange={this.changeField}
                            />
                        </FormControl>
                        <br />
                        <FormControl sx={{ mt: 1.5 }}>
                            <TextField
                                id="Information"
                                style={{ width: "400px", margin: "5px" }}
                                type="text"
                                required
                                label="Information"
                                value={this.state.value}
                                defaultValue={newsPost != null ? newsPost.Information : ("")}
                                variant="outlined"
                                multiline
                                rows={10}
                                onChange={this.changeField}
                            />
                        </FormControl>
                        <br />
                        <FormControl sx={{ mt: 1.5 }}>
                            <InputLabel id="categoryLabel">Category</InputLabel>
                            <Select
                                id="CategoryId"
                                labelId="categoryLabel"
                                value={this.state.value}
                                defaultValue={
                                    this.props.categoryId != null ? this.props.categoryId : ("")
                                }
                                label="Category"
                                onChange={this.changeField}
                            >
                                {mapCategories}
                            </Select>
                        </FormControl>
                        <br />
                        <FormControl sx={{ mt: 1.5 }}>
                            <TextField
                                id="Tag"
                                style={{ width: "400px", margin: "5px" }}
                                type="text"
                                label="Tag"
                                value={this.state.value}
                                defaultValue={newsPost != null ? newsPost.Tag : ("")}
                                variant="outlined"
                                onChange={this.changeField}
                            />
                        </FormControl>
                        <br />
                        <FormControl sx={{ mt: 1.5 }}>
                            <TextField
                                id="Description"
                                style={{ width: "400px", margin: "5px" }}
                                type="text"
                                label="RSS description"
                                value={this.state.value}
                                defaultValue={newsPost != null ? newsPost.Description : ("")}
                                variant="outlined"
                                multiline
                                rows={5}
                                onChange={this.changeField}
                            />
                        </FormControl>
                        <br />
                        {userRole === "Admin" || userRole === "Moderator" ? (
                            <Button sx={{ mt: 1.5 }} variant="contained" color="primary" type="submit">
                                Save
                            </Button>
                        ) : ("")}
                    </form>
                </Grid>
            </Container>
      );
    }
  }
}

export default NewsPostForm;
