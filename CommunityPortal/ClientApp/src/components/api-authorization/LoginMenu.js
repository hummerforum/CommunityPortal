import React, { Component, Fragment } from "react";
import { Link as RouterLink } from "react-router-dom";
import Button from "@mui/material/Button";
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
        <Chip avatar={<Avatar/>} label={userName} />
        <Button 
          component={RouterLink}
          variant="text"
          to={logoutPath}
          sx={{ my: 1, mx: 1.5 }}
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
          variant="text"
          to={registerPath}
          sx={{ my: 1, mx: 1.5 }}
        >
          Register
        </Button>
        <Button 
          component={RouterLink}
          variant="text"
          to={loginPath}
        >
          Login
        </Button>
      </Fragment>
    );
  }
}
