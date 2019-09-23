import React, { Component } from 'react';
import { Glyphicon } from 'react-bootstrap';

export class RunningTasks extends Component {

    constructor(props) {
        super(props);
        this.state = {
            rucksacks: [],
            loading: true
        };

        fetch('api/RunningTasks')
            .then(response => response.json())
            .then(data => {
                this.setState({ rucksacks: data, loading: false });
            });
    }
    abort = (event) => {
        const id = event.currentTarget.dataset.id;
        console.log(id);
        fetch('api/RunningTasks/Abort', { method: 'POST', headers: { "Content-Type": "application/json" }, body: JSON.stringify(id) });
    }
    repeat = () => {
        this.setState({ loading: false });

        fetch('api/RunningTasks')
            .then(response => response.json())
            .then(data => {
                this.setState({ rucksacks: data, loading: false });
            });
    }
    render() {
        const { loading, rucksacks } = this.state;

        setTimeout(this.repeat, 5000);

        if (loading) {
            return (
                <div className="header">Loading...</div>
            );
        } else {
            console.log(rucksacks);
            return (
                <div>

                    <table className="completed-tasks-table">
                        <thead className="completed-tasks-table__row">
                            <tr className="completed-tasks-table__row-head">
                                <th className="completed-tasks-table__titel completed-tasks-table_border">Name</th>
                                <th className="completed-tasks-table__titel completed-tasks-table_border">Weight</th>
                                <th className="completed-tasks-table__buttons completed-tasks-table_border-for-delete"></th>
                            </tr>
                        </thead>
                        <tbody>
                            {rucksacks.map((rucksack, index) => (
                                <tr key={rucksack.id} className="completed-tasks-table__row">
                                    <th className="completed-tasks-table__cell completed-tasks-table_border">{rucksack.name}</th>
                                    <th className="completed-tasks-table__cell completed-tasks-table_border">{rucksack.weight}</th>
                                    <th className="completed-tasks-table__buttons completed-tasks-table_border-for-delete">
                                        <button
                                            data-id={rucksack.id}
                                            className="button__remove-row"
                                            onClick={this.abort}>
                                            <Glyphicon glyph='remove' />
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