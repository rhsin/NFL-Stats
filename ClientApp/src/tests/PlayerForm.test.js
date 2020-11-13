import { render, screen } from '@testing-library/react';
import PlayerForm from '../components/PlayerForm';

const setPlayers = jest.fn();
const setWeek = jest.fn();

test('renders week select options', () => {
  render(
    <PlayerForm 
      week={9}
      setPlayers={()=> setPlayers()}
      setWeek={()=> setWeek()}
    />
  );
  const textElement = screen.getByLabelText(/Week/i);
  const optionElement = screen.getByLabelText(/9/i);

  expect(textElement).toBeInTheDocument();
  expect(optionElement).toBeInTheDocument();
});

test('renders position select options', () => {
  render(
    <PlayerForm 
      week={9}
      setPlayers={()=> setPlayers()}
      setWeek={()=> setWeek()}
    />
  );
  const textElement = screen.getByLabelText(/Position/i);
  const optionElement = screen.getByLabelText(/QB/i);

  expect(textElement).toBeInTheDocument();
  expect(optionElement).toBeInTheDocument();
});

test('renders search button', () => {  
  render(
    <PlayerForm
      week={9}
      setPlayers={()=> setPlayers()}
      setWeek={()=> setWeek()}
    />
  );
  const button = screen.getByRole('button', {name: 'search'});
  
  expect(button).toBeInTheDocument();
});


