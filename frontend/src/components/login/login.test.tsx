import {render, screen} from '@testing-library/react'
import Login from './login'

describe('Login', () => {
   test('should render without crashing', () => {
    render(<Login>Content</Login>)
    const el = screen.getByTestId('login');
    expect(el).toBeInTheDocument();
    expect(el).toHaveTextContent('Content');
    })
   test('should match snapshot', () => {
    const {asFragment} = render(<Login>Content</Login>)
        expect(asFragment()).toMatchSnapshot()
    });
});
