import { render, screen } from '@testing-library/react';
import PlayerForm from '../components/PlayerForm';

const setPlayers = jest.fn();

test('renders search button', () => {  
  render(<PlayerForm setPlayers={()=> setPlayers()}/>);
  const button = screen.getByRole('button', {name: 'search'});
  expect(button).toBeTruthy();
});



