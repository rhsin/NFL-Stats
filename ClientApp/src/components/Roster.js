import React, { useState, useEffect } from 'react';
import axios from 'axios';
import NavBar from './NavBar';
import PlayerTable from './PlayerTable';
import PlayerForm from './PlayerForm';
import PlayerModal from './PlayerModal';
import Container from '@material-ui/core/Container';
import IconButton from '@material-ui/core/IconButton';
import BarChartIcon from '@material-ui/icons/BarChart';
import { url } from './AppConstants';

function Roster() {
  const [roster, setRoster] = useState(null);
  const [players, setPlayers] = useState([]);
  const [details, setDetails] = useState([]);
  const [loading, setLoading] = useState(false);
  const [open, setOpen] = useState(false);

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
      const response = await axios.put(
        `${url}Rosters/Players/${action}/1/${id}`);
      console.log(response.data);
    }
    catch (error) {
      console.log(error);
    }
    setLoading(false);
  };

  const fetchFantasyData = async (week = 8) => {
    try {
      setLoading(true);
      const response = await axios.get(
        `${url}Players/Fantasy/Rosters/1/${week}`);
      setDetails(response.data);
      setOpen(!open);
    }
    catch (error) {
      console.log(error);
    }
    setLoading(false);
  };

  const handleModal = async (id, week = 8) => {
    try {
      setLoading(true);
      const response = await axios.get(
        `${url}Players/Fantasy/${id}/${week}`);
      setDetails([response.data]);
      setOpen(!open);
    }
    catch (error) {
      console.log(error);
    }
    setLoading(false);
  };

  return (
    <Container maxWidth='md'>
      <NavBar />
      {loading && <div>Loading...</div>}
      <IconButton 
        onClick={()=> fetchFantasyData(8)}
        color='primary'
        aria-label='roster'
      >
        <div className='button-update'>Update</div>
        <BarChartIcon />
      </IconButton>
      <PlayerModal 
        open={open}
        players={details}
        setOpen={()=> setOpen(!open)}
      />
      {roster && (
        <PlayerTable 
          type='roster'
          players={roster.players} 
          handleClick={id => handlePlayer('Remove', id)}
          handleModal={id => handleModal(id, 8)}
        />
      )}
      <PlayerForm 
        setPlayers={players => setPlayers(players)}
      />
      <PlayerTable 
        type='players'
        players={players} 
        handleClick={id => handlePlayer('Add', id)}
        handleModal={id => handleModal(id, 8)}
      />
    </Container>
  );
}

export default Roster;
