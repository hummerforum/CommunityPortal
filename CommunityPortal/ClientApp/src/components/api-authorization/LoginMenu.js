import React, { Component, Fragment } from "react";
import { Link as RouterLink } from "react-router-dom";
import Button from "@mui/material/Button";
import LogoutIcon from "@mui/icons-material/Logout";
import Avatar from "@mui/material/Avatar";
import Chip from "@mui/material/Chip";
import authService from "./AuthorizeService";
import { ApplicationPaths } from "./ApiAuthorizationConstants";

export class LoginMenu extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isAuthenticated: false,
      userName: null,
    };
  }

  componentDidMount() {
    this._subscription = authService.subscribe(() => this.populateState());
    this.populateState();
  }

  componentWillUnmount() {
    authService.unsubscribe(this._subscription);
  }

  async populateState() {
    const [isAuthenticated, user] = await Promise.all([
      authService.isAuthenticated(),
      authService.getUser(),
    ]);
    this.setState({
      isAuthenticated,
      userName: user && user.name,
    });
  }

  render() {
    const { isAuthenticated, userName } = this.state;
    if (!isAuthenticated) {
      const registerPath = `${ApplicationPaths.Register}`;
      const loginPath = `${ApplicationPaths.Login}`;
      return this.anonymousView(registerPath, loginPath);
    } else {
      const profilePath = `${ApplicationPaths.Profile}`;
      const logoutPath = {
        pathname: `${ApplicationPaths.LogOut}`,
        state: { local: true },
      };
      return this.authenticatedView(userName, profilePath, logoutPath);
    }
  }

  authenticatedView(userName, profilePath, logoutPath) {
    return (
      <Fragment>
        <Chip
          sx={{ color: "#FFF" }}
          avatar={
            <Avatar alt={userName} sx={{ bgcolor: "#95B2B8", color: "#FFF" }}>
              {userName.charAt(0)}
            </Avatar>
          }
          label={userName}
        />

        <Button
          component={RouterLink}
          variant="outline"
          to={logoutPath}
          sx={{ my: 1, mx: 1.5 }}
          endIcon={<LogoutIcon />}
        >
          Logout
        </Button>
      </Fragment>
    );
  }

  anonymousView(registerPath, loginPath) {
    return (
      <Fragment>
        <Button
          component={RouterLink}
          variant="outline"
          to={registerPath}
          sx={{ my: 1, mx: 1.5 }}
        >
          Register
        </Button>
        <Button component={RouterLink} variant="outline" to={loginPath}>
          Login
        </Button>
      </Fragment>
    );
  }
}
