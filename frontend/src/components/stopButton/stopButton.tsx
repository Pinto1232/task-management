import styles from './stopButton.module.css';
import type { StopButtonProps } from './stopButton.types';

const StopButton = ({ children, className }: StopButtonProps) => {
  return (
    <div className={[styles.container, className].filter(Boolean).join(' ')} data-testid="stop-button">
      {children} Pinto
    </div>
  );
};

export default StopButton;