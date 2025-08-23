import {render, screen} from '@testing-library/react'
import SidebarMenu from './sidebarMenu'

describe('SidebarMenu', () => {
   test('should render without crashing', () => {
    render(<SidebarMenu>Content</SidebarMenu>)
    const el = screen.getByTestId('sidebar-menu');
    expect(el).toBeInTheDocument();
    expect(el).toHaveTextContent('Content');
    })
   test('should match snapshot', () => {
    const {asFragment} = render(<SidebarMenu>Content</SidebarMenu>)
        expect(asFragment()).toMatchSnapshot()
    });
});
