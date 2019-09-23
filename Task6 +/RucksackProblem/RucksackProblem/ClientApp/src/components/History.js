import React, { Component } from 'react';
import './History.css';
import { Glyphicon } from 'react-bootstrap';

export class History extends Component {

    constructor(props) {
        super(props);
        this.state = {
            rucksacks: [],
            loading: true,
            showInfoTask: false,
            infoTask: 0,
            loadingInfoTask: true,
            dataInfoTask: null,
        };

        fetch('api/History')
            .then(response => response.json())
            .then(data => {
                this.setState({ rucksacks: data, loading: false });
            });
    }
    getInfoOfTask = (id, event) => {
        event.preventDefault();
        this.setState({
            showInfoTask: true,
            infoTask: id
        });

        const path = 'api/History/' + id;

        fetch(path)
            .then(response => response.json())
            .then(data => {
                this.setState({ dataInfoTask: data, loadingInfoTask: false});
            });
    }
    back = () => {
        this.setState({
            showInfoTask: false,
            infoTask: 0,
            loadingInfoTask: true,
            dataInfoTask: null,
            loading: true
        });

        fetch('api/History')
            .then(response => response.json())
            .then(data => {
                this.setState({ rucksacks: data, loading: false });
            });
    }
    render() {
        const { loading, rucksacks, showInfoTask, loadingInfoTask } = this.state;

        if (showInfoTask) {
            if (loadingInfoTask) {
                return (
                    <div className="header">Loading...</div>
                );
            } else {
                const { dataInfoTask } = this.state;
                console.log(dataInfoTask);
                return (
                    <div>
                        <button className="button__back" onClick={this.back}>Back</button>
                        <table className="completed-tasks-table">
                            <thead className="completed-tasks-table__row">
                                <tr className="completed-tasks-table__row-head">
                                    <th className="completed-tasks-table__titel completed-tasks-table_border">Name</th>
                                    <th className="completed-tasks-table__titel completed-tasks-table_border-for-delete">Weight</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr className="completed-tasks-table__row">
                                    <th className="completed-tasks-table__cell completed-tasks-table_border">{dataInfoTask.name}</th>
                                    <th className="completed-tasks-table__cell completed-tasks-table_border-for-delete">{dataInfoTask.weight}</th>
                                </tr>
                            </tbody>
                        </table>
                        <table className="table-things">
                            <thead className="table-things__row">
                                <tr className="table-things__row">
                                    <th className="table-things__titel table-things_border">Name</th>
                                    <th className="table-things__titel table-things_border">Weight</th>
                                    <th className="table-things__titel table-things_border">Cost</th>
                                </tr>
                            </thead>
                            <tbody className="table-things__row">
                                {dataInfoTask.things.map((item, index) => (
                                    <tr key={index} className="table-things__row">
                                        <th className="table-things__cell table-things_border">
                                            {item.name}
                                        </th>
                                        <th className="table-things__cell table-things_border">
                                            {item.weight}
                                        </th>
                                        <th className="table-things__cell table-things_border">
                                            {item.cost}
                                        </th>
                                    </tr>
                                ))}
                            </tbody>
                        </table>
                        <div className="header">Solution: </div>
                        <div><span className="header-2">Max cost </span> {dataInfoTask.cost}</div>
                        <div><span className="header-2">Time work </span> {dataInfoTask.timeWork} sec</div>
                        <table className="table-things">
                            <thead className="table-things__row">
                                <tr className="table-things__row">
                                    <th className="table-things__titel table-things_border">Name</th>
                                    <th className="table-things__titel table-things_border">Weight</th>
                                    <th className="table-things__titel table-things_border">Cost</th>
                                </tr>
                            </thead>
                            <tbody className="table-things__row">
                                {
                                 dataInfoTask.things.filter(thing => thing.put === true).map((item, index) => (
                                    <tr key={index} className="table-things__row">
                                        <th className="table-things__cell table-things_border">
                                            {item.name}
                                        </th>
                                        <th className="table-things__cell table-things_border">
                                            {item.weight}
                                        </th>
                                        <th className="table-things__cell table-things_border">
                                            {item.cost}
                                        </th>
                                    </tr>
                                ))}
                            </tbody>

                        </table>
                    </div>
                    );
            }
        }
        if (loading) {
            return (
                <div className="header">Loading...</div>
            );
        } else {
            console.log(rucksacks);
            return (
                <div>
                    <div className="header">Completed tasks:</div>

                    <table className="completed-tasks-table">
                        <thead className="completed-tasks-table__row">
                            <tr className="completed-tasks-table__row-head">
                                <th className="completed-tasks-table__titel completed-tasks-table_border">Name</th>
                                <th className="completed-tasks-table__titel completed-tasks-table_border">Weight</th>
                                <th className="completed-tasks-table__titel completed-tasks-table_border">Cost</th>
                                <th className="completed-tasks-table__buttons completed-tasks-table_border-for-delete"></th>
                            </tr>
                        </thead>
                        <tbody>
                            {rucksacks.map((rucksack, index) => (
                                <tr key={rucksack.id} className="completed-tasks-table__row">
                                    <th className="completed-tasks-table__cell completed-tasks-table_border">{rucksack.name}</th>
                                    <th className="completed-tasks-table__cell completed-tasks-table_border">{rucksack.weight}</th>
                                    <th className="completed-tasks-table__cell completed-tasks-table_border">{rucksack.cost}</th>
                                    <th className="completed-tasks-table__buttons completed-tasks-table_border-for-delete">
                                        <button
                                            data-id={index}
                                            className="button__info-task"
                                            onClick={(e) => this.getInfoOfTask(rucksack.id, e)}>
                                            <Glyphicon glyph='info-sign' />
                                        </button>
                                    </th>
                                </tr>
                            ))}
                        </tbody>
                    </table>
                </div>
            );
        }
    }
}