import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import Button from "@mui/material/Button";
import { LoginMenu } from "../components/api-authorization/LoginMenu";
import Link from "@mui/material/Link";
import { Link as RouterLink } from "react-router-dom";

const NavMenu = () => {
    return (
        <AppBar position="static">
            <Container
                maxWidth="xl"
                sx={{
                    bgcolor: "#70163C",
                }}
            >
                <Toolbar
                    disableGutters
                    sx={{
                        bgcolor: "#70163C",
                    }}
                >
                    <Link component={RouterLink} to="/" underline="none">
                        <Typography
                            variant="h6"
                            noWrap
                            component="div"
                            sx={{ mr: 2, display: { xs: "none", md: "flex" }, color: "#FFF" }}
                        >
                            Hummer
                        </Typography>
                    </Link>
                    <Box sx={{ flexGrow: 1, display: { xs: "none", md: "flex" } }}>
                        <Button sx={{ my: 2, color: "white", display: "block" }} component={RouterLink} to="/news">
                            News
                        </Button>
                        <Button sx={{ my: 2, color: "white", display: "block" }} component={RouterLink} to="/forum">
                            Forum
                        </Button>
                        <Button sx={{ my: 2, color: "white", display: "block" }} component={RouterLink} to="/fetch-data">
                            Fetch-data (AUTH CHECK)
                        </Button>
                    </Box>
                    <Button sx={{ my: 2, color: "white", display: "block" }} component={RouterLink} to="/messages">
                        Messages
                    </Button>
                    <LoginMenu />
                </Toolbar>
            </Container>
        </AppBar>
    );
};

export default NavMenu;
