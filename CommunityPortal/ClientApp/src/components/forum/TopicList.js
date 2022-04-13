import styled from "styled-components";
import React, { Component } from "react";
import Link from "@mui/material/Link";
import { Link as RouterLink } from "react-router-dom";
import AddIcon from "@mui/icons-material/Add";
import IconButton from "@mui/material/IconButton";

const Category = styled.div`
  &:first-child {
    margin-top: 35px;
  }
  margin-top: 15px;
`;

const ThreadContainer = styled.div`
  display: grid;
  grid-template-columns: 80% 10% 10%;
  background: #eff0f1;
  align-items: center;
  margin-bottom: 5px;
  margin-top: 5px;
  border: 1px solid #a2a3a4;
  height: 50px;
`;

const ThreadData = styled.div`
  padding-left: 5px;
`;

const ThreadInfo = styled.div`
  display: flex;
  align-items: center;
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

const CategoryTitle = styled.div`
  padding-left: 5px;
`;

const ThreadName = styled.div`
  font-weight: bold;
  font-size: 15px;
`;

const ThreadAuthor = styled.div`
  font-size: 14px;
  font-weight: bold;
`;

const ThreadDate = styled.div`
  font-size: 13px;
  padding-left: 5px;
  color: #989ca0;
`;

const CategoryListStats = styled.div`
  text-align: center;
  font-size: 19px;
  margin-bottom: -5px;
`;

const CategoryListText = styled.div`
  text-align: center;
  font-size: 12px;
`;

const ThreadViews = styled.div`
  display: flex;
  flex-direction: column;
`;

const ThreadReplies = styled.div`
  display: flex;
  flex-direction: column;
`;

function Thread(props) {
  return (
    <Link
      component={RouterLink}
      to={`/forum/f${props.forumId}/t${props.topicId}`}
      underline="none"
      sx={{
        color: "#000",
      }}
    >
      <ThreadContainer>
        <ThreadData>
          <ThreadName>{props.name}</ThreadName>
          <ThreadInfo>
            <ThreadAuthor>{props.author},</ThreadAuthor>
            <ThreadDate>{props.date}</ThreadDate>
          </ThreadInfo>
        </ThreadData>
        <ThreadViews>
          <CategoryListStats>{props.views}</CategoryListStats>
          <CategoryListText>Views</CategoryListText>
        </ThreadViews>
        <ThreadReplies>
          <CategoryListStats>{props.replies}</CategoryListStats>
          <CategoryListText>Replies</CategoryListText>
        </ThreadReplies>
      </ThreadContainer>
    </Link>
  );
}

class TopicList extends Component {
  constructor(props) {
    super(props);
    this.state = { topics: null };
  }

  getTopics = async () => {
    try {
      const params = window.location.pathname;
      const pattern = /(?<=\/forum\/f)(\d)/g;
      const id = params.match(pattern).toString();
      const response = await fetch(`/api/DiscussionForum/Forum/${id}`, {
        method: "GET",
      });
      const topicsData = await response.json();
      this.setState({ topics: topicsData, isLoaded: true });
      console.log(topicsData);
    } catch (error) {
      console.error(error);
    }
  };

  componentDidMount() {
    if (!this.state.topics) {
      this.getTopics();
    }
  }

  render() {
    const { isLoaded, topics } = this.state;
    if (!isLoaded) {
      return <div>Loading forum...</div>;
    } else {
      return (
        <>
          <Category>
            <CategoryHeaderContainer>
              <CategoryTitle>Lobster</CategoryTitle>
              <IconButton
                aria-label="create thread"
                sx={{
                  color: "#FFF",
                }}
              >
                <AddIcon />
              </IconButton>
            </CategoryHeaderContainer>
            {topics.length <= 0 && (
              <h3>
                This forum is empty so far, maybe you can create some topics!
              </h3>
            )}
            {topics.map((topic) => (
              <Thread
                key={topic.DiscussionTopicId}
                name={topic.Subject}
                author={topic.Author.UserName}
                date={topic.Time}
                topicId={topic.DiscussionTopicId}
                forumId={topic.DiscussionForumId}
                views={topic.NrOfViews}
                replies={45}
              />
            ))}
          </Category>
        </>
      );
    }
  }
}

export default TopicList;
