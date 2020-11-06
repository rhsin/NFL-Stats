import { render, screen } from '@testing-library/react';
import axios from 'axios';
import Roster from '../components/Roster';
import { url as apiUrl } from '../components/AppContants';

jest.mock('axios');

const players = [
  {
    id: 455,
    name: 'Patrick Mahomes',
    position: 'QB',
    team: 'KAN',
    points: 285.04
  }
];

test('fetch players', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case apiUrl + 'Rosters/1':
        return Promise.resolve({players: players});
      case apiUrl + 'Players':
        return Promise.resolve(players);
      default:
        return Promise.reject(new Error('Not Found'));
    }
  });

  const res = await axios.get(apiUrl + 'Players');
  expect(res).toEqual(players);
});

// test('renders player table rows', () => {
//   axios.get.mockImplementation((url) => {
//     switch(url) {
//       case apiUrl + 'Rosters/1':
//         return Promise.resolve({players: players});
//       case apiUrl + 'Players':
//         return Promise.resolve(players);
//       default:
//         return Promise.reject(new Error('Not Found'));
//     }
//   });
  
//   render(<Roster />);
//   const textElement = screen.getByText(/Patrick Mahomes/i);
//   expect(textElement).toBeInTheDocument();
// });

