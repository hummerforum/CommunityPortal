import React from "react";
import { Component } from "react";
import { Navigate } from "react-router-dom";
import {
  ApplicationPaths,
  QueryParameterNames,
} from "./ApiAuthorizationConstants";
import authService from "./AuthorizeService";

export default class AuthorizeRoute extends Component {
  constructor(props) {
    super(props);
    this.state = {
      ready: false,
      authenticated: false,
    };
  }

  componentDidMount() {
    this._subscription = authService.subscribe(() =>
      this.authenticationChanged()
    );
    this.populateAuthenticationState();
  }

  componentWillUnmount() {
    authService.unsubscribe(this._subscription);
  }

  render() {
    const { ready, authenticated } = this.state;
    // should probably convert this whole to a functional one so we can take advantage of more react router hooks..
    const returnUrl = window.location.href;
    const redirectUrl = `${ApplicationPaths.Login}?${
      QueryParameterNames.ReturnUrl
    }=${encodeURIComponent(returnUrl)}`;
    if (!ready) {
      return <div>?</div>;
    } else {
      const { component: Component } = this.props;
      return (
        <>{authenticated ? Component : <Navigate replace to={redirectUrl} />}</>
      );
    }
  }

  async populateAuthenticationState() {
    const authenticated = await authService.isAuthenticated();
    this.setState({ ready: true, authenticated });
  }

  async authenticationChanged() {
    this.setState({ ready: false, authenticated: false });
    await this.populateAuthenticationState();
  }
}
