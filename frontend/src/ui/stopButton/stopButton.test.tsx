import { render, screen } from '@testing-library/react';
import StopButton from './stopButton';

describe('StopButton (UI)', () => {
  test('renders content', () => {
    render(<StopButton>Hello</StopButton>);
    expect(screen.getByTestId('stop-button')).toHaveTextContent('Hello');
  });

  test('supports variant and size classes', () => {
    render(<StopButton variant="primary" size="lg" />);
    const el = screen.getByTestId('stop-button');
    expect(el.className).toMatch(/variant_/);
    expect(el.className).toMatch(/size_/);
  });

  test('allows polymorphic `as` prop', () => {
    render(
      <StopButton as="button" type="button">Click</StopButton>
    );
    const el = screen.getByRole('button');
    expect(el).toBeInTheDocument();
  });
});
