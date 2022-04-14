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
  height: 50px;
`;

const CategoryListContainer = styled.div`
  display: grid;
  grid-template-columns: 20% 80%;
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

const CategoryListDescription = styled.div`
  font-size: 12px;
`;

function CategoryHeader(props) {
  return <CategoryHeaderStyle>{props.name}</CategoryHeaderStyle>;
}

function CategoryList(props) {
  return (
    <Link
      component={RouterLink}
      to={`/forum/f${props.id}`}
      underline="none"
      sx={{
        color: "#000",
      }}
    >
      <CategoryListContainer>
        <CategoryListName>{props.name}</CategoryListName>
        <CategoryListDescription>{props.description}</CategoryListDescription>
      </CategoryListContainer>
    </Link>
  );
}

class ForumList extends React.Component {
  constructor(props) {
    super(props);
    this.state = { overview: null };
  }

  arrayIncludesInObj = (arr, key, valueToCheck) => {
    return arr.some((value) => value[key] === valueToCheck);
  };

  getOverview = async () => {
    try {
      const response = await fetch("/api/DiscussionForum/Overview/", {
        method: "GET",
      });
      const overviewData = await response.json();
      const forumData = [];
      // this could probably be done backend wise but let's just do it here for now
      for (const x in overviewData) {
        if (
          !this.arrayIncludesInObj(
            forumData,
            "name",
            overviewData[x].DiscussionCategory.Name
          )
        ) {
          forumData.push({
            name: overviewData[x].DiscussionCategory.Name,
            id: overviewData[x].DiscussionCategory.DiscussionCategoryId,
            forum: [],
          });
        }
      }
      for (const x in overviewData) {
        for (const y in forumData) {
          if (forumData[y].name === overviewData[x].DiscussionCategory.Name) {
            forumData[y].forum.push({
              name: overviewData[x].Name,
              id: overviewData[x].DiscussionForumId,
              description: overviewData[x].Description,
            });
          }
        }
      }
      this.setState({ overview: forumData, isLoaded: true });
    } catch (error) {
      console.error(error);
    }
  };

  componentDidMount() {
    if (!this.state.overview) {
      this.getOverview();
    }
  }

  render() {
    const { isLoaded, overview } = this.state;
    if (!isLoaded) {
      return <div>Loading forum...</div>;
    } else {
      return (
        <>
          {overview.map((overview) => (
            <Category key={`category-${overview.id}`}>
              <CategoryHeader
                name={overview.name}
                key={`category-${overview.id}`}
              />
              {overview.forum.map((forum) => (
                <CategoryList
                  name={forum.name}
                  description={forum.description}
                  key={forum.id}
                  id={forum.id}
                />
              ))}
            </Category>
          ))}
        </>
      );
    }
  }
}

export default ForumList;
