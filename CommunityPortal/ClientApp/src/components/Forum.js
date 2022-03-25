import React, { Component } from "react";
import Grid from "@mui/material/Grid";
import { loremIpsum } from "lorem-ipsum";
import Table from "@mui/material/Table";
import TableBody from "@mui/material/TableBody";
import TableCell from "@mui/material/TableCell";
import faker from '@faker-js/faker';
import TableHead from "@mui/material/TableHead";
import TableRow from "@mui/material/TableRow";

export class Forum extends Component {
  static displayName = Forum.name;

  rows = addJunk();

  render() {
    return (
      <>
        <Table sx={{ minWidth: 650 }} aria-label="simple table">
          <TableHead>
            <TableRow>
              <TableCell>Topics</TableCell>
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
                  {row.topic} {row.author}
                </TableCell>
                <TableCell align="right">{row.category}</TableCell>
                <TableCell align="right">{row.recent}</TableCell>
                <TableCell align="right">{row.replies}</TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </>
    );
  }
}

function addJunk() {
  var rows = [];
  var x = 20;
  for (var i = 0; i < x; i++) {
    rows.push({
      topic: loremIpsum(),
      author: faker.name.findName(),
      category: "Random",
      recent: faker.name.findName(),
      replies: Math.floor(Math.random() * 100),
    });
  }
  return rows;
}
