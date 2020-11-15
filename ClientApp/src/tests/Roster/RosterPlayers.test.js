import { render, screen, waitFor, waitForElementToBeRemoved } from '@testing-library/react';
import user from '@testing-library/user-event';
import axios from 'axios';
import Roster from '../../components/Roster';
import { url } from '../../components/AppConstants';
import { players, rosterPlayers, newPlayers, seasonPlayers } from '../TestData';

jest.mock('axios');

const rosterUrl = url + 'Rosters/1';
const playerUrl = url + 'Players';
const searchUrl = url + 'Players/Find?position=QB&name=Lamar';
const statsUrl = url + 'Players/Stats?field=Yards&type=Passing&value=4000';
const addUrl = url + 'Rosters/Players/Add/1/1';
const removeUrl = url + 'Rosters/Players/Remove/1/1';
const seasonUrl = url + 'Players/Season/2019';

test('renders Roster with player table rows', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case rosterUrl:
        return Promise.resolve({data: {players: rosterPlayers}});
      case playerUrl:
        return Promise.resolve({data: players});
      default:
        return Promise.reject(new Error('Axios Not Called: Render Roster'));
    }
  });
  render(<Roster />);

  await waitForElementToBeRemoved(()=> screen.getByText(/Loading Roster/i));

  expect(axios.get).toHaveBeenCalledTimes(2);
  expect(screen.getByText(/NFL Stats/i)).toBeInTheDocument();
  expect(screen.getByText(/Players/i)).toBeInTheDocument();
  expect(screen.getByText(/Dalvin Cook/i)).toBeInTheDocument();
  expect(screen.getByText(/Patrick Mahomes/i)).toBeInTheDocument();
});

test('fetch players on search button click', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case searchUrl:
        return Promise.resolve({data: newPlayers});
      default:
        return Promise.reject(new Error('Axios Not Called: Search Button'));
    }
  });
  render(<Roster />);

  user.type(screen.getByLabelText(/Search Player/i), 'Lamar');
  user.click(screen.getByRole('button', {name: 'search'}));
  await waitFor(()=> expect(axios.get).toHaveBeenCalledTimes(2));

  expect(screen.getByText(/Lamar Jackson/i)).toBeInTheDocument();
});

test('fetch players on stats form button click', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case statsUrl:
        return Promise.resolve({data: players});
      default:
        return Promise.reject(new Error('Axios Not Called: Stats Button'));
    }
  });
  render(<Roster />);

  user.clear(screen.getByLabelText(/Input Value/i));
  user.type(screen.getByLabelText(/Input Value/i), '4000');
  user.click(screen.getByRole('button', {name: 'stats'}));
  await waitFor(()=> expect(axios.get).toHaveBeenCalledTimes(2));

  expect(screen.getByText(/Patrick Mahomes/i)).toBeInTheDocument();
});

test('fetch players on season button click then reset', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case playerUrl:
        return Promise.resolve({data: players});
      case seasonUrl:
        return Promise.resolve({data: seasonPlayers});
      default:
        return Promise.reject(new Error('Axios Not Called: Season Button'));
    }
  });
  render(<Roster />);

  user.click(screen.getByRole('button', {name: 'season'}));
  await waitFor(()=> expect(axios.get).toHaveBeenCalledTimes(2));

  expect(screen.getByText(/Keenan Allen/i)).toBeInTheDocument();

  user.click(screen.getByRole('button', {name: 'reset'}));
  await waitFor(()=> expect(axios.get).toHaveBeenCalledTimes(3));

  expect(screen.getAllByText(/Players/i)).toHaveLength(2);
});

test('handlePlayer buttons sends put requests', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case rosterUrl:
        return Promise.resolve({data: {players: rosterPlayers}});
      case playerUrl:
        return Promise.resolve({data: players});
      default:
        return Promise.reject(new Error('Axios Not Called: HandlePlayer'));
    }
  });
  axios.put.mockImplementation((url) => {
    switch(url) {
      case addUrl:
        return Promise.resolve('Player Added!');
      case removeUrl:
        return Promise.resolve('Player Removed!');
      default:
        return Promise.reject(new Error('Axios Not Called: HandlePlayer Buttons'));
    }
  });
  render(<Roster />);

  await waitFor(()=> expect(axios.get).toHaveBeenCalledTimes(2));
  user.click(screen.getAllByRole('button', {name: 'player'})[0]);
  user.click(screen.getAllByRole('button', {name: 'player'})[1]);
  await waitForElementToBeRemoved(()=> screen.getAllByText(/Loading/i));
  
  expect(axios.put).toHaveBeenCalledTimes(2);
});



