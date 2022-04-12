import React, { Component } from 'react'
import FormControl from '@mui/material/FormControl';
import FormHelperText from '@mui/material/FormHelperText';
import Autocomplete from '@mui/material/Autocomplete';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import axios from 'axios'

export default class ReceiversDropdown extends Component {

    constructor(props) {
        super(props)
        this.state = {
            selectOptions: [],
            selectedValue: null,
            receiverLabel: "Receiver",
            receiverHelperText: "",
            receiverError: false
        }
    }

    async getOptions() {
        const response = await axios.get('https://localhost:5001/api/PrivateMessages/GetAllUsers');
        const responseData = response.data;
        const options = responseData.map(data => ({
            "userid": data.Id,
            "username": data.UserName
        }))

        this.setState({
            selectOptions: options
        });

        if (this.props.match) {
            if (this.props.match.params.receiverid) {
                this.state.selectOptions.forEach((option) => {
                    if (option.userid === this.props.match.params.receiverid) {
                        this.setState({
                            selectedValue: option
                        });

                        this.props.getReceiverData(option.userid, option.username);
                    }
                });
            }
        }

    }

    handleChange(event, selectedValue) {
        if (selectedValue) {
            this.setState({ selectedValue: selectedValue })
            this.props.getReceiverData(selectedValue.userid, selectedValue.username);
        }

        this.validateReceiver(selectedValue);
    }

    handleBlur() {
        this.validateReceiver(this.state.selectedValue);
    }

    validateReceiver(selectedValue) {
        if (selectedValue) {
            this.setState({
                receiverLabel: "Receiver",
                receiverHelperText: "",
                receiverError: false
            });
        } else {
            this.setState({
                receiverLabel: "Error",
                receiverHelperText: "The message must have a receiver!",
                receiverError: true
            });
        }
    }

    componentDidMount() {
        this.getOptions();
    }

    componentDidUpdate(prevProps, prevState) {
        if (prevProps.validationToggle !== this.props.validationToggle) {
            this.validateReceiver(this.state.selectedValue);
        }
    }

    render() {
        return (
            <FormControl fullWidth sx={{ mb: 1.5 }}>
                <Autocomplete
                    options={this.state.selectOptions}
                    autoHighlight
                    getOptionLabel={(option) => option.username}
                    value={this.state.selectedValue}
                    renderOption={(props, option) => (
                        <Box component="li" sx={{ '& > img': { mb: 1.5, mr: 2, flexShrink: 0 } }} {...props}>
                            {option.username}
                        </Box>
                    )}
                    renderInput={(params) => (
                        <TextField
                            {...params}
                            label={this.state.receiverLabel}
                            error={this.state.receiverError}
                            inputProps={{
                                ...params.inputProps
                            }}
                        />
                    )}
                    onChange={(event, selectedValue) => this.handleChange(event, selectedValue)}
                    onBlur={() => this.handleBlur()}
                />
                <FormHelperText
                    error={this.state.receiverError}
                >
                    {this.state.receiverHelperText}
                </FormHelperText>
            </FormControl>
        )
    }
}