import styled from "styled-components";
import React from "react";
import Link from "@mui/material/Link";
import { Link as RouterLink } from "react-router-dom";

const Category = styled.div`
  &:first-child {
    margin-top: 35px;
  }
  margin-top: 15px;
`;

const CategoryHeaderStyle = styled.div`
  background-color: #70163c;
  color: #fff;
  font-size: 23px;
  padding: 5px;
  border: 1px solid #000;
`;

const CategoryListContainer = styled.div`
  display: grid;
  grid-template-columns: 60% 20% 20%;
  background: #eff0f1;
  align-items: center;
  margin-bottom: 5px;
  margin-top: 5px;
  border: 1px solid #a2a3a4;
  height: 50px;
`;

const CategoryListName = styled.div`
  font-weight: bold;
  font-size: 15px;
  padding-left: 5px;
`;

const CategoryListThreads = styled.div`
  text-align: center;
  display: flex;
  flex-direction: column;
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

const CategoryListReplies = styled.div`
  display: flex;
  flex-direction: column;
`;

function CategoryHeader(props) {
  return <CategoryHeaderStyle>{props.name}</CategoryHeaderStyle>;
}

function CategoryList(props) {
  return (
    <Link
      component={RouterLink}
      to="/forum/f1"
      underline="none"
      sx={{
        color: "#000",
      }}
    >
      <CategoryListContainer>
        <CategoryListName>{props.name}</CategoryListName>
        <CategoryListThreads>
          <CategoryListStats>{props.threads}</CategoryListStats>
          <CategoryListText>Threads</CategoryListText>
        </CategoryListThreads>
        <CategoryListReplies>
          <CategoryListStats>{props.replies}</CategoryListStats>
          <CategoryListText>Replies</CategoryListText>
        </CategoryListReplies>
      </CategoryListContainer>
    </Link>
  );
}

function ForumList(props) {
  return (
    <>
      <Category>
        <CategoryHeader name="Fishing" />
        <CategoryList name="Lobster" threads={4} replies={45} />
        <CategoryList name="Cod" threads={4} replies={45} />
        <CategoryList name="Anchovy" threads={4} replies={45} />
      </Category>
      <Category>
        <CategoryHeader name="Cars" />
        <CategoryList name="Volvo" threads={4} replies={45} />
        <CategoryList name="SAAB" threads={4} replies={45} />
        <CategoryList name="BMW" threads={4} replies={45} />
      </Category>
      <Category>
        <CategoryHeader name="Gaming" />
        <CategoryList name="World of Warcraft" threads={4} replies={45} />
        <CategoryList name="Super Mario 64" threads={4} replies={45} />
        <CategoryList name="Counter-Strike" threads={4} replies={45} />
      </Category>
      <Category>
        <CategoryHeader name="Beer" />
        <CategoryList name="IPA" threads={4} replies={45} />
        <CategoryList name="Gose" threads={4} replies={45} />
        <CategoryList name="Stout" threads={4} replies={45} />
      </Category>
    </>
  );
}

export default ForumList;
