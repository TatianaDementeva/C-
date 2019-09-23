import React, { Component } from 'react';
import './NewTask.css';
import { Glyphicon } from 'react-bootstrap';

export class NewTask extends Component {

    constructor(props) {
        super(props);
        this.state = {
            nameTask: '',
            weightTask: '',
            rows: [{ name: '', weight: 0, cost: 0 }] 
        };
        
    }

    incrementRow = () => {
        const { rows } = this.state;
        this.setState({
            rows: rows.concat({ name: '', weight: 0, cost: 0 })
        });
    }
    removeRow = (event) => {
        const { rows } = this.state;

        const id = event.currentTarget.dataset.id;
        rows.splice(id, 1);

        this.setState({
            rows: rows
        });
    }
    handleInputChange = (event) => {
        const target = event.target;
        const value = target.value;
        const name = target.name;
        const id = Number(target.id);

        if (name === "name") {
            this.setState({
                rows: this.state.rows.map((item, index) => {    
                    if (index !== id) return item;
                    return {
                        ...item,
                        name: value,
                    }
                })
            });
        }
        if (name === "weight") {
            this.setState({
                rows: this.state.rows.map((item, index) => {
                    if (index !== id) return item;
                    return {
                        ...item,
                        weight: value
                    }
                })
            });
        }
        if (name === "cost") {
            this.setState({
                rows: this.state.rows.map((item, index) => {
                    if (index !== id) return item;
                    return {
                        ...item,
                        cost: Number(value)
                    }
                })
            });
        }else {
            this.setState({
                [name]: value
            });
        }
    }
    onSubmit = (event) => {
        event.preventDefault();
        console.log("onSubmit");
        const { nameTask, weightTask, rows } = this.state;

        const data = { "nameRucksack": nameTask, "weightRucksack": weightTask, "things": rows };
        fetch('api/NewTask/NewRucksack', { method: 'POST', headers: { "Content-Type": "application/json" }, body: JSON.stringify(data) });

        this.setState(
            {
                nameTask: '',
                weightTask: '',
                rows: [{ name: '', weight: 0, cost: 0 }] 
            })   
    }
    render()
    {
        const { rows, nameTask, weightTask } = this.state;
        console.log("render");
        return (
            <form className="new-task" onSubmit={this.onSubmit}>
                <input
                    required
                    className="new-task__field"
                    type="text"
                    name="nameTask"
                    placeholder="Name task"
                    onChange={this.handleInputChange}
                    value={nameTask}
                />
                <input
                    required
                    className="new-task__field"
                    type="number"
                    min="0"
                    name="weightTask"
                    placeholder="Weight rucksack"
                    onChange={this.handleInputChange}
                    value={weightTask}
                />

                <div className="table-name">Add things:</div>
                <table className="table-things">
                    <thead className="table-things__row">
                        <tr className="table-things__row">
                            <th className="table-things__titel table-things_border">Name</th>
                            <th className="table-things__titel table-things_border">Weight</th>
                            <th className="table-things__titel table-things_border">Cost</th>
                            <th className="table-things__delete table-things_border"></th>
                        </tr>
                    </thead>
                    <tbody className="table-things__row">
                        {rows.map((item, index) => (
                            <tr key={index} className="table-things__row">
                                <th className="table-things__cell table-things_border">
                                    <input
                                        required
                                        id={index}
                                        name="name"
                                        className="table-things__imput"
                                        type="text"
                                        min="0"
                                        value={item.name}
                                        onChange={this.handleInputChange}
                                    />
                                </th>
                                <th className="table-things__cell table-things_border">
                                    <input
                                        required
                                        id={index}
                                        name="weight"
                                        className="table-things__imput"
                                        type="number"
                                        min="0"
                                        value={item.weight}
                                        onChange={this.handleInputChange}
                                    />
                                </th>
                                <th className="table-things__cell table-things_border">
                                    <input
                                        required
                                        id={index}
                                        name="cost"
                                        className="table-things__imput"
                                        type="number"
                                        min="0"
                                        value={item.cost}
                                        onChange={this.handleInputChange}
                                    />
                                </th>
                                <th className="table-things_border">
                                    <button
                                        data-id={index}
                                        className="button__remove-row"
                                        onClick={this.removeRow}>
                                        <Glyphicon glyph='remove' />
                                    </button>
                                </th>
                            </tr>
                        ))}
                    </tbody>
                  
                </table>
                <button className="button__add-row"onClick={this.incrementRow}>Add row</button>

          <input className="new-task__button" type="submit" value="Create" />
        </form>
      );
    }
}