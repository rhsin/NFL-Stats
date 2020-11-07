import React, { useState, useEffect } from 'react';
import axios from 'axios';
import NavBar from './NavBar';
import PlayerTable from './PlayerTable';
import PlayerForm from './PlayerForm';
import Container from '@material-ui/core/Container';
import { url } from './AppConstants';

function Roster() {
  const [roster, setRoster] = useState(null);
  const [players, setPlayers] = useState([]);
  const [loading, setLoading] = useState(false);

  useEffect(()=> {
    fetchData();
  }, [loading]);

  const fetchData = async () => {
    try {
      const response = await axios.get(url + 'Rosters/1');
      const responseP = await axios.get(url + 'Players');
      setRoster(response.data);
      setPlayers(responseP.data);
    }
    catch (error) {
      console.log(error);
    }
  };

  const handlePlayer = async (action, id) => {
    try {
      setLoading(true);
      const response = await axios.put(`${url}Rosters/Players/${action}/1/${id}`);
      console.log(response.data);
    }
    catch (error) {
      console.log(error);
    }
    finally {
      setLoading(false);
    }
  };

  return (
    <Container maxWidth='md'>
      <NavBar />
      {loading && <div>Loading...</div>}
      {roster && (
        <PlayerTable 
          type='roster'
          players={roster.players} 
          handleClick={id => handlePlayer('Remove', id)}
        />
      )}
      <PlayerForm 
        setPlayers={players => setPlayers(players)}
      />
      <PlayerTable 
        type='players'
        players={players} 
        handleClick={id => handlePlayer('Add', id)}
      />
    </Container>
  );
}

export default Roster;
