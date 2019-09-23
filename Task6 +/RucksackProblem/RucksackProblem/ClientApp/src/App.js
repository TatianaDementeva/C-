import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { FetchData } from './components/FetchData';
import { NewTask } from './components/NewTask';
import { RunningTasks } from './components/RunningTasks';
import { History } from './components/History';

export default class App extends Component {
  displayName = App.name

  render() {
    return (
        <Layout>
            <Route exact path='/' component={RunningTasks} />
            <Route path='/new' component={NewTask} />
            <Route path='/history' component={History} />
        </Layout>
    );
  }
}
