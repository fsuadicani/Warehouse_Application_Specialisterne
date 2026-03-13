import React from 'react';import './css/home.css'
import Graphs from './Graphs.jsx'

function Home({ onLogout }) {
    const handleLogout = () => {
        localStorage.removeItem('warehouseAuth');

        if (typeof onLogout === 'function') {
            onLogout();
        }

        window.location.hash = '#/login';
    };

    return (
        <div className="home">
            <div className="home-nav">
            <ul>
                <li><a href="#products">Products</a></li>
                <li><a href="#warehouses">Warehouses</a></li>
                <li><a href="#transits">Transits</a></li>
                <li className="logout-item"><button className="logout-button" onClick={handleLogout}>Logout</button></li>
            </ul>
            </div>
            &nbsp;
            <div className="home-container">
                <Graphs />
            </div>
        </div>
    );
}

export default Home