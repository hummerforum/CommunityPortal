import React, { Component } from "react";
import { Route, Routes } from "react-router";
import { Layout } from "./components/Layout";
import { Home } from "./components/Home";
import { Forum } from "./components/Forum";
import { News } from "./components/News";
import { Messages } from "./components/Messages";
import { FetchData } from "./components/FetchData";
import AuthorizeRoute from "./components/api-authorization/AuthorizeRoute";
import {
    ApplicationPaths,
    LoginActions,
    LogoutActions,
} from "./components/api-authorization/ApiAuthorizationConstants";
import { Login } from "./components/api-authorization/Login";
import { Logout } from "./components/api-authorization/Logout";

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <Layout>
                <Routes>
                    <Route exact path="/" element={<Home />} />
                    <Route path="/forum" element={<Forum />} />
                    <Route path="/news" element={<News />} />
                    <Route path="/messages" element={<Messages />} />
                    {/* This is how you do a privileged route path is the url component is the react component that loads */}
                    <Route
                        path="/fetch-data"
                        element={
                            <AuthorizeRoute component={<FetchData />} />
                        }
                    />
                    <Route
                        path={ApplicationPaths.Login}
                        element={<Login action={LoginActions.Login} />}
                    />
                    <Route
                        path={ApplicationPaths.LoginFailed}
                        element={<Logout action={LoginActions.LoginFailed} />}
                    />
                    <Route
                        path={ApplicationPaths.LoginCallback}
                        element={<Login action={LoginActions.LoginCallback} />}
                    />
                    <Route
                        path={ApplicationPaths.Profile}
                        element={<Login action={LoginActions.Profile} />}
                    />
                    <Route
                        path={ApplicationPaths.Register}
                        element={<Login action={LoginActions.Register} />}
                    />
                    <Route
                        path={ApplicationPaths.LogOut}
                        element={<Logout action={LogoutActions.Logout} />}
                    />
                    <Route
                        path={ApplicationPaths.LogOutCallback}
                        element={<Logout action={LogoutActions.LogoutCallback} />}
                    />
                    <Route
                        path={ApplicationPaths.LoggedOut}
                        element={<Logout action={LogoutActions.LoggedOut} />}
                    />
                </Routes>
            </Layout>
        );
    }
}
