// This stats button group sets players from data fetched from .NET API endpoints.
// HandleClick retrieves players ordered by TD/Turnover Ratio calculated by .NET service class.

import React from 'react';
import axios from 'axios';
import IconButton from '@material-ui/core/IconButton';
import BarChartIcon from '@material-ui/icons/BarChart';
import { url } from './AppConstants';

function StatsButton(props) {
  const { players, setPlayers, setTable } = props;

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

  return (
    <>
      <IconButton 
        onClick={()=> handleClick()}
        color='primary'
        aria-label='td-ratio'
      >
        <div className='button-stats'>TD Ratio</div>
        <BarChartIcon />
      </IconButton>
    </>
  );
}

export default StatsButton;