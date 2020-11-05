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

  useEffect(()=> {
    axios.get(url + 'Rosters/1')
      .then(res => setRoster(res.data))
      .catch(err => console.log(err));
    axios.get(url + 'Players')
      .then(res => setPlayers(res.data))
      .catch(err => console.log(err));
  }, []);

  return (
    <Container maxWidth='md'>
      <NavBar />
      {roster && <PlayerTable players={roster.players} />}
      <PlayerForm 
        setPlayers={players => setPlayers(players)}
      />
      <PlayerTable players={players} />
    </Container>
  );
}

export default Roster;
