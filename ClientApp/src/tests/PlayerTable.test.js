import { render, screen } from '@testing-library/react';
import user from '@testing-library/user-event';
import PlayerTable from '../components/PlayerTable';

const players = [
  {
    id: 455,
    name: 'Patrick Mahomes',
    position: 'QB',
    team: 'KAN',
    points: 285.04
  }
];

const handleClick = jest.fn();

test('renders table rows', () => {  
  render(<PlayerTable players={players}/>);
  const textElement = screen.getByText(/Patrick Mahomes/i);
  expect(textElement).toBeInTheDocument();
});

test('button calls handleClick', () => {  
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

