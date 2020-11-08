import { render, screen } from '@testing-library/react';
import App from '../App';

test('renders NavBar heading', () => {
  render(<App />);
  const textElement = screen.getByText(/NFL Stats/i);
  expect(textElement).toBeInTheDocument();
});

test('renders loading alert', () => {
  render(<App />);
  const textElement = screen.getByText(/Loading/i);
  expect(textElement).toBeInTheDocument();
});

test('renders update button', () => {  
  render(<App />);
  const button = screen.getByRole('button', {name: 'search'});
  expect(button).toBeInTheDocument();
});

test('renders PlayerTable heading', () => {
  render(<App />);
  const textElements = screen.getAllByText(/Points/);
  expect(textElements).toBeTruthy();
});

test('renders PlayerForm search textfield', () => {
  render(<App />);
  const textElement = screen.getByLabelText(/Search Player/i);
  expect(textElement).toBeInTheDocument();
});

test('renders PlayerForm select textfield', () => {
  render(<App />);
  const textElement = screen.getByLabelText(/Position/i);
  expect(textElement).toBeInTheDocument();
});
