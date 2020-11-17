
export const url = 'https://localhost:44365/api/';

export const positions = ['QB', 'RB', 'WR', 'TE'];

export const weeks = [1, 2, 3, 4, 5, 6, 7, 8, 9];

export const seasons = [2019, 2018, 2017];

export const fields = ['Yards', 'Touchdowns', 'Turnovers'];

export const types = ['Passing', 'Rushing', 'Receiving', 'Interceptions', 'Fumbles'];

export const divisions = ['AFC East', 'AFC North', 'AFC South', 'AFC West', 'NFC East', 'NFC North', 'NFC South', 'NFC West'];

export const column = (table) => {
  switch (table) {
    case 'Total Touchdowns':
      return 'Total TDs';
    case 'TD Ratio':
      return 'TD/TO';
    case 'Scrimmage Yards':
      return 'YFS';
    default:
      return 'Points';
  }
};