import React, { Component } from "react";
import Container from "@mui/material/Container";
import ForumList from "../components/forum/ForumList";
import { Route, Routes } from "react-router";
import TopicList from "../components/forum/TopicList";
import Topic from "../components/forum/Topic";

export class Forum extends Component {
  static displayName = Forum.name;

  render() {
    return (
      <Container>
        <Routes>
          <Route path="/" element={<ForumList />} />
          <Route path="/f:id" element={<TopicList />} />
          <Route path="/f:id/t:id" element={<Topic />} />
        </Routes>
      </Container>
    );
  }
}
