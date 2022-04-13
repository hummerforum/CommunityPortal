import React, { Component } from "react";
import GlobalStyles from "@mui/material/GlobalStyles";
import CssBaseline from "@mui/material/CssBaseline";
import NavMenu from "./NavMenu";

export class Layout extends Component {
  static displayName = Layout.name;

  render() {
    return (
      <div>
        <GlobalStyles
          styles={{ ul: { margin: 0, padding: 0, listStyle: "none" } }}
        />
        <CssBaseline />
            <NavMenu />

        {this.props.children}
      </div>
    );
  }
}
