import { render, screen } from '@testing-library/react';
import PlayerForm from '../components/PlayerForm';

const setPlayers = jest.fn();
const setLoading = jest.fn();

test('renders search button', () => {  
  render(
    <PlayerForm 
      setPlayers={()=> setPlayers()}
      setLoading={()=> setLoading()}
    />
  );
  const button = screen.getByRole('button', {name: 'search'});
  expect(button).toBeInTheDocument();
});

test('renders position select', () => {
  render(
    <PlayerForm 
      setPlayers={()=> setPlayers()}
      setLoading={()=> setLoading()}
    />
  );
  const textElement = screen.getByLabelText(/Position/i);
  expect(textElement).toBeInTheDocument();
});


