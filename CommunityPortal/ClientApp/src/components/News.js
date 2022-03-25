import React, { Component } from "react";
import Grid from "@mui/material/Grid";
import { LoremIpsum } from "lorem-ipsum";

export class News extends Component {
  static displayName = News.name;

  render() {
    return (
      <Grid
        container
        direction="column"
        justifyContent="space-evenly"
        alignItems="center"
      >
          insert news feed here
      </Grid>
    );
  }
}
