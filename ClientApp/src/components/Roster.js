// HandlePlayer function takes action parameter "Add" or "Remove" to add/remove player(id) from roster.
// FetchFantasyData & FetchFantasyDetails both open PlayerModal and retrieves fantasy points for selected week.

import React, { useState, useEffect } from 'react';
import axios from 'axios';
import NavBar from './NavBar';
import PlayerTable from './PlayerTable';
import PlayerForm from './PlayerForm';
import StatsForm from './StatsForm';
import PlayerModal from './PlayerModal';
import LoadingAlert from './LoadingAlert';
import Container from '@material-ui/core/Container';
import IconButton from '@material-ui/core/IconButton';
import BarChartIcon from '@material-ui/icons/BarChart';
import { url } from './AppConstants';

function Roster() {
  const [roster, setRoster] = useState(null);
  const [players, setPlayers] = useState([]);
  const [details, setDetails] = useState([]);
  const [week, setWeek] = useState(9);
  const [render, setRender] = useState(false);
  const [loading, setLoading] = useState(false);
  const [open, setOpen] = useState(false);

  useEffect(()=> {
    fetchData();
  }, [loading]);

  const fetchData = async () => {
    try {
      setRender(true);
      const response = await axios.get(url + 'Rosters/1');
      const responseP = await axios.get(url + 'Players');
      setRoster(response.data);
      setPlayers(responseP.data);
    }
    catch (error) {
      console.log(error);
    }
    setRender(false);
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

  const fetchFantasyData = async () => {
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

  const fetchFantasyDetails = async (id) => {
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
    <Container maxWidth='lg'>
      <NavBar />
      {loading && <LoadingAlert data='Players' />}
      {render && <LoadingAlert data='Roster' />}
      <IconButton 
        onClick={()=> fetchFantasyData()}
        color='primary'
        aria-label='roster'
      >
        <div className='button-update'>Update</div>
        <BarChartIcon />
      </IconButton>
      <PlayerForm 
        week={week}
        setPlayers={players => setPlayers(players)}
        setWeek={week => setWeek(week)}
        setLoading={loading => setLoading(loading)}
      />
      <StatsForm 
        setPlayers={players => setPlayers(players)}
        setLoading={loading => setLoading(loading)}
      />
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
          handleModal={id => fetchFantasyDetails(id)}
        />
      )}
      <PlayerTable 
        type='players'
        players={players} 
        handleClick={id => handlePlayer('Add', id)}
        handleModal={id => fetchFantasyDetails(id)}
      />
    </Container>
  );
}

export default Roster;
