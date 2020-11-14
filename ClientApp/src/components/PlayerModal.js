// HandleClick checks if players in roster make a standard fantasy team lineup, 
// and returns total fantasy points if valid.

import React, { useState } from 'react';
import axios from 'axios';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogTitle from '@material-ui/core/DialogTitle';
import Button from '@material-ui/core/Button';
import FantasyTable from './FantasyTable';
import { url } from './AppConstants';

function PlayerModal(props) {
  const { open, players, setOpen } = props;

  const [total, setTotal] = useState(null);

  const handleClick = async () => {
    try {
      const response = await axios.post(`${url}Rosters/Fantasy`, players, {
          headers: { 'Content-Type': 'application/json' }
        }
      );
      setTotal(response.data);
    }
    catch (error) {
      setTotal('Invalid Lineup!');
      console.log(error);
    }
  };
  
  return (
    <Dialog
      className='modal-player'
      fullWidth={true}
      maxWidth='md'
      open={open}
      onClose={()=> setOpen()}
      aria-labelledby='modal-player'
    >
      <DialogTitle>Fantasy Update</DialogTitle>
      <div className='card-modal'>
        <FantasyTable players={players} />
        <div className='modal-text'>
          Total: <span id='total-text'>{total}</span>
        </div>
      </div>
      <DialogActions>
        <Button 
          onClick={()=> handleClick()}
          color='primary' 
        >
          Submit
        </Button>
        <Button 
          onClick={()=> setOpen()}
          color='primary' 
          autoFocus
        >
          Close
        </Button>
      </DialogActions>
    </Dialog>
  );
}

export default PlayerModal;
