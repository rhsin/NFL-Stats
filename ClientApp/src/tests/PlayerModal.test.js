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
      setOpen={()=> setOpen()}
    />
  );
  const textElement = screen.getByText(/Points/i);
  expect(textElement).toBeInTheDocument();
});

test('setOpen called on button click', () => {  
  render(
    <PlayerModal 
      open={true}
      players={players}
      setOpen={()=> setOpen()}
    />
  );
  const button = screen.getByRole('button', {name: 'Close'});
  user.click(button);
  expect(setOpen).toHaveBeenCalledTimes(1);
});



