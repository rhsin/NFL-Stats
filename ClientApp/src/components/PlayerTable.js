// HandleClick function adds or removes player from roster, based on type of table (roster vs players).
// HandleModal function opens PlayerModal and retrieves fantasy points for selected week.

import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableContainer from '@material-ui/core/TableContainer';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import AddBoxIcon from '@material-ui/icons/AddBox';
import RemoveCircleIcon from '@material-ui/icons/RemoveCircle';

function PlayerTable(props) {
  const { type, players, handleClick, handleModal } = props;

  return (
    <TableContainer component={Paper}>
      <Table className='table-player' size='small'>
        <TableHead>
          <TableRow>
            <TableCell>Player</TableCell>
            <TableCell align='right'>Pos</TableCell>
            <TableCell align='right'>Team</TableCell>
            <TableCell align='right'>Points</TableCell>
            <TableCell align='right'>Pass Yds</TableCell>
            <TableCell align='right'>Pass TDs</TableCell>
            <TableCell align='right'>Int</TableCell>
            <TableCell align='right'>Rush Yds</TableCell>
            <TableCell align='right'>Rush TDs</TableCell>
            <TableCell align='right'>Rec Yds</TableCell>
            <TableCell align='right'>Rec TDs</TableCell>
            <TableCell align='right'>Fumbles</TableCell>
            <TableCell align='right'>Roster</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {players.map(player =>
            <TableRow key={player.id}>
              <TableCell component='th' scope='row'>
                <Button 
                  onClick={()=> handleModal(player.id)}
                  color='primary'
                  aria-label='modal'
                >
                  <div className='button-player'>
                    {player.name}
                  </div>
                </Button>
              </TableCell>
              <TableCell align='right'>{player.position}</TableCell>
              <TableCell align='right'>{player.team}</TableCell>
              <TableCell align='right'>{player.points}</TableCell>
              <TableCell align='right'>{player.passYds}</TableCell>
              <TableCell align='right'>{player.passTds}</TableCell>
              <TableCell align='right'>{player.passInt}</TableCell>
              <TableCell align='right'>{player.rushYds}</TableCell>
              <TableCell align='right'>{player.rushTds}</TableCell>
              <TableCell align='right'>{player.recYds}</TableCell>
              <TableCell align='right'>{player.recTds}</TableCell>
              <TableCell align='right'>{player.fumbles}</TableCell>
              <TableCell align='right'>
                <IconButton 
                  onClick={()=> handleClick(player.id)}
                  edge='end'
                  color='primary'
                  aria-label='player'
                >
                  {type === 'players' ?
                    <AddBoxIcon /> : 
                    <RemoveCircleIcon />
                  }
                </IconButton>
              </TableCell>
            </TableRow>
          )}
        </TableBody>
      </Table>
    </TableContainer>
  );
}

export default PlayerTable;