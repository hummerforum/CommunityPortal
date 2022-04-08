import React, { Component } from 'react'
/*import Select from 'react-select'*/
import Select from '@mui/material/Select';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import InputLabel from '@mui/material/InputLabel';
import axios from 'axios'

export default class ReceiversDropdown extends Component {

    constructor(props) {
        super(props)
        this.state = {
            selectOptions: [],
            selectedValue: ""
        }
    }

    async getOptions() {
        const response = await axios.get('https://localhost:5001/api/PrivateMessages/GetAllUsers');
        const responseData = response.data;
        const options = responseData.map(data => ({
            "userid": data.Id,
            "username": data.UserName
        }))

        this.setState({ selectOptions: options });

    }

    handleChange(event) {
        this.setState({ selectedValue: event.target.value })

        this.state.selectOptions.forEach((option) => {
            if (event.target.value === option.userid) {
                this.props.getReceiverData(event.target.value, option.username);
            }

        });
    }

    componentDidMount() {
        this.getOptions();
    }

    render() {
        return (
            <FormControl fullWidth>
                <InputLabel id="to-user-select-label">To user</InputLabel>
                <Select
                    labelId="to-user-select-label"
                    id="to-user-simple-select"
                    value={this.state.selectedValue}
                    label="To user"
                    onChange={this.handleChange.bind(this)}
                >
                    {this.state.selectOptions.length > 0 ?
                        this.state.selectOptions.map((option) => (
                            <MenuItem key={option.userid} value={option.userid}>{option.username}</MenuItem>
                        ))
                        : null
                    }
                </Select>
            </FormControl>
        )
    }
}