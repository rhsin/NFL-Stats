import React, { useState } from 'react';
import axios from 'axios';
import TextField from '@material-ui/core/TextField';
import MenuItem from '@material-ui/core/MenuItem';
import IconButton from '@material-ui/core/IconButton';
import SearchIcon from '@material-ui/icons/Search';
import { url, positions } from './AppConstants';

function PlayerForm(props) {
  const { setPlayers } = props;

  const [name, setName] = useState('');
  const [position, setPosition] = useState('');

  const handleClick = async () => {
    try {
      const response = await axios.get(
        `${url}Players/Find?position=${position}&name=${name}`);
      setPlayers(response.data);
    }
    catch (error) {
      console.log(error);
    }
  };

  return (
    <>
      <TextField 
        id='search-player' 
        label='Search Player' 
        type='search' 
        onChange={e => setName(e.target.value)}
        variant='outlined' 
        size='small'
      />
      <TextField
        id='select-position'
        select
        label='Position'
        value={position}
        onChange={e => setPosition(e.target.value)}
        variant='outlined'
        size='small'
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
    </>
  );
}

export default PlayerForm;