import React, { Component } from "react";
import Link from "@mui/material/Link";
import { LoginMenu } from "./api-authorization/LoginMenu";
import { Link as RouterLink } from "react-router-dom";
import AppBar from "@mui/material/AppBar";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Button from "@mui/material/Button";

export class NavMenu extends Component {
  static displayName = NavMenu.name;

  constructor(props) {
    super(props);

    this.toggleNavbar = this.toggleNavbar.bind(this);
    this.state = {
      collapsed: true,
    };
  }

  toggleNavbar() {
    this.setState({
      collapsed: !this.state.collapsed,
    });
  }

  render() {
    return (
      <header>
        <AppBar
          position="static"
          color="default"
          elevation={0}
          sx={{ borderBottom: (theme) => `1px solid ${theme.palette.divider}` }}
        >
          <Toolbar sx={{ flexWrap: "wrap" }}>
            <Link
              component={RouterLink}
              to="/"
              variant="h5"
              color="#fb551c"
              underline="none"
              noWrap
              sx={{ flexGrow: 1 }}
            >
              Hummer
            </Link>
            <nav>
              <Button component={RouterLink} variant="text" to="/forum">
                Forum
              </Button>
              <Button component={RouterLink} variant="text" to="/news">
                News
              </Button>
              {/*               <Link
                component={RouterLink}
                variant="button"
                to="/counter"
                sx={{ my: 1, mx: 1.5 }}
              >
                Counter
              </Link>
              <Link
                component={RouterLink}
                variant="button"
                to="/fetch-data"
                sx={{ my: 1, mx: 1.5 }}
              >
                Fetch Data
              </Link> */}
              <LoginMenu />
            </nav>
          </Toolbar>
        </AppBar>
      </header>
    );
  }
}
