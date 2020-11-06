import { render, screen, waitFor } from '@testing-library/react';
import user from '@testing-library/user-event';
import axios from 'axios';
import Roster from '../components/Roster';
import { url } from '../components/AppContants';

jest.mock('axios');

const rosterUrl = url + 'Rosters/1';
const playerUrl = url + 'Players'

const players = [
  {
    id: 455,
    name: 'Patrick Mahomes',
    position: 'QB',
    team: 'KAN',
    points: 285.04
  }
];

const newPlayers = [
  {
    id: 408,
    name: 'Lamar Jackson',
    position: 'QB',
    team: 'BAL',
    points: 415.68
  }
];

test('renders player table rows', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case rosterUrl:
        return Promise.resolve({data: {players: players}});
      case playerUrl:
        return Promise.resolve({data: players});
      default:
        return Promise.reject(new Error('Axios Not Called'));
    }
  });
  render(<Roster />);
  await waitFor(()=> expect(axios.get).toHaveBeenCalledTimes(2));
  const textElements = screen.getAllByText(/Patrick Mahomes/);
  expect(textElements).toBeTruthy();
});

test('fetch data after search button clicked', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case rosterUrl:
        return Promise.resolve({data: {players: newPlayers}});
      case playerUrl:
        return Promise.resolve({data: players});
      default:
        return Promise.reject(new Error('Axios Not Called'));
    }
  });
  render(<Roster />);
  const button = screen.getByRole('button', {name: 'search'});
  user.click(button);
  await waitFor(()=> expect(axios.get).toHaveBeenCalledTimes(3));
  const textElements = screen.getAllByText(/Lamar Jackson/);
  expect(textElements).toBeTruthy();
});



