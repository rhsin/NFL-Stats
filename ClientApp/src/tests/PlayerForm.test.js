import { render, screen } from '@testing-library/react';
import PlayerForm from '../components/PlayerForm';

test('renders week select options', () => {
  render(<PlayerForm week={9} />);

  expect(screen.getByLabelText(/Week/i)).toBeInTheDocument();
  expect(screen.getByLabelText(/9/i)).toBeInTheDocument();
});

test('renders search player textfield', () => {
  render(<PlayerForm week={9} />);

  expect(screen.getByLabelText(/Search Player/i)).toBeInTheDocument();
});

test('renders position select options', () => {
  render(<PlayerForm week={9} />);

  expect(screen.getByLabelText(/Position/i)).toBeInTheDocument();
  expect(screen.getByLabelText(/QB/i)).toBeInTheDocument();
});

test('renders update button', () => {  
  render(<PlayerForm week={9} />);
  
  expect(screen.getByRole('button', {name: 'update'})).toBeInTheDocument();
});

test('renders search button', () => {  
  render(<PlayerForm week={9} />);
  
  expect(screen.getByRole('button', {name: 'search'})).toBeInTheDocument();
});


