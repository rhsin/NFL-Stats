// This stats button group sets players from data fetched from .NET API endpoints.
// HandleClick retrieves players ordered by TD/Turnover Ratio calculated by .NET service class.

import React, { useState } from 'react';
import axios from 'axios';
import TextField from '@material-ui/core/TextField';
import MenuItem from '@material-ui/core/MenuItem';
import IconButton from '@material-ui/core/IconButton';
import DoubleArrowIcon from '@material-ui/icons/DoubleArrow';
import BarChartIcon from '@material-ui/icons/BarChart';
import RefreshIcon from '@material-ui/icons/Refresh';
import { url, seasons } from './AppConstants';

function StatsButton(props) {
  const { players, setPlayers, setTable, setLoading } = props;

  const [season, setSeason] = useState(2019);

  const handleClick = async () => {
    try {
      const response = await axios.post(
        `${url}Stats/Ratio/Passing`, players, {
          headers: { 'Content-Type': 'application/json' }
        }
      );
      setTable('TD Ratio');
      setPlayers(response.data);
    }
    catch (error) {
      console.log(error);
    }
  };

  const handleSelect = async () => {
    try {
      const response = await axios.get(`${url}Players/Season/${season}`);
      setPlayers(response.data);
    }
    catch (error) {
      console.log(error);
    }
  };

  const handleReset = () => {
    setLoading(true);
    setTable('Players');
    setTimeout(()=> setLoading(false), 3000);
  };

  return (
    <>
      <span id='input-season'>
        <TextField
          id='select-season'
          select
          label='Season'
          value={season}
          onChange={e => setSeason(e.target.value)}
          variant='outlined'
          size='small'
        >
          {seasons.map(season => 
            <MenuItem key={season} value={season}>
              {season}
            </MenuItem>
          )}
        </TextField>
      </span>
      <span className='button-season'>
        <IconButton 
          onClick={()=> handleSelect()}
          edge='start'
          color='primary'
          aria-label='season'
        >
          <DoubleArrowIcon />
        </IconButton>
      </span>
      <IconButton 
        onClick={()=> handleClick()}
        color='primary'
        aria-label='ratio'
      >
        <div className='button-text'>TD Ratio</div>
        <BarChartIcon />
      </IconButton>
      <IconButton 
        onClick={()=> handleReset()}
        color='primary'
        aria-label='reset'
      >
        <div className='button-text'>Reset</div>
        <RefreshIcon />
      </IconButton>
    </>
  );
}

export default StatsButton;