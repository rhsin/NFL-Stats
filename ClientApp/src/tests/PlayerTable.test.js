import { render, screen } from '@testing-library/react';
import user from '@testing-library/user-event';
import PlayerTable from '../components/PlayerTable';
import { players } from './TestData';

const handleClick = jest.fn();

test('renders table rows from props', () => {  
  render(<PlayerTable players={players}/>);
  const textElement = screen.getByText(/Patrick Mahomes/i);
  expect(textElement).toBeInTheDocument();
});

test('add player button calls handleClick', () => {  
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

