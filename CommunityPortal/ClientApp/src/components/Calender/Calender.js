// npm i @material-ui / pickers@3.2.10 --save
// npm install date - fns@2.16.1 --save
// npm install @date-io / date - fns@1.3.13 --save
// npm install @material-ui / styles


import React, { useState, useEffect, useRef } from 'react';
import DateFnsUtils from '@date-io/date-fns'; // choose your lib
import {
    DatePicker,
    MuiPickersUtilsProvider,
} from '@material-ui/pickers';

import { createMuiTheme } from "@material-ui/core";
import { ThemeProvider } from "@material-ui/styles";
import ReactDOM from "react-dom";
import ListNewsPosts from "../newsposts/ListNewsPosts";


//import Container from "@mui/material/Container";
//import Table from "@mui/material/Table";
//import TableBody from "@mui/material/TableBody";
//import TableCell from "@mui/material/TableCell";
//import TableHead from "@mui/material/TableHead";
//import TableRow from "@mui/material/TableRow";
//import TableContainer from "@mui/material/TableContainer";
//import Typography from "@mui/material/Typography";
//import Grid from "@mui/material/Grid";
//import { Button } from "@mui/material";

const materialTheme = createMuiTheme({
    overrides: {
        MuiPickersToolbar: {
            toolbar: {
                backgroundColor: "white",
            },
        },
        MuiPickersCalendarHeader: {
            switchHeader: {
                backgroundColor: "#70163C",
                color: "white",
            },
        },
    },
});



function Calender() {

    const [selectedDate, handleDateChange] = useState(new Date());
    const [newsposts, ChangeNewsposts] = useState([]);
    const [showList, SetShowList] = useState(false);
    const mounted = useRef(false);
    const [counter, setCounter] = useState(0);


    useEffect(() => {
        Load();
    }, []
    );


    async function Load() {
        mounted.current = true;
        const response = await fetch("/api/newspost/", {
            method: "GET",
        });
        const newsPostData = await response.json();
        ChangeNewsposts(newsPostData);
        SetShowList(false);
        console.log(newsPostData);
    }


    //async function LoadNewsPosts(date) {

    //    if (date == null)
    //        return;

    //    //alert(date);

    //    //const response = await fetch("/api/newspost/GetByDate" + date, {
    //    //    method: "GET",
    //    //    body: JSON.stringify({date}),
    //    //});

    //   //const ListnewsPostData = await response.json();
    //    //console.log("LoadNewsPosts" + date);
    //    //console.log(ListnewsPostData);

    //}



    //function closeList() {

    //    SetShowList(false);
    //}




    function accept(val) {

        setCounter(counter + 1);

        if (counter > 0) {
            handleDateChange(val);
            ReactDOM.unmountComponentAtNode(document.getElementById("NewsPostView"));
            ReactDOM.render(<ListNewsPosts categoryId={null} selectedDate={val} />, document.getElementById("NewsPostView"));
        }

    }

    const disableDates = (date) => {
        var date2 = date.toISOString().substring(0, 10);

        var v = newsposts.find(item => {
            return (item.CreatedDate.substring(0, 10)) === date2
        })
        if (v == null)
            return true;
        else {
            return false;
        }
    }


    return (


        //<Container>

        //    {showList &&
        //        <Grid
        //            container
        //            direction="column"
        //            justifyContent="space-evenly"
        //            alignItems="center"
        //        >

        //            <Button onClick={closeList}   > View Calender</Button>

        //            <TableContainer>
        //                <Table aria-label="simple table">
        //                <TableHead> News Post 
        //                        <TableRow >
        //                            <TableCell>User</TableCell>
        //                        </TableRow>
        //                    </TableHead>
        //                    <TableBody>
        //                        <TableRow>
        //                            <TableCell component="th" scope="row">
        //                            </TableCell>
        //                        </TableRow>
        //                    </TableBody>
        //                </Table>
        //            </TableContainer>


        //        </Grid>
        //    }

        //    {!showList &&
        <MuiPickersUtilsProvider utils={DateFnsUtils}>
            <ThemeProvider theme={materialTheme}>
                <DatePicker
                    label="New Post Days"
                    variant="static"
                    value={selectedDate}
                    onChange={accept}
                    maxDate={Date.now()}
                    color="secondary"
                    emptyLabel="Empty"
                    shouldDisableDate={disableDates}
                />
            </ThemeProvider>
        </MuiPickersUtilsProvider>
        //    }
        //</Container>
    );

}


export default Calender;