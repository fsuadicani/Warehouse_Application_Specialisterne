import { StrictMode } from 'react';
import { createRoot } from 'react-dom/client';
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import './css/index.css';
import './css/home.css';
import MenuLayout from './MenuLayout.jsx';
import ProductsPage from './pages/ProductsPage.jsx';
import TransitsPage from './pages/TransitsPage.jsx';
import EmployeesPage from './pages/EmployeesPage.jsx';
import LogoutPage from './pages/LogoutPage.jsx';
import WarehousePage from './pages/WarehousePage.jsx';
import App from './App.jsx';

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route element={<MenuLayout />}>
          <Route path="/" element={<Navigate replace to="/warehouses" />} />
          <Route path="/products" element={<ProductsPage />} />
          <Route path="/warehouses" element={<WarehousePage />} />
          <Route path="/transits" element={<TransitsPage />} />
          <Route path="/employees" element={<EmployeesPage />} />
          <Route path="/login" element={<App />} />
          <Route path="/logout" element={<LogoutPage />} />
        </Route>
      </Routes>
    </BrowserRouter>
  </StrictMode>,
);
