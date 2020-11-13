import { render, screen } from '@testing-library/react';
import StatsButton from '../components/StatsButton';
import { players } from './TestData';

const setPlayers = jest.fn();
const setTable = jest.fn();

test('renders TD Ratio button', () => {  
  render(
    <StatsButton
      players={players}
      setPlayers={()=> setPlayers()}
      setTable={()=> setTable()}
    />
  );
  const button = screen.getByRole('button', {name: 'td-ratio'});
  expect(button).toBeInTheDocument();
});



