import styles from './sidebarMenu.module.css';
import type { SidebarMenuProps } from './sidebarMenu.types';
import { MdDashboard, MdTask, MdFolder, MdCalendarToday, MdSettings } from 'react-icons/md';

const SidebarMenu = ({ children, className, isOpen = false }: SidebarMenuProps) => {
  return (
    <aside 
      className={[
        styles.container, 
        isOpen ? styles.open : styles.closed,
        className
      ].filter(Boolean).join(' ')} 
      data-testid="sidebar-menu"
    >
      <div className={styles.content}>
        <nav className={styles.navigation}>
          <ul className={styles.menuList}>
            <li className={styles.menuItem}>
              <a href="#dashboard" className={styles.menuLink}>
                <span className={styles.icon}>
                  <MdDashboard />
                </span>
                Dashboard
              </a>
            </li>
            <li className={styles.menuItem}>
              <a href="#tasks" className={styles.menuLink}>
                <span className={styles.icon}>
                  <MdTask />
                </span>
                Tasks
              </a>
            </li>
            <li className={styles.menuItem}>
              <a href="#projects" className={styles.menuLink}>
                <span className={styles.icon}>
                  <MdFolder />
                </span>
                Projects
              </a>
            </li>
            <li className={styles.menuItem}>
              <a href="#calendar" className={styles.menuLink}>
                <span className={styles.icon}>
                  <MdCalendarToday />
                </span>
                Calendar
              </a>
            </li>
            <li className={styles.menuItem}>
              <a href="#settings" className={styles.menuLink}>
                <span className={styles.icon}>
                  <MdSettings />
                </span>
                Settings
              </a>
            </li>
          </ul>
        </nav>
        {children}
      </div>
    </aside>
  );
};

export default SidebarMenu;