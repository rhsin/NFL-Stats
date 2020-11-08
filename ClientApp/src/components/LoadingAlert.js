import React from 'react';
import Paper from '@material-ui/core/Paper';
import CircularProgress from '@material-ui/core/CircularProgress';

function LoadingAlert({ data }) {
  return (
    <Paper className='alert' elevation={2}>
      <CircularProgress
        size={17}
        thickness={2}
      />
      <span className='alert-text'>Loading {data}...</span>
    </Paper>
  );
}

export default LoadingAlert;