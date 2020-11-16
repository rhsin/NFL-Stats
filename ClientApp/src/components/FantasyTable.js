import React from 'react';
import Table from '@material-ui/core/Table';
import TableBody from '@material-ui/core/TableBody';
import TableCell from '@material-ui/core/TableCell';
import TableHead from '@material-ui/core/TableHead';
import TableRow from '@material-ui/core/TableRow';
import Paper from '@material-ui/core/Paper';

function FantasyTable({ players }) {
  return (
    <Paper elevation={3}>
      <Table className='table-player'>
        <TableHead>
          <TableRow>
            <TableCell>Player</TableCell>
            <TableCell align='right'>Position</TableCell>
            <TableCell align='right'>Team</TableCell>
            <TableCell align='right'>Points</TableCell>
          </TableRow>
        </TableHead>
        <TableBody>
          {players.map(player =>
            <TableRow key={player.id}>
              <TableCell component='th' scope='row'>
                {player.name}
              </TableCell>
              <TableCell align='right'>{player.position}</TableCell>
              <TableCell align='right'>{player.teamName}</TableCell>
              <TableCell align='right'>{player.points}</TableCell>
            </TableRow>
          )}
        </TableBody>
      </Table>
    </Paper>
  );
}

export default FantasyTable;