import React, { Component, Fragment } from "react";
import { Link as RouterLink } from "react-router-dom";
import Button from "@mui/material/Button";
import authService from "../../components/api-authorization/AuthorizeService";

export class MessagesLink extends Component {
  constructor(props) {
    super(props);

    this.state = {
      isAuthenticated: false,
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
    const [isAuthenticated] = await Promise.all([
      authService.isAuthenticated(),
    ]);
    this.setState({
      isAuthenticated,
    });
  }

  render() {
    if (this.state.isAuthenticated) {
      return this.authenticatedView();
    } else {
      return null;
    }
  }

  authenticatedView() {
    return (
      <Fragment>
        <Button
          sx={{ my: 2, color: "white", display: "block" }}
          component={RouterLink}
          to="/messages"
        >
          Messages
        </Button>
      </Fragment>
    );
  }
}
