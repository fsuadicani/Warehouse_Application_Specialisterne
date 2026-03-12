import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './css/home.css'

function Home() {
    return (
        <div className="home">
        <a href="#" className="home-link">Home</a>
        <a href="#" className="home-link">About</a>
        <a href="#" className="home-link">Contact</a>
            <div className="home-container">
                <img src="https://static.vecteezy.com/system/resources/thumbnails/022/935/143/small/large-warehouse-for-storage-of-goods-racks-shelves-goods-background-generative-ai-photo.jpg" alt="Warehouse Storage" />
            </div>
        </div>
    );
}

export default Home;