import { render, screen } from '@testing-library/react';
import StatsForm from '../components/StatsForm';

test('renders field select options', () => {
  render(<StatsForm />);
  const textElement = screen.getByLabelText(/Field/i);
  const optionElement = screen.getByLabelText(/Yards/i);

  expect(textElement).toBeInTheDocument();
  expect(optionElement).toBeInTheDocument();
});

test('renders type select options', () => {
  render(<StatsForm />);
  const textElement = screen.getByLabelText(/Type/i);
  const optionElement = screen.getByLabelText(/Passing/i);

  expect(textElement).toBeInTheDocument();
  expect(optionElement).toBeInTheDocument();
});

test('renders input value textfield', () => {
  render(<StatsForm />);
  const textElement = screen.getByLabelText(/Input Value/i);

  expect(textElement).toBeInTheDocument();
});

test('renders search button', () => {  
  render(<StatsForm />);
  const button = screen.getByRole('button', {name: 'stats'});
  
  expect(button).toBeInTheDocument();
});


