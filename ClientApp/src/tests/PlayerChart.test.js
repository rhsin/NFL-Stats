import { render, screen } from '@testing-library/react';
import PlayerChart from '../components/PlayerChart';
import { players } from '../TestData';

test('renders players passed through props as bar chart', () => {
  render(<PlayerChart players={players} />);

  expect(screen.getByText(/Patrick Mahomes/i)).toBeInTheDocument();
  expect(screen.getByText(/4000/i)).toBeInTheDocument();
  expect(screen.getByText(/5097/i)).toBeInTheDocument();
});



