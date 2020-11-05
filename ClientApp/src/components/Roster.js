import React, { useState, useEffect } from 'react';
import axios from 'axios';
import PlayerTable from './PlayerTable';

function Roster() {
  const [roster, setRoster] = useState(null);
  const [players, setPlayers] = useState([]);
  const url = 'https://localhost:44365/api/';

  useEffect(()=> {
    axios.get(url + 'Rosters/1')
      .then(res => setRoster(res.data))
      .catch(err => console.log(err));
  }, []);

  useEffect(()=> {
    axios.get(url + 'Players')
      .then(res => setPlayers(res.data))
      .catch(err => console.log(err));
  }, []);

  return (
    <>
      <h3>NFL Stats</h3>
      {roster && <div>{roster.team}</div>}
      {roster && <PlayerTable players={roster.players} />}
      <br/>
      <PlayerTable players={players} />
    </>
  );
}

export default Roster;
