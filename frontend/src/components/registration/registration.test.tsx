import {render, screen} from '@testing-library/react'
import Registration from './registration'

describe('Registration', () => {
   test('should render without crashing', () => {
    render(<Registration>Content</Registration>)
    const el = screen.getByTestId('registration');
    expect(el).toBeInTheDocument();
    expect(el).toHaveTextContent('Content');
    })
   test('should match snapshot', () => {
    const {asFragment} = render(<Registration>Content</Registration>)
        expect(asFragment()).toMatchSnapshot()
    });
});
