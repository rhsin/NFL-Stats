import { render, screen } from '@testing-library/react';
import App from '../App';

test('renders App heading', () => {
  render(<App />);
  const textElement = screen.getByText(/NFL Stats/i);
  expect(textElement).toBeInTheDocument();
});

test('renders PlayerTable heading', () => {
  render(<App />);
  const textElement = screen.getByText(/Player/i);
  expect(textElement).toBeInTheDocument();
});
