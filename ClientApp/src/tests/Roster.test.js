import { render, screen, waitFor, waitForElementToBeRemoved } from '@testing-library/react';
import user from '@testing-library/user-event';
import axios from 'axios';
import Roster from '../components/Roster';
import { url } from '../components/AppConstants';
import { players, rosterPlayers, newPlayers } from './TestData';

jest.mock('axios');

const rosterUrl = url + 'Rosters/1';
const playerUrl = url + 'Players';
const searchUrl = url + 'Players/Find?position=&name=';
const addUrl = url + 'Rosters/Players/Add/1/455';
const removeUrl = url + 'Rosters/Players/Remove/1/415';

test('renders player table rows', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case rosterUrl:
        return Promise.resolve({data: {players: rosterPlayers}});
      case playerUrl:
        return Promise.resolve({data: players});
      default:
        return Promise.reject(new Error('Axios Not Called'));
    }
  });
  render(<Roster />);
  await waitFor(()=> expect(axios.get).toHaveBeenCalledTimes(2));
  expect(screen.getByText(/Dalvin Cook/i)).toBeInTheDocument();
  expect(screen.getByText(/Patrick Mahomes/i)).toBeInTheDocument();
});

test('fetch data after search button clicked', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case searchUrl:
        return Promise.resolve({data: newPlayers});
      default:
        return Promise.reject(new Error('Axios Not Called'));
    }
  });
  render(<Roster />);
  const button = screen.getByRole('button', {name: 'search'});
  await user.click(button);
  expect(axios.get).toHaveBeenCalledTimes(2);
  expect(screen.getByText(/Lamar Jackson/i)).toBeInTheDocument();
});

test('handlePlayer buttons sends put requests', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case rosterUrl:
        return Promise.resolve({data: {players: rosterPlayers}});
      case playerUrl:
        return Promise.resolve({data: players});
      default:
        return Promise.reject(new Error('Axios Not Called'));
    }
  });
  axios.put.mockImplementation((url) => {
    switch(url) {
      case addUrl:
        return Promise.resolve('Player Added!');
      case removeUrl:
        return Promise.resolve('Player Removed!');
      default:
        return Promise.reject(new Error('Axios Not Called'));
    }
  });
  render(<Roster />);
  await waitFor(()=> expect(axios.get).toHaveBeenCalledTimes(2));
  const button = screen.getAllByRole('button', {name: 'player'});
  user.click(button[0]);
  user.click(button[1]);
  await waitForElementToBeRemoved(()=> screen.getByText(/Loading.../i));
  expect(axios.put).toHaveBeenCalledTimes(2);
});



