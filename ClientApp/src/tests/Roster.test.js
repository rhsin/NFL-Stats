import { render, screen } from '@testing-library/react';
import user from '@testing-library/user-event';
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

test('fetch data twice after render', () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case apiUrl + 'Rosters/1':
        return Promise.resolve({data: {players: players}});
      case apiUrl + 'Players':
        return Promise.resolve({data: players});
      default:
        return Promise.reject(new Error('Not Found'));
    }
  });
  
  render(<Roster />);
  expect(axios.get).toHaveBeenCalledTimes(2);
});

test('fetch data after search button clicked', () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case apiUrl + 'Rosters/1':
        return Promise.resolve({data: {players: players}});
      case apiUrl + 'Players':
        return Promise.resolve({data: players});
      default:
        return Promise.reject(new Error('Not Found'));
    }
  });
  
  render(<Roster />);
  const button = screen.getByRole('button', {name: 'search'});
  user.click(button);
  expect(axios.get).toHaveBeenCalledTimes(3);
});

// test('renders player table rows', () => {
//   axios.get.mockImplementation((url) => {
//     switch(url) {
//       case apiUrl + 'Rosters/1':
//         return Promise.resolve({data: {players: players}});
//       case apiUrl + 'Players':
//         return Promise.resolve({data: players});
//       default:
//         return Promise.reject(new Error('Not Found'));
//     }
//   });
  
//   render(<Roster />);
//   const textElement = screen.getAllByText(/Patrick Mahomes/);
//   expect(textElement).toBeTruthy();
// });



