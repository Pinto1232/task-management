import styles from './login.module.css';
import type { LoginProps } from './login.types';

const Login = ({ className, onSwitchToRegistration }: LoginProps) => {
  return (
    <div className={[styles.container, className].filter(Boolean).join(' ')} data-testid="login">
      <h2 className={styles.title}>Welcome Back</h2>
      <p className={styles.subtitle}>Sign in to your account</p>
      <form className={styles.form}>
        <div className={styles.inputGroup}>
          <label htmlFor="email" className={styles.label}>Email</label>
          <input
            type="email"
            id="email"
            className={styles.input}
            placeholder="Enter your email"
            required
          />
        </div>
        <div className={styles.inputGroup}>
          <label htmlFor="password" className={styles.label}>Password</label>
          <input
            type="password"
            id="password"
            className={styles.input}
            placeholder="Enter your password"
            required
          />
        </div>
        <button type="submit" className={styles.submitButton}>
          Sign In
        </button>
      </form>
      <p className={styles.switchText}>
        Don't have an account? 
        <button 
          type="button"
          onClick={onSwitchToRegistration}
          className={styles.link}
        >
          Sign up
        </button>
      </p>
    </div>
  );
};

export default Login;