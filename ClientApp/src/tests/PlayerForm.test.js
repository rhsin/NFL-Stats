import { render, screen } from '@testing-library/react';
import PlayerForm from '../components/PlayerForm';

test('renders week select options', () => {
  render(<PlayerForm week={9} />);
  const textElement = screen.getByLabelText(/Week/i);
  const optionElement = screen.getByLabelText(/9/i);

  expect(textElement).toBeInTheDocument();
  expect(optionElement).toBeInTheDocument();
});

test('renders search player textfield', () => {
  render(<PlayerForm week={9} />);
  const textElement = screen.getByLabelText(/Search Player/i);

  expect(textElement).toBeInTheDocument();
});

test('renders position select options', () => {
  render(<PlayerForm week={9} />);
  const textElement = screen.getByLabelText(/Position/i);
  const optionElement = screen.getByLabelText(/QB/i);

  expect(textElement).toBeInTheDocument();
  expect(optionElement).toBeInTheDocument();
});

test('renders update button', () => {  
  render(<PlayerForm week={9} />);
  const button = screen.getByRole('button', {name: 'update'});
  
  expect(button).toBeInTheDocument();
});

test('renders search button', () => {  
  render(<PlayerForm week={9} />);
  const button = screen.getByRole('button', {name: 'search'});
  
  expect(button).toBeInTheDocument();
});


