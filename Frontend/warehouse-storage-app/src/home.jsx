import './css/home.css'
function Home() {
    return (
        <div className="home">
            <div className="home-nav">
            <ul>
                <li><a href="#home">Products</a></li>
                <li><a href="#news">Warehouses</a></li>
                <li><a href="#contact">Transits</a></li>
                <li className="logout-item"><button className="logout-button">Logout</button></li>
            </ul>
            </div>
            &nbsp;
            <div className="home-container">
                <img src="https://static.vecteezy.com/system/resources/thumbnails/022/935/143/small/large-warehouse-for-storage-of-goods-racks-shelves-goods-background-generative-ai-photo.jpg" alt="Warehouse Storage" />
            </div>
        </div>
    );
}

export default Home