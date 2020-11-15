import { render, screen } from '@testing-library/react';
import StatsForm from '../components/StatsForm';

test('renders field select options', () => {
  render(<StatsForm />);

  expect(screen.getByLabelText(/Field/i)).toBeInTheDocument();
  expect(screen.getByLabelText(/Yards/i)).toBeInTheDocument();
});

test('renders type select options', () => {
  render(<StatsForm />);

  expect(screen.getByLabelText(/Type/i)).toBeInTheDocument();
  expect(screen.getByLabelText(/Passing/i)).toBeInTheDocument();
});

test('renders input value textfield', () => {
  render(<StatsForm />);

  expect(screen.getByLabelText(/Input Value/i)).toBeInTheDocument();
});

test('renders stats form button', () => {  
  render(<StatsForm />);

  expect(screen.getByRole('button', {name: 'stats'})).toBeInTheDocument();
});


