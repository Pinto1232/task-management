import styles from './registration.module.css';
import type { RegistrationProps } from './registration.types';

const Registration = ({ className, onSwitchToLogin }: RegistrationProps) => {
  return (
    <div className={[styles.container, className].filter(Boolean).join(' ')} data-testid="registration">
      <h2 className={styles.title}>Create Account</h2>
      <p className={styles.subtitle}>Join us to get started</p>
      <form className={styles.form}>
        <div className={styles.inputGroup}>
          <label htmlFor="fullName" className={styles.label}>Full Name</label>
          <input
            type="text"
            id="fullName"
            className={styles.input}
            placeholder="Enter your full name"
            required
          />
        </div>
        <div className={styles.inputGroup}>
          <label htmlFor="regEmail" className={styles.label}>Email</label>
          <input
            type="email"
            id="regEmail"
            className={styles.input}
            placeholder="Enter your email"
            required
          />
        </div>
        <div className={styles.inputGroup}>
          <label htmlFor="regPassword" className={styles.label}>Password</label>
          <input
            type="password"
            id="regPassword"
            className={styles.input}
            placeholder="Create a password"
            required
          />
        </div>
        <div className={styles.inputGroup}>
          <label htmlFor="confirmPassword" className={styles.label}>Confirm Password</label>
          <input
            type="password"
            id="confirmPassword"
            className={styles.input}
            placeholder="Confirm your password"
            required
          />
        </div>
        <button type="submit" className={styles.submitButton}>
          Create Account
        </button>
      </form>
      <p className={styles.switchText}>
        Already have an account? 
        <button 
          type="button"
          onClick={onSwitchToLogin}
          className={styles.link}
        >
          Sign in
        </button>
      </p>
    </div>
  );
};

export default Registration;