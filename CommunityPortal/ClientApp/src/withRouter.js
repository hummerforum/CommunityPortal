import React, { Component } from "react";
import { useLocation, useNavigate } from 'react-router-dom';

export const withRouter = (Component) => {
    const Wrapper = (props) => {
        const navigate = useNavigate();
        const { state } = useLocation();

        return (
            <Component
                navigate={navigate}
                state={state}
                {...props}
            />
        );
    };

    return Wrapper;
};