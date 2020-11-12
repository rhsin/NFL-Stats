import { render, screen, waitFor, waitForElementToBeRemoved } from '@testing-library/react';
import user from '@testing-library/user-event';
import axios from 'axios';
import Roster from '../components/Roster';
import { url } from '../components/AppConstants';
import { players, rosterPlayers, newPlayers, fantasyPlayers, fantasyPlayer } from './TestData';

jest.mock('axios');

const rosterUrl = url + 'Rosters/1';
const playerUrl = url + 'Players';
const searchUrl = url + 'Players/Find?position=QB&name=';
const statsUrl = url + 'Players/Stats?field=Yards&type=Passing&value=3000';
const addUrl = url + 'Rosters/Players/Add/1/455';
const removeUrl = url + 'Rosters/Players/Remove/1/415';
const fantasyUrl = url + 'Stats/Fantasy/Rosters/1/9';
const detailUrl = url + 'Stats/Fantasy/415/9';
const checkUrl = url + 'Rosters/Fantasy';

test('renders player table rows', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case rosterUrl:
        return Promise.resolve({data: {players: rosterPlayers}});
      case playerUrl:
        return Promise.resolve({data: players});
      default:
        return Promise.reject(new Error('Axios Not Called 1'));
    }
  });
  render(<Roster />);
  await waitForElementToBeRemoved(()=> screen.getByText(/Loading Roster/i));
  expect(axios.get).toHaveBeenCalledTimes(2);
  expect(screen.getByText(/Dalvin Cook/i)).toBeInTheDocument();
  expect(screen.getByText(/Patrick Mahomes/i)).toBeInTheDocument();
});

test('fetch data after search button clicked', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case searchUrl:
        return Promise.resolve({data: newPlayers});
      default:
        return Promise.reject(new Error('Axios Not Called 2A'));
    }
  });
  render(<Roster />);
  const button = screen.getByRole('button', {name: 'search'});
  await user.click(button);
  await waitFor(()=> expect(axios.get).toHaveBeenCalledTimes(2));
  expect(screen.getByText(/Lamar Jackson/i)).toBeInTheDocument();
});

test('fetch data after stats button clicked', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case statsUrl:
        return Promise.resolve({data: players});
      default:
        return Promise.reject(new Error('Axios Not Called 2B'));
    }
  });
  render(<Roster />);
  const button = screen.getByRole('button', {name: 'stats'});
  await user.click(button);
  await waitFor(()=> expect(axios.get).toHaveBeenCalledTimes(2));
  expect(screen.getByText(/Patrick Mahomes/i)).toBeInTheDocument();
});

test('fetch fantasy roster on update button clicked', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case fantasyUrl:
        return Promise.resolve({data: fantasyPlayers});
      default:
        return Promise.reject(new Error('Axios Not Called 3A'));
    }
  });
  render(<Roster />);
  const button = screen.getByRole('button', {name: 'roster'});
  user.click(button);
  await waitForElementToBeRemoved(()=> screen.getAllByText(/Loading/i));
  expect(axios.get).toHaveBeenCalledTimes(4);
  expect(screen.getByText(/Travis Kelce/i)).toBeInTheDocument();
});

test('fetch fantasy roster total on submit button clicked', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case fantasyUrl:
        return Promise.resolve({data: fantasyPlayers});
      default:
        return Promise.reject(new Error('Axios Not Called 3B'));
    }
  });
  axios.post.mockImplementation((url) => {
    switch(url) {
      case checkUrl:
        return Promise.resolve({data: 15});
      default:
        return Promise.reject(new Error('Axios Not Called 3C'));
    }
  });
  render(<Roster />);
  const button = screen.getByRole('button', {name: 'roster'});
  user.click(button);
  await waitForElementToBeRemoved(()=> screen.getAllByText(/Loading/i));
  const submitButton = screen.getByRole('button', {name: 'Submit'});
  await user.click(submitButton);
  await waitFor(()=> expect(axios.post).toHaveBeenCalledTimes(1));
  expect(screen.getByText(/Travis Kelce/i)).toBeInTheDocument();
  expect(screen.getByText(/15/i)).toBeInTheDocument();
});

test('fetch fantasy details on player button clicked', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case rosterUrl:
        return Promise.resolve({data: {players: rosterPlayers}});
      case playerUrl:
        return Promise.resolve({data: players});
      case detailUrl:
        return Promise.resolve({data: fantasyPlayer});
      default:
        return Promise.reject(new Error('Axios Not Called 4'));
    }
  });
  render(<Roster />);
  await waitFor(()=> expect(axios.get).toHaveBeenCalledTimes(2));
  const button = screen.getAllByRole('button', {name: 'modal'});
  user.click(button[0]);
  await waitForElementToBeRemoved(()=> screen.getAllByText(/Loading/i));
  expect(screen.getByText(/Alvin Kamara/i)).toBeInTheDocument();
});

test('handlePlayer buttons sends put requests', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case rosterUrl:
        return Promise.resolve({data: {players: rosterPlayers}});
      case playerUrl:
        return Promise.resolve({data: players});
      default:
        return Promise.reject(new Error('Axios Not Called 5'));
    }
  });
  axios.put.mockImplementation((url) => {
    switch(url) {
      case addUrl:
        return Promise.resolve('Player Added!');
      case removeUrl:
        return Promise.resolve('Player Removed!');
      default:
        return Promise.reject(new Error('Axios Not Called 6'));
    }
  });
  render(<Roster />);
  await waitFor(()=> expect(axios.get).toHaveBeenCalledTimes(2));
  const button = screen.getAllByRole('button', {name: 'player'});
  user.click(button[0]);
  user.click(button[1]);
  await waitForElementToBeRemoved(()=> screen.getAllByText(/Loading/i));
  expect(axios.put).toHaveBeenCalledTimes(2);
});



