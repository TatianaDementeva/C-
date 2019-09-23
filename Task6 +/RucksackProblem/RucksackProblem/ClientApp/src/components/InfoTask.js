import React, { Component } from 'react';
import './InfoTask.css';

export class RunningTasks extends Component {

    constructor(props) {
        super(props);
        this.state = { rucksacks: [], loading: true };

        fetch('api/RunningTasks')
            .then(response => response.json())
            .then(data => {
                this.setState({ rucksacks: data, loading: false });
            });
    }
}