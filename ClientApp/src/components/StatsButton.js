// This stats button group sets players from data fetched from .NET API endpoints.
// HandleClick retrieves players ordered by stat (TDs, TD/TO, YFS) calculated by .NET service class. 
// HandleSeason retrieves players from the selected season, ordered by points.

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
  const { players, setPlayers, setTable, setLoading, setRender } = props;

  const [season, setSeason] = useState(2019);

  const handleClick = async (type, table) => {
    try {
      setRender(true);
      const response = await axios.post(`${url}Stats/${type}`, players, {
          headers: { 'Content-Type': 'application/json' }
        }
      );
      setTable(table);
      setPlayers(response.data);
    }
    catch (error) {
      console.log(error);
    }
    setRender(false);
  };

  const handleSeason = async () => {
    try {
      const response = await axios.get(`${url}Players/Season/${season}`);
      setTable('Players');
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
          onClick={()=> handleSeason()}
          edge='start'
          color='primary'
          aria-label='season'
        >
          <DoubleArrowIcon />
        </IconButton>
      </span>
      <IconButton 
        onClick={()=> handleClick('Touchdowns', 'Total Touchdowns')}
        color='primary'
        aria-label='touchdowns'
      >
        <div className='button-text'>Total TDs</div>
        <BarChartIcon />
      </IconButton>
      <IconButton 
        onClick={()=> handleClick('Ratio', 'TD Ratio')}
        color='primary'
        aria-label='ratio'
      >
        <div className='button-text'>TD Ratio</div>
        <BarChartIcon />
      </IconButton>
      <IconButton 
        onClick={()=> handleClick('Scrimmage', 'Scrimmage Yards')}
        color='primary'
        aria-label='yards'
      >
        <div className='button-text'>Scrimmage Yds</div>
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