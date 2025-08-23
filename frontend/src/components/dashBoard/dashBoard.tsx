import { useState } from 'react';
import styles from './dashBoard.module.css';
import type { DashBoardProps } from './dashBoard.types';
import NavBar from '../navBar';
import SidebarMenu from '../sidebarMenu';

const DashBoard = ({ children, className }: DashBoardProps) => {
  const [isSidebarOpen, setIsSidebarOpen] = useState(false);

  const toggleSidebar = () => {
    setIsSidebarOpen(!isSidebarOpen);
  };

  return (
    <div className={[styles.container, className].filter(Boolean).join(' ')} data-testid="dash-board">
      <NavBar onMenuToggle={toggleSidebar}>
        <button className={styles.logoutButton}>
          Logout
        </button>
      </NavBar>
      
      <SidebarMenu isOpen={isSidebarOpen} />
      
      <main className={[styles.main, isSidebarOpen ? styles.mainWithSidebar : ''].filter(Boolean).join(' ')}>
        {children}
      </main>
    </div>
  );
};

export default DashBoard;