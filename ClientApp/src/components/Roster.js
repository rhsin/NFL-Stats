import React, { useState, useEffect } from 'react';
import axios from 'axios';

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
      {roster && roster.players.map(player =>
        <div key={player.id}>
          <div>{player.name}</div>
          <div>{player.position}</div>
          <div>{player.team}</div>
          <div>{player.points}</div>
        </div>
      )}
      <br></br>
      {players.map(player =>
        <div key={player.id}>
          <div>{player.name}</div>
          <div>{player.position}</div>
          <div>{player.team}</div>
          <div>{player.points}</div>
        </div>
      )}
    </>
  );
}

export default Roster;
