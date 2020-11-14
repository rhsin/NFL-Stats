import { render, screen, waitFor, waitForElementToBeRemoved } from '@testing-library/react';
import user from '@testing-library/user-event';
import axios from 'axios';
import Roster from '../../components/Roster';
import { url } from '../../components/AppConstants';
import { players, rosterPlayers, fantasyPlayers, fantasyPlayer, ratioPlayers, yardsPlayers } from '../TestData';

jest.mock('axios');

const rosterUrl = url + 'Rosters/1';
const playerUrl = url + 'Players';
const fantasyUrl = url + 'Stats/Fantasy/Rosters/1/9';
const detailUrl = url + 'Stats/Fantasy/2/9';
const ratioUrl = url + 'Stats/Ratio/Passing';
const yardsUrl = url + 'Stats/Scrimmage';
const checkUrl = url + 'Rosters/Fantasy';

test('fetch fantasy roster on update button click', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case fantasyUrl:
        return Promise.resolve({data: fantasyPlayers});
      default:
        return Promise.reject(new Error('Axios Not Called: Update Button'));
    }
  });
  render(<Roster />);

  user.click(screen.getByRole('button', {name: 'update'}));
  await waitForElementToBeRemoved(()=> screen.getAllByText(/Loading/i));

  expect(axios.get).toHaveBeenCalledTimes(4);
  expect(screen.getByText(/Travis Kelce/i)).toBeInTheDocument();
});

test('fetch data on ratio stats button click', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case playerUrl:
        return Promise.resolve({data: players});
      default:
        return Promise.reject(new Error('Axios Not Called: Ratio'));
    }
  });
  axios.post.mockImplementation((url) => {
    switch(url) {
      case ratioUrl:
        return Promise.resolve({data: ratioPlayers});
      default:
        return Promise.reject(new Error('Axios Not Called: Ratio Button'));
    }
  });
  render(<Roster />);

  await waitForElementToBeRemoved(()=> screen.getAllByText(/Loading/i));
  user.click(screen.getByRole('button', {name: 'ratio'}));
  await waitFor(()=> expect(axios.post).toHaveBeenCalledTimes(1));

  expect(screen.getByText(/Patrick Mahomes/i)).toBeInTheDocument();
  expect(screen.getByText(/3.25/i)).toBeInTheDocument();
});

test('fetch data on scrimmage yards stats button click', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case playerUrl:
        return Promise.resolve({data: players});
      default:
        return Promise.reject(new Error('Axios Not Called: Yards'));
    }
  });
  axios.post.mockImplementation((url) => {
    switch(url) {
      case yardsUrl:
        return Promise.resolve({data: yardsPlayers});
      default:
        return Promise.reject(new Error('Axios Not Called: Yards Button'));
    }
  });
  render(<Roster />);

  await waitForElementToBeRemoved(()=> screen.getAllByText(/Loading/i));
  user.click(screen.getByRole('button', {name: 'yards'}));
  await waitFor(()=> expect(axios.post).toHaveBeenCalledTimes(1));

  expect(screen.getByText(/Christian McCaffrey/i)).toBeInTheDocument();
  expect(screen.getByText(/1965.00/i)).toBeInTheDocument();
});

test('fetch fantasy roster total on submit button click', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case fantasyUrl:
        return Promise.resolve({data: fantasyPlayers});
      default:
        return Promise.reject(new Error('Axios Not Called: Fantasy Total'));
    }
  });
  axios.post.mockImplementation((url) => {
    switch(url) {
      case checkUrl:
        return Promise.resolve({data: 15});
      default:
        return Promise.reject(new Error('Axios Not Called: Submit Button'));
    }
  });
  render(<Roster />);

  user.click(screen.getByRole('button', {name: 'update'}));
  await waitForElementToBeRemoved(()=> screen.getAllByText(/Loading/i));

  user.click(screen.getByRole('button', {name: 'Submit'}));
  await waitFor(()=> expect(axios.post).toHaveBeenCalledTimes(1));
  
  expect(screen.getByText(/Travis Kelce/i)).toBeInTheDocument();
  expect(screen.getByText(/15/i)).toBeInTheDocument();
});

test('fetch fantasy details on player button click', async () => {
  axios.get.mockImplementation((url) => {
    switch(url) {
      case rosterUrl:
        return Promise.resolve({data: {players: rosterPlayers}});
      case playerUrl:
        return Promise.resolve({data: players});
      case detailUrl:
        return Promise.resolve({data: fantasyPlayer});
      default:
        return Promise.reject(new Error('Axios Not Called: Player Button'));
    }
  });
  render(<Roster />);

  await waitFor(()=> expect(axios.get).toHaveBeenCalledTimes(2));
  user.click(screen.getAllByRole('button', {name: 'modal'})[0]);
  await waitForElementToBeRemoved(()=> screen.getAllByText(/Loading/i));
  
  expect(screen.getByText(/Alvin Kamara/i)).toBeInTheDocument();
});




