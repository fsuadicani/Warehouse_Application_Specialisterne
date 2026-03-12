import { useState } from 'react';

function Login() {
  const [username, setUsername] = useState('');
  const [password, setPassword] = useState('');
  const [errorMessage, setErrorMessage] = useState('');

  const handleSubmit = (e) => {
    e.preventDefault();

    const normalizedUsername = username.trim().toLowerCase();
    const normalizedPassword = password.trim();

    if (normalizedUsername === 'admin' && normalizedPassword === '1234') {
      setErrorMessage('');
      return;
    }

    setErrorMessage('Invalid username or password');
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

      {errorMessage && <p className="login-error">{errorMessage}</p>}
    </form>
  );
}

export default Login;