import type React from 'react';

export type StopButtonVariant = 'default' | 'primary' | 'secondary' | 'ghost';
export type StopButtonSize = 'sm' | 'md' | 'lg';

type AsProp<T extends React.ElementType> = {
  as?: T;
};

type PolymorphicProps<T extends React.ElementType, P> = P &
  AsProp<T> &
  Omit<React.ComponentPropsWithoutRef<T>, keyof P | 'as' | 'ref'> & {
    className?: string;
  };

export type BaseStopButtonProps = {
  variant?: StopButtonVariant;
  size?: StopButtonSize;
  fullWidth?: boolean;
  children?: React.ReactNode;
};

export type StopButtonProps<T extends React.ElementType = 'div'> =
  PolymorphicProps<T, BaseStopButtonProps>;
