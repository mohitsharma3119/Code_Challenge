import React, { Component } from 'react';
import { DropzoneArea } from 'material-ui-dropzone';
import Typography from '@material-ui/core/Typography';
import Button from '@material-ui/core/Button';
import axios from 'axios';
import { Redirect } from "react-router-dom";

export default class Home extends Component {
    constructor(props) {
        super(props);
        this.state = {
            files: [],            
            redirect: null
        };
        this.onFormSubmit = this.onFormSubmit.bind(this)
        this.fileUpload = this.fileUpload.bind(this)
    }
    handleChange(files) {
        this.setState({
            files: files
        });
    }

    onFormSubmit(e) {
        e.preventDefault();
        this.fileUpload(this.state.files);
    }

    fileUpload(file) {
        console.log(file);
        console.log(file[0].path + '  ' + file[0].name);
        const formData = new FormData();
        formData.append('file',file[0]);
        axios({
            url: "https://localhost:44380/api/Dealerships/UploadCsv",
            method: 'POST',
            data: formData,
            headers: {
                'Content-Type': 'multipart/form-data',
            }
        })
        .then((response) => {
                console.log("response :", response);
                this.setState({ redirect: "/data" });
            })
        .catch((error) => {
                console.log("error in uploading file :", error);
                alert('error occured please try again');
            })
    }

    render() {
        if (this.state.redirect) {
            return <Redirect to={this.state.redirect} />
          }
        return (
            <div>
                <form onSubmit={this.onFormSubmit}>
                    <Typography component="h3" variant="h3" align="center" color="textPrimary" gutterBottom>
                        Upload CSV
                    </Typography>
                    <DropzoneArea
                        onChange={this.handleChange.bind(this)}
                    />
                    <Button type="submit" variant="contained" color="primary" onClick={this.hdndleUpload} >
                        Upload
                    </Button>
                </form>
            </div>
        )
    }
}
