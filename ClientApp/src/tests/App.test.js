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

test('renders stats button', () => {  
  render(<App />);
  const button = screen.getByRole('button', {name: 'td-ratio'});
  expect(button).toBeInTheDocument();
});

test('renders PlayerForm week textfield', () => {
  render(<App />);
  const textElement = screen.getByLabelText(/Week/i);
  expect(textElement).toBeInTheDocument();
});

test('renders PlayerForm search textfield', () => {
  render(<App />);
  const textElement = screen.getByLabelText(/Search Player/i);
  expect(textElement).toBeInTheDocument();
});

test('renders StatsForm value textfield', () => {
  render(<App />);
  const textElement = screen.getByLabelText(/Input Value/i);
  expect(textElement).toBeInTheDocument();
});

test('renders PlayerTable accordion', () => {
  render(<App />);
  const textElement = screen.getByText(/Players/i);
  expect(textElement).toBeInTheDocument();
});

test('renders PlayerTable heading', () => {
  render(<App />);
  const textElements = screen.getAllByText(/Points/);
  expect(textElements).toHaveLength(1);
});
