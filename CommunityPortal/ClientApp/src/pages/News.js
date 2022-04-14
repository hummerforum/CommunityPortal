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
import ListNewsPosts from "../components/newsposts/ListNewsPosts";
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

  getRSSLink = (id) => {
    return "/api/newspost/GetRSSByCategoryId/" + id;
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
            (
               <TableContainer sx={{ mt: 1.5 }} component={Paper}>
                  <Table className='table table-bordered'>
                    <TableHead>
                      <TableRow>
                        <TableCell>Category</TableCell>
                        <TableCell>RSS</TableCell>
                        <TableCell>View</TableCell>
                      </TableRow>
                    </TableHead>
                    <TableBody>
                      {categories.map((category) => (
                        <TableRow key={category.CategoryId}>
                          <TableCell>{category.Title}</TableCell>
                          <TableCell><a href={this.getRSSLink(category.CategoryId)}>RSS</a></TableCell>
                          <TableCell>
                            <Button variant="contained" color="primary" value={category.CategoryId} onClick={e => this.clickCategory(e.target.value)}>
                              View
                            </Button>
                          </TableCell>
                        </TableRow>
                      ))}
                    </TableBody>
                  </Table>
                </TableContainer>
              )
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
