import React, { Component } from "react";
import ReactDOM from "react-dom";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";
import ListNewsPosts from "./NewsPost/ListNewsPosts";
import Button from "@mui/material/Button";

export class News extends Component {
  static displayName = News.name;

  constructor(props) {
    super(props);
    this.state = {
      isLoaded: false,
      categories: []
    };
  }

  componentDidMount() {
    this.getCategories();
  }

  async getCategories() {
    const response = await fetch("/api/category", { 
      method: 'GET'
    });
    const categoryData = await response.json();
    this.setState({ categories: categoryData, isLoaded: true });
  }

  clickCategory = (id) => {
    ReactDOM.unmountComponentAtNode(document.getElementById('NewsPostView'));
    ReactDOM.render(<ListNewsPosts categoryId={id} />, document.getElementById('NewsPostView'));
  }

  render() {
    const {isLoaded, categories} = this.state;
    if (!isLoaded) {
      return <div>Loading categories...</div>
    } else {
      return (
        <Container>
          <Grid
            container
            direction="column"
            justifyContent="space-evenly"
            alignItems="center"
          >
            <div id="NewsPostView">
              {categories.length > 0 ?
                categories.map((category) => (
                  <Button variant="contained" color="primary" value={category.CategoryId} onClick={e => this.clickCategory(e.target.value)}>
                    {category.Title}
                  </Button>
                ))
              : 
                <ListNewsPosts categoryId={null} />
              }
            </div>
          </Grid>
        </Container>
      );
    }
  }
}
