import { render, screen } from '@testing-library/react';
import PlayerChart from '../components/PlayerChart';
import { players } from '../TestData';

test('renders players passed through props as bar chart', () => {
  render(<PlayerChart players={players} />);

  expect(screen.getAllByText(/Patrick Mahomes/i)).toBeTruthy();
});



