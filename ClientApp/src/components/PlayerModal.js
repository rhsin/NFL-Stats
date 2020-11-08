import React from 'react';
import Dialog from '@material-ui/core/Dialog';
import DialogActions from '@material-ui/core/DialogActions';
import DialogTitle from '@material-ui/core/DialogTitle';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import FantasyTable from './FantasyTable';

function PlayerModal(props) {
  const { open, players, setOpen } = props;
  
  return (
    <Dialog
      className='player-modal'
      fullWidth={true}
      maxWidth='md'
      open={open}
      onClose={()=> setOpen()}
      aria-labelledby='player-modal'
    >
      <DialogTitle>Fantasy Update</DialogTitle>
      <Paper className='card-modal' elevation={3}>
        <FantasyTable players={players} />
      </Paper>
      <DialogActions>
        <Button 
          onClick={()=> setOpen()}
          color="primary" 
          autoFocus
        >
          Close
        </Button>
      </DialogActions>
    </Dialog>
  );
}

export default PlayerModal;
