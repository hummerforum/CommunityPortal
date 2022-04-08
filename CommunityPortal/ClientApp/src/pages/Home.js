import React, { Component } from "react";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";
import lobster from "../img/lobster.jpg";
import Typography from "@mui/material/Typography";

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <Container>
        <Grid
          container
          direction="column"
          justifyContent="space-evenly"
          alignItems="center"
        >
          <Typography variant="h6" component="div" gutterBottom>
            Hummer - the Community Portal
          </Typography>
          <img src={lobster} alt="a lobster"></img>
        </Grid>
      </Container>
    );
  }
}
