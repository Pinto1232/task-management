import {render, screen} from '@testing-library/react'
import StopButton from './stopButton'

describe('StopButton', () => {
   test('should render without crashing', () => {
    render(<StopButton>Content</StopButton>)
    const el = screen.getByTestId('stop-button');
    expect(el).toBeInTheDocument();
    expect(el).toHaveTextContent('Content');
    })
   test('should match snapshot', () => {
    const {asFragment} = render(<StopButton>Content</StopButton>)
        expect(asFragment()).toMatchSnapshot()
    });
});
