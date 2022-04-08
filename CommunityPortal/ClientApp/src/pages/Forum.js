import React, { Component } from "react";
import Container from "@mui/material/Container";
import ForumList from "../components/forum/ForumList";
import { Route, Routes } from "react-router";
import ThreadList from "../components/forum/ThreadList";

export class Forum extends Component {
  static displayName = Forum.name;

  rows = fakeData();

  render() {
    return (
      <Container>
        <Routes>
          <Route path="/" element={<ForumList />} />
          <Route path="/f:id" element={<ThreadList />} />
          <Route path="/t:id" element={<ThreadList />} />
        </Routes>
      </Container>
    );
  }
}

function fakeData() {
  var categories = [
    { id: 1, name: "Fishing", parentId: null },
    { id: 2, name: "Lobster", parentId: 1 },
    { id: 3, name: "Cod", parentId: 1 },
    { id: 4, name: "Anchovy", parentId: 1 },
    { id: 5, name: "Cars", parentId: null },
    { id: 6, name: "Volvo", parentId: 5 },
    { id: 7, name: "SAAB", parentId: 5 },
    { id: 8, name: "BMW", parentId: 5 },
  ];
  return categories;
}
