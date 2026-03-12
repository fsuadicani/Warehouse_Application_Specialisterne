import { useState } from 'react';

function Login({ onLogin }) {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState('');
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const DEV_ONLY_LOGIN_ENABLED = import.meta.env.DEV && import.meta.env.VITE_DEV_LOGIN_ENABLED === 'true';

  const handleSubmit = async (e) => {
    e.preventDefault();

    const normalizedUsername = username.trim().toLowerCase();
    const normalizedPassword = password;
    // TODO: Remove DEV-only login fallback before release and rely only on server-side authentication.
    if (DEV_ONLY_LOGIN_ENABLED) {
      const devUsername = (import.meta.env.VITE_DEV_LOGIN_USERNAME ?? '').trim().toLowerCase();
      const devPassword = (import.meta.env.VITE_DEV_LOGIN_PASSWORD ?? '').trim();

      if (normalizedUsername === devUsername && normalizedPassword === devPassword && devUsername && devPassword) {
        setErrorMessage('');
        setIsAuthenticated(true);

        const session = {
          username: normalizedUsername,
          token: 'dev-auth-token',
          loggedInAt: new Date().toISOString(),
        };

        localStorage.setItem('warehouseAuth', JSON.stringify(session));

        if (typeof onLogin === 'function') {
          onLogin(session);
        }

        window.location.hash = '#/home';
        return;
      }

      setIsAuthenticated(false);
      setErrorMessage('Invalid username or password');
      return;
    }

    try {
      const response = await fetch('/api/auth/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          username: normalizedUsername,
          password: normalizedPassword,
        }),
      });

      if (!response.ok) {
        throw new Error('Invalid username or password');
      }

      const data = await response.json();
      const session = {
        username: data.username ?? normalizedUsername,
        token: data.token ?? '',
        loggedInAt: new Date().toISOString(),
      };

      setErrorMessage('');
      setIsAuthenticated(true);

      localStorage.setItem('warehouseAuth', JSON.stringify(session));

      if (typeof onLogin === 'function') {
        onLogin(session);
      }

      window.location.hash = '#/home';
    } catch {
      setIsAuthenticated(false);
      setErrorMessage('Invalid username or password');
    }
  };


  return (
    <form className="login-form" onSubmit={handleSubmit}>
      <div className="login-grid">
        <div className="field-column">
          <input
            type="text"
            id="username"
            name="username"
            placeholder="Enter your username"
            required
            value={username}
            onChange={(e) => setUsername(e.target.value)}
          />

          <div className="form-meta">
            <label className="remember">
              <input type="checkbox" id="remember" name="remember" /> Remember me
            </label>
            <a href="#">Forgot password?</a>
          </div>
        </div>

        <div className="field-column field-column-password">
          <input
            type="password"
            id="password"
            name="password"
            placeholder="Enter your password"
            required
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />

          <button className="login-submit" type="submit">Login</button>
        </div>
      </div>

      {isAuthenticated && <p className="login-success">Login successful.</p>}
      {errorMessage && <p className="login-error">{errorMessage}</p>}
    </form>
  );
}

export default Login;