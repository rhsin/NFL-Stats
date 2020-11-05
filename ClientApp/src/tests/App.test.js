import { render, screen } from '@testing-library/react';
import App from '../App';

test('renders app heading', () => {
  render(<App />);
  const textElement = screen.getByText(/NFL Stats/i);
  expect(textElement).toBeInTheDocument();
});
