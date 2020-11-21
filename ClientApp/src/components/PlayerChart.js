import React from 'react';
import ReactFrappeChart from 'react-frappe-charts';

function PlayerChart({ players }) {
  return (
    <ReactFrappeChart
      type='bar'
      colors={['#21ba45']}
      axisOptions={{ xAxisMode: 'tick', yAxisMode: 'tick', xIsSeries: 1 }}
      height={250}
      data={{
        labels: players.map(player => player.name),
        datasets: [{ values: players.map(player => player.points) }],
      }}
    />
  );
}

export default PlayerChart;