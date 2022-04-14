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

const ReplyContainer = styled.form`
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
    this.state = { content: null };
  }

  handleChange = async (event) => {
    if (event.target.id === "content") {
      this.setState({ content: event.target.value });
    }
  };

  handleSubmit = async (event) => {
    event.preventDefault();
    const params = window.location.pathname;
    const pattern = /f(\d+)\/t+(\d+)/g;
    const id = pattern.exec(params);
    let formData = new FormData();
    formData.append("content", this.state.content);
    const response = await fetch(`/api/DiscussionForum/ReplyCreate/${id[2]}`, {
      method: "post",
      body: formData,
    });
    const result = await response.json();
    const parsed = JSON.parse(result);
    if (id && parsed.topicId) {
      // this could be done better, now it kinda forces a refresh
      // but we can repopulate the replies properly if we pass the component
      // furtner down the tree
      this.props.navigate(0);
      const scrollingElement = document.scrollingElement || document.body;
      scrollingElement.scrollTop = scrollingElement.scrollHeight;
    }
  };

  render() {
    return (
      <Container>
        <Header>Post a reply</Header>
        <ReplyContainer onSubmit={this.handleSubmit}>
          <ReplyBody
            placeholder="Write your reply..."
            id="content"
            onChange={this.handleChange}
          ></ReplyBody>
          <Button
            id="postReply"
            variant="contained"
            onClick={this.handleSubmit}
            endIcon={<SendIcon />}
          >
            Send
          </Button>
        </ReplyContainer>
      </Container>
    );
  }
}

export default withRouter(CreateReply);
