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

const TopicContainer = styled.form`
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
    this.state = { subject: null, content: null };
  }

  handleChange = async (event) => {
    if (event.target.id === "subject") {
      this.setState({ subject: event.target.value });
    }
    if (event.target.id === "content") {
      this.setState({ content: event.target.value });
    }
  };

  handleSubmit = async (event) => {
    event.preventDefault();
    const params = window.location.pathname;
    const pattern = /(?<=\/forum\/f)(\d)/g;
    const id = params.match(pattern).toString();
    let formData = new FormData();
    formData.append("subject", this.state.subject);
    formData.append("content", this.state.content);
    const response = await fetch(`/api/DiscussionForum/TopicCreate/${id}`, {
      method: "post",
      body: formData,
    });
    const result = await response.json();
    const parsed = JSON.parse(result);
    console.log(parsed.forumId);
    console.log(parsed.topicId);
    if (parsed.forumId && parsed.topicId) {
      this.props.navigate(`/forum/f${parsed.forumId}/t${parsed.topicId}`);
    }
  };

  render() {
    return (
      <Container>
        <Header>Post a Topic</Header>
        <TopicContainer onSubmit={this.handleSubmit}>
          <TopicSubject
            placeholder="Write your subject..."
            id="subject"
            onChange={this.handleChange}
          ></TopicSubject>
          <TopicBody
            placeholder="Write your content..."
            id="content"
            onChange={this.handleChange}
          ></TopicBody>
          <Button
            id="postReply"
            variant="contained"
            endIcon={<SendIcon />}
            onClick={this.handleSubmit}
          >
            Send
          </Button>
        </TopicContainer>
      </Container>
    );
  }
}

export default withRouter(CreateTopic);
