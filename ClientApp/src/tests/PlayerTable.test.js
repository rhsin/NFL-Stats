import { render, screen } from '@testing-library/react';
import user from '@testing-library/user-event';
import PlayerTable from '../components/PlayerTable';
import FantasyTable from '../components/FantasyTable';
import { players, fantasyPlayers } from './TestData';

const handleClick = jest.fn();
const handleModal = jest.fn();

test('renders accordion heading from props', () => {  
  render(<PlayerTable table='Test' players={players} />);

  expect(screen.getAllByText(/Test/)).toHaveLength(1);
});

test('renders table headings', () => {  
  render(<PlayerTable players={players} />);

  expect(screen.getAllByText(/Points/)).toHaveLength(1);
});

test('renders table rows from props', () => {  
  render(<PlayerTable players={players} />);

  expect(screen.getByText(/Patrick Mahomes/i)).toBeInTheDocument();
});

test('renders fantasy table rows from props', () => {  
  render(<FantasyTable players={fantasyPlayers} />);

  expect(screen.getByText(/Travis Kelce/i)).toBeInTheDocument();
});

test('handlePlayer button calls handleClick', () => {  
  render(
    <PlayerTable
      players={players} 
      handleClick={()=> handleClick()}
    />
  );
  user.click(screen.getByRole('button', {name: 'player'}));

  expect(handleClick).toHaveBeenCalledTimes(1);
});

test('player button calls handleModal', () => {  
  render(
    <PlayerTable
      players={players} 
      handleModal={()=> handleModal()}
    />
  );
  user.click(screen.getByRole('button', {name: 'modal'}));

  expect(handleModal).toHaveBeenCalledTimes(1);
});

