import styled from "styled-components";
import React, { Component } from "react";
import SendIcon from "@mui/icons-material/Send";
import Button from "@mui/material/Button";
import { withRouter } from "../../withRouter";

const Container = styled.div`
  margin-top: 25px;
  margin-bottom: 25px;
`;

const Header = styled.div`
  font-size: 26px;
`;

const ReplyContainer = styled.div`
  display: flex;
  flex-direction: column;
  background: #eff0f1;
  border: 1px solid #a2a3a4;
  padding: 20px;
`;

const ReplyBody = styled.textarea`
  display: flex;
  height: 150px;
  flex-direction: column;
  margin-bottom: 15px;
`;

class CreateReply extends Component {
  constructor(props) {
    super(props);
    this.state = {
      topic: null,
    };
  }

  render() {
    return (
      <Container>
        <Header>Post a reply</Header>
        <ReplyContainer>
          <ReplyBody placeholder="Write your reply..."></ReplyBody>
          <Button id="postReply" variant="contained" endIcon={<SendIcon />}>
            Send
          </Button>
        </ReplyContainer>
      </Container>
    );
  }
}

export default withRouter(CreateReply);
