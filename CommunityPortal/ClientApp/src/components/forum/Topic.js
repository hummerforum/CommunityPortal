import styled from "styled-components";
import React, { Component } from "react";
import AddIcon from "@mui/icons-material/Add";
import MessageIcon from "@mui/icons-material/Message";
import IconButton from "@mui/material/IconButton";
import Avatar from "@mui/material/Avatar";
import Tooltip from "@mui/material/Tooltip";
import { withRouter } from "../../withRouter";
import authService from "../../components/api-authorization/AuthorizeService";
import CreateReply from "./CreateReply";
import { formatRelative } from 'date-fns'

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

function SendPm(navigate, authorId, date, content) {
  let uri = `/messages/${authorId}/Reply%20to%20post%20posted%20at:%20${date}/Reply%20to%20message:%20${content.replace(
    /[/?.]/g,
    ""
  )}%0A----------%0A`;
  navigate(uri);
}

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
        {props.isAuthenticated ? (
          <Tooltip disableFocusListener title="Send PM">
            <IconButton
              color="primary"
              aria-label="send private message"
              component="span"
              onClick={() => {
                SendPm(
                  props.navigate,
                  props.authorId,
                  props.date,
                  props.content
                );
              }}
            >
              <MessageIcon />
            </IconButton>
          </Tooltip>
        ) : null}
        <ReplyDate>{formatRelative(Date.parse(props.date), Date.now())}</ReplyDate>
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
    this.state = {
      isAuthenticated: false,
      topic: null,
    };
  }

  isCorrect(topics, topic) {
    return topics.DiscussionTopicId === topic;
  }

  getTopic = async () => {
    try {
      const params = window.location.pathname;
      const pattern = /f(\d+)\/t+(\d+)/g;
      const id = pattern.exec(params);
      const response = await fetch(`/api/DiscussionForum/Topic/${id[2]}`, {
        method: "GET",
      });
      const topicData = await response.json();
      this.setState({ topicLoaded: true, topic: topicData[0]});
    } catch (error) {
      console.error(error);
    }
  };

  getReplies = async () => {
    try {
      const params = window.location.pathname;
      const pattern = /f(\d+)\/t+(\d+)/g;
      const id = pattern.exec(params);
      const response = await fetch(`/api/DiscussionForum/Replies/${id[2]}`, {
        method: "GET",
      });
      const repliesData = await response.json();
      this.setState({ repliesLoaded: true, replies: repliesData });
    } catch (error) {
      console.error(error);
    }
  };

  componentDidMount() {
    this._subscription = authService.subscribe(() => this.populateState());
    this.populateState();

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

  componentWillUnmount() {
    authService.unsubscribe(this._subscription);
  }

  async populateState() {
    const [isAuthenticated] = await Promise.all([
      authService.isAuthenticated(),
    ]);
    this.setState({
      isAuthenticated,
    });
  }

  scrollToReply() {
    const scrollingElement = (document.scrollingElement || document.body);
    scrollingElement.scrollTop = scrollingElement.scrollHeight;
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
              {this.state.isAuthenticated && (
                <IconButton
                  aria-label="create thread"
                  sx={{
                    color: "#FFF",
                  }}
                  onClick={this.scrollToReply}
                >
                  <AddIcon />
                </IconButton>
              )}
            </CategoryHeaderContainer>
          </Category>
          <Reply
            key="topic"
            authorName={topic.Author.UserName}
            authorId={topic.Author.Id}
            content={topic.Content}
            date={topic.Time}
            navigate={this.props.navigate}
            isAuthenticated={this.state.isAuthenticated}
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
                navigate={this.props.navigate}
                isAuthenticated={this.state.isAuthenticated}
              />
            ))}
          {this.state.isAuthenticated && (
            <CreateReply replyTo={topic.DiscussionTopicId} />
          )}
        </>
      );
    }
  }
}

export default withRouter(Topic);
