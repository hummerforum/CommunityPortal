import styled from "styled-components";
import React, { Component } from "react";
import AddIcon from "@mui/icons-material/Add";
import SendIcon from "@mui/icons-material/Send";
import IconButton from "@mui/material/IconButton";
import Avatar from "@mui/material/Avatar";

const Category = styled.div`
  &:first-child {
    margin-top: 35px;
  }
  margin-top: 15px;
`;

const CategoryTitle = styled.div`
  padding-left: 15px;
`;

const CategoryHeaderContainer = styled.div`
  background-color: #70163c;
  color: #fff;
  font-size: 23px;
  border: 1px solid #000;
  display: grid;
  grid-template-columns: 90% 10%;
  align-items: center;
  height: 50px;
`;

const ReplyContainer = styled.div`
  display: flex;
  flex-direction: row;
  background: #eff0f1;
  border: 1px solid #a2a3a4;
  margin-bottom: 15px;
`;

const AuthorContainer = styled.div`
  display: flex;
  flex-direction: column;
  align-items: center;
  width: 200px;
`;

const ReplyContent = styled.div`
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
`;

const ReplyBody = styled.div`
  display: flex;
  margin: 20px;
  flex-direction: column;
`;

const AuthorName = styled.h1`
  font-size: 15px;
`;

const ReplyDate = styled.h1`
  font-size: 12px;
`;

function Reply(props) {
  // we have userId in props.authorId for private message
  return (
    <ReplyContainer>
      <AuthorContainer>
        <AuthorName>{props.authorName}</AuthorName>
        <Avatar
          alt={props.authorName}
          sx={{ bgcolor: "#95B2B8", color: "#FFF", width: 150, height: 150 }}
        >
          {props.authorName.charAt(0)}
        </Avatar>
        <IconButton
          color="primary"
          aria-label="send private message"
          component="span"
        >
          <SendIcon />
        </IconButton>
        <ReplyDate>{props.date}</ReplyDate>
      </AuthorContainer>
      <ReplyContent>
        <ReplyBody>{props.content}</ReplyBody>
      </ReplyContent>
    </ReplyContainer>
  );
}

class Topic extends Component {
  constructor(props) {
    super(props);
    this.state = { topic: null };
  }

  getTopic = async () => {
    try {
      const params = window.location.pathname;
      const pattern = /f(\d)\/t+(\d)/g;
      const id = pattern.exec(params);
      const response = await fetch(`/api/DiscussionForum/Forum/${id[1]}`, {
        method: "GET",
      });
      const topicData = await response.json();
      this.setState({ topicLoaded: true, topic: topicData[0] });
      console.log(topicData);
    } catch (error) {
      console.error(error);
    }
  };

  getReplies = async () => {
    try {
      const params = window.location.pathname;
      const pattern = /f(\d)\/t+(\d)/g;
      const id = pattern.exec(params);
      const response = await fetch(`/api/DiscussionForum/Topic/${id[2]}`, {
        method: "GET",
      });
      const repliesData = await response.json();
      console.log(repliesData);
      this.setState({ repliesLoaded: true, replies: repliesData });
    } catch (error) {
      console.error(error);
    }
  };

  componentDidMount() {
    if (!this.state.topic) {
      this.getTopic();
    }
    if (!this.state.replies) {
      this.getReplies();
    }
    if (this.state.replies && this.state.topic) {
      this.setState({});
    }
  }

  render() {
    const { topicLoaded, topic, replies } = this.state;
    if (!topicLoaded) {
      return <div>Loading forum...</div>;
    } else {
      return (
        <>
          <Category>
            <CategoryHeaderContainer>
              <CategoryTitle>{topic.Subject}</CategoryTitle>
              <IconButton
                aria-label="create thread"
                sx={{
                  color: "#FFF",
                }}
              >
                <AddIcon />
              </IconButton>
            </CategoryHeaderContainer>
          </Category>
          <Reply
            key="topic"
            authorName={topic.Author.UserName}
            authorId={topic.Author.Id}
            content={topic.Content}
            date={topic.Time}
          />
          {replies &&
            replies.length > 0 &&
            replies.map((reply) => (
              <Reply
                key={reply.DiscussionReplyId}
                authorName={reply.Author.UserName}
                authorId={reply.Author.Id}
                content={reply.Content}
                date={reply.Time}
              />
            ))}
        </>
      );
    }
  }
}

export default Topic;
