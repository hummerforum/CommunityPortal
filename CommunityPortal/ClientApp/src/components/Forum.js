import React, { Component } from "react";
import Container from "@mui/material/Container";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import faker from "@faker-js/faker";
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";
import TableContainer from "@mui/material/TableContainer";
import Pagination from "@mui/material/Pagination";
import Stack from "@mui/material/Stack";

export class Forum extends Component {
  static displayName = Forum.name;

  rows = addJunk();

  render() {
    return (
      <Container>
        <TableContainer>
          <Table aria-label="simple table">
            <TableHead>
              <TableRow>
                <TableCell>Topics</TableCell>
                <TableCell align="right">Author</TableCell>
                <TableCell align="right">Category</TableCell>
                <TableCell align="right">Recent Reply</TableCell>
                <TableCell align="right">Replies</TableCell>
              </TableRow>
            </TableHead>
            <TableBody>
              {this.rows.map((row) => (
                <TableRow
                  key={row.topic}
                  sx={{ "&:last-child td, &:last-child th": { border: 0 } }}
                >
                  <TableCell component="th" scope="row">
                    {row.topic}
                  </TableCell>
                  <TableCell align="right">{row.author}</TableCell>
                  <TableCell align="right">{row.category}</TableCell>
                  <TableCell align="right">{row.recent}</TableCell>
                  <TableCell align="right">{row.replies}</TableCell>
                </TableRow>
              ))}
            </TableBody>
          </Table>
        </TableContainer>
        <Stack direction="row" justifyContent="center" alignItems="center">
          <Pagination count={10} size="large" />
        </Stack>
      </Container>
    );
  }
}

function addJunk() {
  var rows = [];
  var x = 15;
  for (var i = 0; i < x; i++) {
    rows.push({
      topic: faker.hacker.phrase(),
      author: faker.name.findName(),
      category: faker.name.jobArea(),
      recent: faker.name.findName(),
      replies: Math.floor(Math.random() * 100),
    });
  }
  return rows;
}
