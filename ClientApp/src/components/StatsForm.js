// This stats form component sets players from data fetched from .NET API endpoints.
// HandleClick retrieve players using Field, Type & Value in SQL query to find 
// players filtered by pass/rush/rec stats. 

import React, { useState } from 'react';
import axios from 'axios';
import TextField from '@material-ui/core/TextField';
import MenuItem from '@material-ui/core/MenuItem';
import IconButton from '@material-ui/core/IconButton';
import DoubleArrowIcon from '@material-ui/icons/DoubleArrow';
import { url, fields, types } from './AppConstants';

function StatsForm({ setPlayers }) {
  const [field, setField] = useState('Yards');
  const [type, setType] = useState('Passing');
  const [value, setValue] = useState(3000);

  const handleClick = async () => {
    try {
      const response = await axios.get(
        `${url}Players/Stats?field=${field}&type=${type}&value=${value}`);
      setPlayers(response.data);
    }
    catch (error) {
      console.log(error);
    }
  };

  return (
    <>
      <TextField
        id='select-field'
        select
        label='Field'
        value={field}
        onChange={e => setField(e.target.value)}
        variant='outlined'
        size='small'
      >
        {fields.map((field, index) => 
          <MenuItem key={index} value={field}>
            {field}
          </MenuItem>
        )}
      </TextField>
      <TextField
        id='select-type'
        select
        label='Type'
        value={type}
        onChange={e => setType(e.target.value)}
        variant='outlined'
        size='small'
      >
        {types.map((type, index) => 
          <MenuItem key={index} value={type}>
            {type}
          </MenuItem>
        )}
      </TextField>
      <TextField 
        id='input-value' 
        label='Input Value' 
        type='number' 
        onChange={e => setValue(e.target.value)}
        defaultValue={value}
        variant='outlined' 
        size='small'
      />
      <IconButton 
        onClick={()=> handleClick()}
        edge='end'
        color='primary'
        aria-label='stats'
      >
        <DoubleArrowIcon />
      </IconButton>
    </>
  );
}

export default StatsForm;