import { render, screen } from '@testing-library/react';
import user from '@testing-library/user-event';
import PlayerTable from '../components/PlayerTable';
import FantasyTable from '../components/FantasyTable';
import { players, fantasyPlayers } from './TestData';

const handleClick = jest.fn();
const handleModal = jest.fn();

test('renders accordion heading from props', () => {  
  render(<PlayerTable table='Test' players={players} />);
  const textElements = screen.getAllByText(/Test/);

  expect(textElements).toHaveLength(1);
});

test('renders table headings', () => {  
  render(<PlayerTable players={players} />);
  const textElements = screen.getAllByText(/Points/);

  expect(textElements).toHaveLength(1);
});

test('renders table rows from props', () => {  
  render(<PlayerTable players={players} />);
  const textElement = screen.getByText(/Patrick Mahomes/i);

  expect(textElement).toBeInTheDocument();
});

test('renders fantasy table rows from props', () => {  
  render(<FantasyTable players={fantasyPlayers} />);
  const textElement = screen.getByText(/Travis Kelce/i);
  
  expect(textElement).toBeInTheDocument();
});

test('handlePlayer button calls handleClick', () => {  
  render(
    <PlayerTable
      players={players} 
      handleClick={()=> handleClick()}
    />
  );
  const button = screen.getByRole('button', {name: 'player'});
  user.click(button);

  expect(handleClick).toHaveBeenCalledTimes(1);
});

test('player button calls handleModal', () => {  
  render(
    <PlayerTable
      players={players} 
      handleModal={()=> handleModal()}
    />
  );
  const button = screen.getByRole('button', {name: 'modal'});
  user.click(button);

  expect(handleModal).toHaveBeenCalledTimes(1);
});

