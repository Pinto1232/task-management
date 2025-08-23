import { useState } from 'react';
import styles from './navBar.module.css';
import type { NavBarProps } from './navBar.types';

const NavBar = ({ children, className, onMenuToggle }: NavBarProps) => {
  return (
    <nav className={[styles.container, className].filter(Boolean).join(' ')} data-testid="nav-bar">
      <div className={styles.content}>
        <button 
          className={styles.menuButton}
          onClick={onMenuToggle}
          aria-label="Toggle menu"
        >
          <span className={styles.hamburger}></span>
          <span className={styles.hamburger}></span>
          <span className={styles.hamburger}></span>
        </button>
        
        <div className={styles.brand}>
          <h1>Task Management</h1>
        </div>
        
        <div className={styles.actions}>
          {children}
        </div>
      </div>
    </nav>
  );
};

export default NavBar;