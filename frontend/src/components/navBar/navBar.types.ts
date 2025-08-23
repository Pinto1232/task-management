import type React from 'react';

export interface NavBarProps {
	className?: string;
	children?: React.ReactNode;
	onMenuToggle?: () => void;
}