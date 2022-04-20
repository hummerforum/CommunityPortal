import React, { Component } from "react";
import ReactDOM from "react-dom";
import Grid from "@mui/material/Grid";
import Container from "@mui/material/Container";
import Typography from "@mui/material/Typography";
import FormControl from "@mui/material/FormControl";
import FormControlLabel from "@mui/material/FormControlLabel";
import Checkbox from "@mui/material/Checkbox";
import TextField from "@mui/material/TextField";
import Button from "@mui/material/Button";
import ListNewsPosts from "./ListNewsPosts";
import authService from "../../components/api-authorization/AuthorizeService";

export class NewsPostForm extends Component {
    static displayName = NewsPostForm.name;

    constructor(props) {
        super(props);
        this.state = {
            userName: null,
            userRole: null,
            isLoaded: false,
            newsPost: null,
            isCategoriesLoaded: false,
            categories: [],
            category: null,
            NewsPostId: 0,
            IsEvent: false,
            Heading: "",
            Information: "",
            Description: "",
            Tag: "",
            CategoryId: 1,
            CreatedDate: null
        };
    }

    async authHeader() {
        const currentUser = await authService.getUser();
        if (currentUser && currentUser.access_token) {
            return { Authorization: `Bearer ${currentUser.access_token}` };
        } else {
            return {};
        }
    }

    async getUserName() {
        const currentUser = await authService.getUser();
        this.setState({ userName: currentUser.name });
    }

    async getUserRole() {
        this.setState({ userRole: await authService.getRole() });
    }

    componentDidMount() {
        this.getUserName();
        this.getUserRole();
        if (this.props.id != null) {
            this.getNewsPost();
        } else {
            if (this.props.categoryId != null) {
                this.setState({ CategoryId: this.props.categoryId });
            }
            this.setState({ isLoaded: true });
        }
        this.getCategories();
    }

    async getNewsPost() {
        const id = this.props.id;
        const response = await fetch("/api/newspost/" + id, {
            method: "GET"
        });
        const newsPost = await response.json();
        this.setState({
            NewsPostId: newsPost.NewsPostId,
            IsEvent: newsPost.IsEvent,
            Heading: newsPost.Heading,
            Information: newsPost.Information,
            CategoryId: newsPost.CategoryId,
            Description: newsPost.Description,
            Tag: newsPost.Tag,
            CreatedDate: newsPost.CreatedDate,
            isLoaded: true
        });
    }

    async getCategory(id) {
        const response = await fetch("/api/category/" + id, {
            method: 'GET'
        });
        const categoryData = await response.json();
        this.setState({ category: categoryData });
    }

    async getCategories() {
        const response = await fetch("/api/category", {
            method: "GET"
        });
        const categoryData = await response.json();
        this.setState({ categories: categoryData });
        if (this.props.categoryId != null) {
            await this.getCategory(this.props.categoryId);
        }
        this.setState({ isCategoriesLoaded: true });
    }

    clickBackToList = () => {
        ReactDOM.unmountComponentAtNode(document.getElementById("NewsPostView"));
        ReactDOM.render(
            <ListNewsPosts categoryId={this.props.categoryId} />,
            document.getElementById("NewsPostView")
        );
    }

    async addNewsPost() {
        const header = await authService.getUser();
        const data = this.state;
        const currentDate = new Date();
        await fetch("/api/newspost", {
            method: "POST",
            headers: {
                header,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                IsEvent: data.IsEvent,
                Heading: data.Heading,
                Information: data.Information,
                CategoryId: data.CategoryId,
                UserName: data.userName,
                CreatedDate: currentDate.toISOString(),
                UpdatedDate: currentDate.toISOString(),
                Description: data.Description,
                Tag: data.Tag
            }),
        });
    }

    async updateNewsPost() {
        const header = await authService.getUser();
        const data = this.state;
        const currentDate = new Date();
        await fetch("/api/newspost", {
            method: "PUT",
            headers: {
                header,
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                NewsPostId: parseInt(data.NewsPostId),
                IsEvent: data.IsEvent,
                Heading: data.Heading,
                Information: data.Information,
                CategoryId: data.CategoryId,
                UserName: data.userName,
                CreatedDate: data.CreatedDate,
                UpdatedDate: currentDate.toISOString(),
                Description: data.Description,
                Tag: data.Tag
            })
        });
    }

    changeField = (event) => {
        switch (event.target.id) {
            case "FormHeading":
                this.setState({ Heading: event.target.value });
                break;

            case "FormInformation":
                this.setState({ Information: event.target.value });
                break;

            case "FormTag":
                this.setState({ Tag: event.target.value });
                break;

            case "FormDescription":
                this.setState({ Description: event.target.value });
                break;

            default:
                this.setState({ IsEvent: event.target.checked });
                break;
        }
    };

    async submitForm(event) {
        event.preventDefault();
        const data = this.state;
        if (data.NewsPostId == 0) {
            await this.addNewsPost();
        } else {
            await this.updateNewsPost();
        }
        ReactDOM.unmountComponentAtNode(document.getElementById("NewsPostView"));
        ReactDOM.render(<ListNewsPosts categoryId={this.props.categoryId} />, document.getElementById("NewsPostView"));
    };

    render() {
        const { userRole, isLoaded, isCategoriesLoaded, category,
            IsEvent, NewsPostId, Heading, Information, Description, Tag } = this.state;
        if (!isLoaded) {
            return <div>Loading news post...</div>;
        } else if (!isCategoriesLoaded) {
            return <div>Loading categories...</div>;
        } else {
            return (
                <Container>
                    <Grid
                        container
                        direction="column"
                        justifyContent="space-evenly"
                        alignItems="center"
                    >

                        <Typography variant="h2" component="div" gutterBottom>
                            {NewsPostId == 0 ? ("Add news post") : ("Edit news post")}
                        </Typography>

                        <Button size="small" color="primary" onClick={() => this.clickBackToList()}>
                            Back to news list
                        </Button>

                        <form sx={{ mt: 1.0 }} onSubmit={async event => { this.submitForm(event) }}>
                            {category != null ? (
                                <Typography sx={{ mt: 2.75 }} variant="h5" component="div" gutterBottom>Category: {category.Title}</Typography>
                            ) : ("")}
                            <FormControlLabel
                                sx={{ mt: 1.5 }}
                                id="FormIsEvent"
                                control={<Checkbox defaultChecked={IsEvent} />}
                                label="Event"
                                onChange={e => this.changeField(e)} />
                            <br />
                            <FormControl sx={{ mt: 1.5 }}>
                                <TextField
                                    id="FormHeading"
                                    style={{ width: "400px", margin: "5px" }}
                                    type="text"
                                    required
                                    label="Header"
                                    value={this.state.value}
                                    defaultValue={Heading}
                                    variant="outlined"
                                    onChange={e => this.changeField(e)}
                                />
                            </FormControl>
                            <br />
                            <FormControl sx={{ mt: 1.5 }}>
                                <TextField
                                    id="FormInformation"
                                    style={{ width: "400px", margin: "5px" }}
                                    type="text"
                                    required
                                    label="Information"
                                    value={this.state.value}
                                    defaultValue={Information}
                                    variant="outlined"
                                    multiline
                                    rows={10}
                                    onChange={e => this.changeField(e)}
                                />
                            </FormControl>
                            <br />
                            <FormControl sx={{ mt: 1.5 }}>
                                <TextField
                                    id="FormTag"
                                    style={{ width: "400px", margin: "5px" }}
                                    type="text"
                                    label="Tag"
                                    value={this.state.value}
                                    defaultValue={Tag}
                                    variant="outlined"
                                    onChange={e => this.changeField(e)}
                                />
                            </FormControl>
                            <br />
                            <FormControl sx={{ mt: 1.5 }}>
                                <TextField
                                    id="FormDescription"
                                    style={{ width: "400px", margin: "5px" }}
                                    type="text"
                                    label="RSS description"
                                    value={this.state.value}
                                    defaultValue={Description}
                                    variant="outlined"
                                    multiline
                                    rows={5}
                                    onChange={e => this.changeField(e)}
                                />
                            </FormControl>
                            <br />
                            {userRole === "Admin" || userRole === "Moderator" ? (
                                <Button sx={{ mt: 1.5 }} variant="contained" color="primary" type="submit">
                                    Save
                                </Button>
                            ) : ("")}
                        </form>
                    </Grid>
                </Container>
            );
        }
    }
}

export default NewsPostForm;
