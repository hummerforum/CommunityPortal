import React, { Component } from "react";
import { Route, Routes } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";
import { Forum } from "./components/Forum";
import { News } from "./components/News";
import { FetchData } from "./components/FetchData";
import { Counter } from "./components/Counter";
import AuthorizeRoute from "./components/api-authorization/AuthorizeRoute";
import ApiAuthorizationRoutes from "./components/api-authorization/ApiAuthorizationRoutes";
import { ApplicationPaths } from "./components/api-authorization/ApiAuthorizationConstants";

export default class App extends Component {
  static displayName = App.name;

  render() {
    return (
      <Layout>
        <Routes>
          <Route exact path="/" element={<Home />} />
          <Route path="/counter" element={<Counter />} />
          <Route path="/forum" element={<Forum />} />
          <Route path="/news" element={<News />} />
          <Route
            path="/fetch-data/*"
            element={
              <AuthorizeRoute path="/fetch-data" component={<FetchData/>} />
            }
          />
          <Route
            path={`${ApplicationPaths.ApiAuthorizationPrefix}/*`}
            element={<ApiAuthorizationRoutes />}
          />
        </Routes>
      </Layout>
    );
  }
}
