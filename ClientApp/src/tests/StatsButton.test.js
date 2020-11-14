import { render, screen } from '@testing-library/react';
import StatsButton from '../components/StatsButton';

test('renders season select options', () => {  
  render(<StatsButton />);
  const textElement = screen.getAllByLabelText(/Season/i);
  const optionElement = screen.getByLabelText(/2019/i);

  expect(textElement).toHaveLength(2);
  expect(optionElement).toBeInTheDocument();
});

test('renders season form button', () => {  
  render(<StatsButton />);
  const button = screen.getByRole('button', {name: 'season'});

  expect(button).toBeInTheDocument();
});

test('renders ratio button', () => {  
  render(<StatsButton />);
  const button = screen.getByRole('button', {name: 'ratio'});
  
  expect(button).toBeInTheDocument();
});

test('renders yards button', () => {  
  render(<StatsButton />);
  const button = screen.getByRole('button', {name: 'yards'});
  
  expect(button).toBeInTheDocument();
});

test('renders reset button', () => {  
  render(<StatsButton />);
  const button = screen.getByRole('button', {name: 'reset'});
  
  expect(button).toBeInTheDocument();
});



