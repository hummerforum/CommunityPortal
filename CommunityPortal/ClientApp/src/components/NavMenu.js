import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import Button from "@mui/material/Button";
import { LoginMenu } from "../components/api-authorization/LoginMenu";
import { MessagesLink } from "../components/messages/MessagesLink";
import Link from "@mui/material/Link";
import { Link as RouterLink } from "react-router-dom";
import authService from "../components/api-authorization/AuthorizeService";
import { useState } from "react";
import { ThemeProvider } from "@material-ui/styles";
import theme from "./theme";

const NavMenu = () => {
  const CheckRole = async () => {
    const currentUser = await authService.getRole();
    if (currentUser === "Admin") {
      setRole("Admin");
    } else {
      setRole("User");
    }
  };

  const [role, setRole] = useState("User");
  CheckRole();

  return (
    <ThemeProvider theme={theme}>
    <AppBar
      position="static"
      sx={{
        bgcolor: "#70163C",
      }}
    >
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
            <Button
              sx={{ my: 2, color: "white", display: "block" }}
              component={RouterLink}
              to="/news"
            >
              News
            </Button>
            <Button
              sx={{ my: 2, color: "white", display: "block" }}
              component={RouterLink}
              to="/forum"
            >
              Forum
            </Button>

            {role === "Admin" ? (
              <Button
                sx={{ my: 2, color: "white", display: "block" }}
                component={RouterLink}
                to="/community-users"
              >
                {" "}
                Users{" "}
              </Button>
            ) : (
              " "
            )}
          </Box>

          <MessagesLink />
          <LoginMenu />
        </Toolbar>
      </Container>
    </AppBar>
    </ThemeProvider>
  );
};

export default NavMenu;
