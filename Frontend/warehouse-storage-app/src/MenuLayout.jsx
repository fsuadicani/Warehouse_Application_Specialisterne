import { NavLink, Outlet } from 'react-router-dom';
import './css/warehouse.css';

function MenuLayout() {
  return (
    <div className="grid-container">
      <div className="menu">
        <div>
          <NavLink to="/products">Products</NavLink>
          <NavLink to="/warehouses">Warehouses</NavLink>
          <NavLink to="/transits">Transits</NavLink>
          <NavLink to="/employees">Manage Employees</NavLink>
        </div>
        <div>
          <NavLink to="/login">Log Out</NavLink>
        </div>
      </div>

      <div className="content">
        <Outlet />
      </div>
    </div>
  );
}

export default MenuLayout;
