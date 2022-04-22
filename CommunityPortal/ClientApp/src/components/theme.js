import { createTheme } from '@material-ui/core/styles'

const theme = createTheme({
    overrides: {
        MuiPickersToolbar: {
            toolbar: {
                backgroundColor: "#70163C",
            },
        },
        MuiPickersCalendarHeader: {
            switchHeader: {
                backgroundColor: "#70163C",
                color: "white",
            },
        },
        MuiLink: {
            color: "#FFF",
        },
        MuiButton: {
            color: "#FFF",
        },
        palette: {
            primary: {
              main: "#FFF",
            },
            secondary: {
              main: "",
            },
          },
    },
});

export default theme;