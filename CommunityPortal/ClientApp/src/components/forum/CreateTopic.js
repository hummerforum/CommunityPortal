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

const TopicContainer = styled.div`
  display: flex;
  flex-direction: column;
  background: #eff0f1;
  border: 1px solid #a2a3a4;
  padding: 20px;
`;

const TopicSubject = styled.textarea`
  display: flex;
  height: 25px;
  flex-direction: column;
  margin-bottom: 15px;
`;

const TopicBody = styled.textarea`
  display: flex;
  height: 150px;
  flex-direction: column;
  margin-bottom: 15px;
`;

class CreateTopic extends Component {
  constructor(props) {
    super(props);
    this.state = {
      topic: null,
    };
  }

  render() {
      console.log(this.state)
      console.log(this.props)
    return (
      <Container>
        <Header>Post a Topic</Header>
        <TopicContainer>
          <TopicSubject placeholder="Write your subject..."></TopicSubject>
          <TopicBody placeholder="Write your content..."></TopicBody>
          <Button id="postReply" variant="contained" endIcon={<SendIcon />}>
            Send
          </Button>
        </TopicContainer>
      </Container>
    );
  }
}

export default withRouter(CreateTopic);
