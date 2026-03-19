import { NavLink, Outlet } from 'react-router-dom';
import './css/ui.css';

function MenuLayout() {
  return (
    <div className="grid-container">
      <div className="menu">
        <div>
          <NavLink to="/products">Products</NavLink>
          <NavLink to="/warehouses">Warehouses</NavLink>
          <NavLink to="/transits">Transits</NavLink>
          <NavLink to="/employees">Admin</NavLink>
        </div>
        <div>
          <NavLink to="/login">Log Out</NavLink>
        </div>
      </div>

      <div className="content-container">
        <Outlet />
      </div>
    </div>
  );
}

export default MenuLayout;
