import { useState } from 'react';


function LoginPage({ onLogin }) {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [remember, setRemember] = useState(false);
  const [errorMessage, setErrorMessage] = useState('');
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const DEV_ONLY_LOGIN_ENABLED = import.meta.env.DEV && import.meta.env.VITE_DEV_LOGIN_ENABLED === 'true';

  const createLabeledError = (name, message) => {
    const error = new Error(message);
    error.name = name;
    return error;
  };

  const getResponseErrorDetails = async (response) => {
    const contentType = response.headers.get('content-type') ?? '';

    try {
      if (contentType.includes('application/json')) {
        const body = await response.json();
        if (typeof body?.message === 'string' && body.message.trim()) {
          return body.message.trim();
        }
        if (typeof body?.error === 'string' && body.error.trim()) {
          return body.error.trim();
        }
      }

      const text = await response.text();
      if (text.trim()) {
        return text.trim();
      }
    } catch {
      return '';
    }

    return '';
  };

  const createResponseError = async (response) => {
    const details = await getResponseErrorDetails(response);
    const statusText = response.statusText?.trim() || 'Request failed';

    if (response.status === 401 || response.status === 403) {
      return createLabeledError('AuthError', details || 'Invalid username or password');
    }

    if (response.status >= 500) {
      return createLabeledError(
        'ServerError',
        details || `Server error (${response.status} ${statusText}). Please try again later.`,
      );
    }

    return createLabeledError(
      'RequestError',
      details || `Login failed (${response.status} ${statusText}).`,
    );
  };

  const persistSession = (session) => {
    const storage = remember ? localStorage : sessionStorage;
    const alternateStorage = remember ? sessionStorage : localStorage;

    alternateStorage.removeItem('warehouseAuth');
    storage.setItem('warehouseAuth', JSON.stringify(session));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    const normalizedUsername = username.trim();
    const normalizedPassword = password.trim();
    // TODO: Remove DEV-only login fallback before release
    if (DEV_ONLY_LOGIN_ENABLED) {
      const devUsername = (import.meta.env.VITE_DEV_LOGIN_USERNAME ?? '').trim();
      const devPassword = (import.meta.env.VITE_DEV_LOGIN_PASSWORD ?? '').trim();

      if (normalizedUsername === devUsername && normalizedPassword === devPassword && devUsername && devPassword) {
        setErrorMessage('');
        setIsAuthenticated(true);

        const session = {
          username: normalizedUsername,
          token: 'dev-auth-token',
          loggedInAt: new Date().toISOString(),
        };

        persistSession(session);

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
        throw await createResponseError(response);
      }

      let data;
      try {
        data = await response.json();
      } catch {
        throw createLabeledError('ParseError', 'Received an invalid response from the server.');
      }

      if (!data.token) {
        throw new Error('Authentication failed: no token received');
      }
      const session = {
        username: data.username ?? normalizedUsername,
        token: data.token,
        loggedInAt: new Date().toISOString(),
      };
      setErrorMessage('');
      setIsAuthenticated(true);

      persistSession(session);

      if (typeof onLogin === 'function') {
        onLogin(session);
      }

      window.location.hash = '#/home';
    } catch (error) {
      setIsAuthenticated(false);

      if (error instanceof TypeError) {
        setErrorMessage('Network error. Check your connection and try again.');
        return;
      }

      if (error instanceof Error) {
        setErrorMessage(error.message);
        return;
      }

      setErrorMessage('Login failed. Please try again.');
    }
  };

  return (
    <browserRouter>
      <div className="header-container">
        <h1></h1>
      </div>
      <div className="button-container"/>

      <div className="login-container">

        <div className="login-box">
          <div className="header-container">
            <h1>Velkommen</h1>
          </div>
          <div className="header-container">
            <h2>Login herunder:</h2>
          </div>
          <form className="login-form" onSubmit={handleSubmit}>
            <div className="login-grid">
              <div className="field-column">
                <input
                    className="input"
                    type="text"
                    id="username"
                    name="username"
                    placeholder="Enter your username"
                    required
                    value={username}
                    onChange={(e) => setUsername(e.target.value)}
                />
              </div>



              <div className="field-column field-column-password">
                <input
                    className={`input`}
                    type="password"
                    id="password"
                    name="password"
                    placeholder="Enter your password"
                    required
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                />


              </div>
                <div className="form-meta">
                  <label className="remember">
                    <input
                        type="checkbox"
                        id="remember"
                        name="remember"
                        checked={remember}
                        onChange={(e) => setRemember(e.target.checked)}
                    />{' '}
                    Remember me
                  </label>
                  <a href="#">Forgot password?</a>
                </div>
              <div>
                <button className="login-submit" type="submit">
                  Login
                </button>
              </div>

            </div>

            {isAuthenticated && <p className="login-success">Login successful.</p>}
            {errorMessage && <p className="login-error">{errorMessage}</p>}
          </form>

        </div>
      </div>

    </browserRouter>
  );
}

export default LoginPage;
