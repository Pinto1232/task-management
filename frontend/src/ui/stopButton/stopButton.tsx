import { forwardRef } from 'react';
import styles from './stopButton.module.css';
import type { StopButtonProps } from './stopButton.types';

function cn(...classes: Array<string | false | null | undefined>) {
  return classes.filter(Boolean).join(' ');
}

type StopButtonComponent = (<C extends React.ElementType = 'div'>(
  props: StopButtonProps<C> & { ref?: React.Ref<unknown> }
) => React.ReactElement | null) & { displayName?: string };

const StopButton = forwardRef<React.ElementType, StopButtonProps<React.ElementType>>((
  {
    as,
    className,
    variant = 'default',
    size = 'md',
    fullWidth = false,
    children,
    ...rest
  },
  ref
) => {
  const Tag = (as || 'div') as React.ElementType;
  return (
    <Tag
      ref={ref as unknown}
      className={cn(
        styles.container,
        styles[`variant_${variant}`],
        styles[`size_${size}`],
        fullWidth && styles.fullWidth,
        className
      )}
      data-testid="stop-button"
      {...rest}
    >
      {children}
    </Tag>
  );
}) as StopButtonComponent;

StopButton.displayName = 'StopButton';

export default StopButton;
