import React, { Component } from "react";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";

export class News extends Component {
  static displayName = News.name;

  render() {
    return (
      <Container>
        <Grid
          container
          direction="column"
          justifyContent="space-evenly"
          alignItems="center"
        >
          insert news feed here
        </Grid>
      </Container>
    );
  }
}
