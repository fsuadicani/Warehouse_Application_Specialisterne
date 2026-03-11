function App() {
  return (
    <div className="App">
      <h1>Warehouse Storage App</h1>
      <form action="login-form" method="POST">

      <input type="text" id="username" name="username" placeholder="Enter your username" required />
      &nbsp;
      <input type="password" id="password" name="password" placeholder="Enter your password" required />

      <div className="form-actions">
        <label className="remember">
          <input type="checkbox" id="remember" name="remember" /> Remember me
        </label>
        <a href="#">Forgot password?</a>
      </div>

      <button type="submit">Login</button>
    </form>

    <p>Don’t have an account? <a href="#">Sign up</a></p>
    </div>
  );
}

export default App;