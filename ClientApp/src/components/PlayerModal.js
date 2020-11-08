import React from 'react';
import Modal from '@material-ui/core/Modal';
import Paper from '@material-ui/core/Paper';
import FantasyTable from './FantasyTable';

function PlayerModal(props) {
  const { open, players, setOpen } = props;
  
  return (
    <Modal
      open={open}
      onClose={()=> setOpen()}
      aria-labelledby='player-modal'
    >
      <Paper elevation={3}>
        <h3>PlayerModal</h3>
        <FantasyTable players={players} />
      </Paper>
    </Modal>
  );
}

export default PlayerModal;
