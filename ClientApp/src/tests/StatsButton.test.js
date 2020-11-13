import { render, screen } from '@testing-library/react';
import StatsButton from '../components/StatsButton';
import { players } from './TestData';

const setPlayers = jest.fn();
const setTable = jest.fn();

test('renders season select options', () => {  
  render(
    <StatsButton
      players={players}
      setPlayers={()=> setPlayers()}
      setTable={()=> setTable()}
    />
  );
  const textElement = screen.getAllByLabelText(/Season/i);
  const optionElement = screen.getByLabelText(/2019/i);

  expect(textElement).toHaveLength(2);
  expect(optionElement).toBeInTheDocument();
});

test('renders ratio button', () => {  
  render(
    <StatsButton
      players={players}
      setPlayers={()=> setPlayers()}
      setTable={()=> setTable()}
    />
  );
  const button = screen.getByRole('button', {name: 'ratio'});
  
  expect(button).toBeInTheDocument();
});

test('renders reset button', () => {  
  render(
    <StatsButton
      players={players}
      setPlayers={()=> setPlayers()}
      setTable={()=> setTable()}
    />
  );
  const button = screen.getByRole('button', {name: 'reset'});
  
  expect(button).toBeInTheDocument();
});



