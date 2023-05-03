import React, { useEffect } from 'react';
import logo from './logo.svg';
import './App.css';
import { MyMessage } from './proto/chat_pb'
import { MessagingClient } from './proto/ChatServiceClientPb'

function App() {

    useEffect(() => {
        const client = new MessagingClient("https://localhost:7270")
        const req = new MyMessage()
        req.setName("Bulat")
        req.setMessage("Hello")
        cli
    }

    )

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.tsx</code> and save to reload.
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
