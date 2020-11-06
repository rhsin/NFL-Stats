import React, { useState } from 'react';
import axios from 'axios';
import Paper from '@material-ui/core/Paper';
import TextField from '@material-ui/core/TextField';
import MenuItem from '@material-ui/core/MenuItem';
import IconButton from '@material-ui/core/IconButton';
import SearchIcon from '@material-ui/icons/Search';
import { url, positions } from './AppConstants';

function PlayerForm(props) {
  const { setPlayers, setLoading } = props;

  const [name, setName] = useState('');
  const [position, setPosition] = useState('');

  const handleClick = async () => {
    try {
      setLoading(true);
      const response = await axios.get(
        url + `Players/Find?position=${position}&name=${name}`);
      setPlayers(response.data);
    }
    catch (error) {
      console.log(error);
    }
    finally {
      setLoading(false);
    }
  };

  return (
    <Paper elevation={3}>
      <TextField 
        id='search-player' 
        label='Search Player' 
        type='search' 
        onChange={e => setName(e.target.value)}
        variant='outlined' 
      />
      <TextField
        id='select-position'
        select
        label='Position'
        value={position}
        onChange={e => setPosition(e.target.value)}
        helperText='Select position'
        variant='outlined'
      >
        <MenuItem value=''>
          All
        </MenuItem>
        {positions.map((position, index) => 
          <MenuItem key={index} value={position}>
            {position}
          </MenuItem>
        )}
      </TextField>
      <IconButton 
        onClick={()=> handleClick()}
        edge='end'
        color='primary'
        aria-label='search'
      >
        <SearchIcon />
      </IconButton>
    </Paper>
  );
}

export default PlayerForm;