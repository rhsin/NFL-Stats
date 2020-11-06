import React, { useState, useEffect } from 'react';
import axios from 'axios';
import NavBar from './NavBar';
import PlayerTable from './PlayerTable';
import PlayerForm from './PlayerForm';
import Container from '@material-ui/core/Container';
import { url } from './AppContants';

function Roster() {
  const [roster, setRoster] = useState(null);
  const [players, setPlayers] = useState([]);
  const [loading, setLoading] = useState(false);

  useEffect(()=> {
    axios.get(url + 'Rosters/1')
      .then(res => setRoster(res.data))
      .catch(err => console.log(err));
    axios.get(url + 'Players')
      .then(res => setPlayers(res.data))
      .catch(err => console.log(err));
  }, [loading]);

  const addPlayer = (id) => {
    axios.put(url + 'Rosters/Players/Add/1/' + id)
      .then(res => console.log(res.data))
      .catch(err => console.log(err));
    setLoading(!loading);
  };

  const removePlayer = (id) => {
    axios.put(url + 'Rosters/Players/Remove/1/' + id)
      .then(res => console.log(res.data))
      .catch(err => console.log(err));
    setLoading(!loading);
  };

  return (
    <Container maxWidth='md'>
      <NavBar />
      {roster && (
        <PlayerTable 
          type='roster'
          players={roster.players} 
          handleClick={id => removePlayer(id)}
        />
      )}
      <PlayerForm 
        setPlayers={players => setPlayers(players)}
      />
      <PlayerTable 
        type='players'
        players={players} 
        handleClick={id => addPlayer(id)}
      />
    </Container>
  );
}

export default Roster;
