import { render, screen } from '@testing-library/react';
import user from '@testing-library/user-event';
import PlayerModal from '../components/PlayerModal';
import { players } from './TestData';

const setOpen = jest.fn();

test('renders fantasy table on open', () => {  
  render(
    <PlayerModal 
      open={true}
      players={players}
    />
  );

  expect(screen.getByText(/Points/i)).toBeInTheDocument();
});

test('renders submit button', () => {  
  render(
    <PlayerModal 
      open={true}
      players={players}
    />
  );

  expect(screen.getByRole('button', {name: 'Submit'})).toBeInTheDocument();
});

test('setOpen called on close button click', () => {  
  render(
    <PlayerModal 
      open={true}
      players={players}
      setOpen={()=> setOpen()}
    />
  );
  user.click(screen.getByRole('button', {name: 'Close'}));
  
  expect(setOpen).toHaveBeenCalledTimes(1);
});



