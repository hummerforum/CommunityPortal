import styled from "styled-components";
import React from "react";
import AddIcon from "@mui/icons-material/Add";
import IconButton from "@mui/material/IconButton";

const Category = styled.div`
  &:first-child {
    margin-top: 35px;
  }
  margin-top: 15px;
`;

const CategoryHeaderStyle = styled.div``;

const ThreadContainer = styled.div`
  display: grid;
  grid-template-columns: 90% 10%;
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

const ThreadReplies = styled.div`
  display: flex;
  flex-direction: column;
`;

function Thread(props) {
  return (
    <ThreadContainer>
      <ThreadData>
        <ThreadName>{props.name}</ThreadName>
        <ThreadInfo>
          <ThreadAuthor>{props.author},</ThreadAuthor>
          <ThreadDate>{props.date}</ThreadDate>
        </ThreadInfo>
      </ThreadData>
      <ThreadReplies>
        <CategoryListStats>{props.replies}</CategoryListStats>
        <CategoryListText>Replies</CategoryListText>
      </ThreadReplies>
    </ThreadContainer>
  );
}

function ThreadList(props) {
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
        <Thread
          name="Lorem ipsum dolor sit amet, consectetur adipiscing elit."
          author="Sven"
          date="2010-02-24"
          replies={45}
        />
        <Thread
          name="Lorem ipsum dolor sit amet, consectetur adipiscing elit."
          author="Sven"
          date="2010-02-24"
          replies={45}
        />
        <Thread
          name="Lorem ipsum dolor sit amet, consectetur adipiscing elit."
          author="Sven"
          date="2010-02-24"
          replies={45}
        />
        <Thread
          name="Lorem ipsum dolor sit amet, consectetur adipiscing elit."
          author="Sven"
          date="2010-02-24"
          replies={45}
        />
        <Thread
          name="Lorem ipsum dolor sit amet, consectetur adipiscing elit."
          author="Sven"
          date="2010-02-24"
          replies={45}
        />
        <Thread
          name="Lorem ipsum dolor sit amet, consectetur adipiscing elit."
          author="Sven"
          date="2010-02-24"
          replies={45}
        />
        <Thread
          name="Lorem ipsum dolor sit amet, consectetur adipiscing elit."
          author="Sven"
          date="2010-02-24"
          replies={45}
        />
        <Thread
          name="Lorem ipsum dolor sit amet, consectetur adipiscing elit."
          author="Sven"
          date="2010-02-24"
          replies={45}
        />
        <Thread
          name="Lorem ipsum dolor sit amet, consectetur adipiscing elit."
          author="Sven"
          date="2010-02-24"
          replies={45}
        />
      </Category>
    </>
  );
}

export default ThreadList;
