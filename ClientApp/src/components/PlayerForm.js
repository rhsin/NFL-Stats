// This form component sets players from data fetched from .NET API endpoints.
// HandleClick retrieve players containing matching substring for name & position.

import React, { useState } from 'react';
import axios from 'axios';
import TextField from '@material-ui/core/TextField';
import MenuItem from '@material-ui/core/MenuItem';
import IconButton from '@material-ui/core/IconButton';
import SearchIcon from '@material-ui/icons/Search';
import BarChartIcon from '@material-ui/icons/BarChart';
import { url, positions, weeks } from './AppConstants';

function PlayerForm(props) {
  const { week, fetchData, setPlayers, setWeek } = props;

  const [name, setName] = useState('');
  const [position, setPosition] = useState('QB');

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
      <span className='button-update'>
        <IconButton 
          onClick={()=> fetchData()}
          color='primary'
          aria-label='roster'
        >
          <div className='update-text'>Update</div>
          <BarChartIcon />
        </IconButton>
      </span>
      <span id='input-week'>
        <TextField
          id='select-week'
          select
          label='Week'
          value={week}
          onChange={e => setWeek(e.target.value)}
          variant='outlined'
          size='small'
        >
          {weeks.map(week => 
            <MenuItem key={week} value={week}>
              {week}
            </MenuItem>
          )}
        </TextField>
      </span>
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