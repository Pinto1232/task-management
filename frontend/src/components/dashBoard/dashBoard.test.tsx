import {render, screen} from '@testing-library/react'
import DashBoard from './dashBoard'

describe('DashBoard', () => {
   test('should render without crashing', () => {
    render(<DashBoard>Content</DashBoard>)
    const el = screen.getByTestId('dash-board');
    expect(el).toBeInTheDocument();
    expect(el).toHaveTextContent('Content');
    })
   test('should match snapshot', () => {
    const {asFragment} = render(<DashBoard>Content</DashBoard>)
        expect(asFragment()).toMatchSnapshot()
    });
});
