import { render, screen } from '@testing-library/react';
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



