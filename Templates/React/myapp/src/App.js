import logo from './logo.svg';
import './App.css';
import { useEffect } from 'react';
import {LogLevel, HubConnectionBuilder, HttpTransportType } from '@microsoft/signalr'

function App() {

  useEffect(()=>{
    const connection = new HubConnectionBuilder()
    .withUrl("https://localhost:8085/colahub")
    .build();
    connection.on("receiveNext", next => console.log("next attended is:", next))
    connection.start().then(() => connection.invoke("AddToGroup", "queue1"))
  },[])

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
