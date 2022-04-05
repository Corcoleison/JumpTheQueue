import logo from './logo.svg';
import './App.css';
import { useEffect } from 'react';
import {LogLevel, HubConnectionBuilder, HttpTransportType } from '@microsoft/signalr';
import styled from "styled-components";

const Button = styled.button`
  background-color: red;
  color: white;
  padding: 5px 15px;
  border-radius: 5px;
  outline: 0;
  text-transform: uppercase;
  margin: 10px 0px;
  cursor: pointer;
  box-shadow: 0px 2px 2px lightgray;
  transition: ease background-color 250ms;
  &:hover {
    background-color: blue;
  }
  &:disabled {
    cursor: default;
    opacity: 0.7;
  }
`;

Button.defaultProps = {
  theme: "blue"
};

function clickMe() {
  const connection = new HubConnectionBuilder()
  .withUrl("https://localhost:8085/colahub")
  .build();
  connection.on("receiveNext", next => console.log("next attended is:", next))
  connection.on("startQueue", queue => console.log(queue, "has started"))
  connection.start().then(() => connection.invoke("AddToGroup", "queue1"))
  alert("You have subscribe for queue1");
}

function App() {

  

  return (
    <div className="App">
      <div>
        <Button onClick={clickMe}>Button</Button>
      </div>
    </div>
  );
}

export default App;
