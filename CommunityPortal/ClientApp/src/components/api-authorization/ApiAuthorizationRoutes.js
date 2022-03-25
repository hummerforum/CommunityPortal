import React, { Component, Fragment } from "react";
import { Route, Routes, useParams } from "react-router";
import { Login } from "./Login";
import { Logout } from "./Logout";
import {
  ApplicationPaths,
  LoginActions,
  LogoutActions,
} from "./ApiAuthorizationConstants";

export default class ApiAuthorizationRoutes extends Component {
  render() {
    console.log(LoginActions.Login)
    return (
      <Routes>

      </Routes>
    );
  }
}
