////för att installera först: @material-ui / pickers in ClientApp
// sedan: npm i @date-io / date - fns@1.x date - fns

import React, { useState } from 'react';
import DateFnsUtils from '@date-io/date-fns'; // choose your lib
import {
    DatePicker,
    TimePicker,
    DateTimePicker,
    MuiPickersUtilsProvider,
} from '@material-ui/pickers';





function Calender() {
    const [selectedDate, handleDateChange] = useState(new Date());
    const [Dates, ChangeDates] = useState("");
    return (
        <MuiPickersUtilsProvider utils={DateFnsUtils}>
            <DatePicker
                variant="static"
                value={selectedDate}
                onChange={handleDateChange}

            />
        </MuiPickersUtilsProvider>
    );
}


export default Calender;

