import { render, screen } from '@testing-library/react';
import StatsButton from '../components/StatsButton';

test('renders season select options', () => {  
  render(<StatsButton />);

  expect(screen.getAllByLabelText(/Season/i)).toHaveLength(2);
  expect(screen.getByLabelText(/2019/i)).toBeInTheDocument();
});

test('renders season form button', () => {  
  render(<StatsButton />);

  expect(screen.getByRole('button', {name: 'season'})).toBeInTheDocument();
});

test('renders touchdowns button', () => {  
  render(<StatsButton />);
  
  expect(screen.getByRole('button', {name: 'touchdowns'})).toBeInTheDocument();
});

test('renders ratio button', () => {  
  render(<StatsButton />);
  
  expect(screen.getByRole('button', {name: 'ratio'})).toBeInTheDocument();
});

test('renders yards button', () => {  
  render(<StatsButton />);
  
  expect(screen.getByRole('button', {name: 'yards'})).toBeInTheDocument();
});

test('renders reset button', () => {  
  render(<StatsButton />);

  expect(screen.getByRole('button', {name: 'reset'})).toBeInTheDocument();
});



