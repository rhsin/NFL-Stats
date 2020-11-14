// HandleClick function adds or removes player from roster, based on type of table (roster vs players).
// HandleModal function opens PlayerModal and retrieves fantasy points for selected week.

import React from 'react';
import Accordion from '@material-ui/core/Accordion';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';
import Button from '@material-ui/core/Button';
import IconButton from '@material-ui/core/IconButton';
import AddBoxIcon from '@material-ui/icons/AddBox';
import RemoveCircleIcon from '@material-ui/icons/RemoveCircle';
import { column } from './AppConstants';

function PlayerTable(props) {
  const { table, players, handleClick, handleModal } = props;

  return (
    <Accordion defaultExpanded={true}>
      <AccordionSummary
        expandIcon={<ExpandMoreIcon />}
        aria-controls='panel-content'
        id='panel-header'
      >
        {table} 
      </AccordionSummary>
      <AccordionDetails>
        <Paper elevation={3}>
          <Table className='table-player' size='small'>
            <TableHead>
              <TableRow>
                <TableCell>Player</TableCell>
                <TableCell align='right'>Pos</TableCell>
                <TableCell align='right'>Team</TableCell>
                <TableCell align='right'>Season</TableCell>
                <TableCell align='right'>Pass Yds</TableCell>
                <TableCell align='right'>Pass TDs</TableCell>
                <TableCell align='right'>Int</TableCell>
                <TableCell align='right'>Rush Yds</TableCell>
                <TableCell align='right'>Rush TDs</TableCell>
                <TableCell align='right'>Rec Yds</TableCell>
                <TableCell align='right'>Rec TDs</TableCell>
                <TableCell align='right'>Fumbles</TableCell>
                <TableCell align='right'>{column(table)}</TableCell>
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
                  <TableCell align='right'>{player.season}</TableCell>
                  <TableCell align='right'>{player.passYds}</TableCell>
                  <TableCell align='right'>{player.passTds}</TableCell>
                  <TableCell align='right'>{player.passInt}</TableCell>
                  <TableCell align='right'>{player.rushYds}</TableCell>
                  <TableCell align='right'>{player.rushTds}</TableCell>
                  <TableCell align='right'>{player.recYds}</TableCell>
                  <TableCell align='right'>{player.recTds}</TableCell>
                  <TableCell align='right'>{player.fumbles}</TableCell>
                  <TableCell align='right'>{player.points.toFixed(2)}</TableCell>
                  <TableCell align='right'>
                    <IconButton 
                      onClick={()=> handleClick(player.id)}
                      edge='end'
                      color='primary'
                      aria-label='player'
                    >
                      {table === 'Players' ?
                        <AddBoxIcon /> : 
                        <RemoveCircleIcon />
                      }
                    </IconButton>
                  </TableCell>
                </TableRow>
              )}
            </TableBody>
          </Table>
        </Paper>
      </AccordionDetails>
    </Accordion>
  );
}

export default PlayerTable;