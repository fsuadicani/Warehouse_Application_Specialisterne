import { useState } from 'react';
import AddTransitModal from '../components/AddTransitModal.jsx';
import CitySelector from '../components/CitySelector.jsx';
import DataTable from '../components/DataTable.jsx';
import EditWarehouseModal from '../components/EditWarehouseModal.jsx';
import '../css/ui.css';

function WarehousePage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedStock, setSelectedStock] = useState(null);
  const stocks = [
    { name: 'Skruetrakker', inhouseLocation: 'A1-14', localPrice: '149.00', localCurrency: 'DKK', inStock: 42 },
    { name: 'Hammer', inhouseLocation: 'B2-07', localPrice: '89.50', localCurrency: 'DKK', inStock: 18 },
    { name: 'Boremaskine', inhouseLocation: 'C3-21', localPrice: '799.00', localCurrency: 'DKK', inStock: 7 },
  ];
  const stockColumns = [
    { key: 'name', label: 'Name' },
    { key: 'inhouseLocation', label: 'Inhouse Location' },
    { key: 'localPrice', label: 'Local Price' },
    { key: 'localCurrency', label: 'Local Currency' },
    { key: 'inStock', label: 'In Stock' },
  ];


  return (
    <div>
      <div className="header-container">
          <h1>Warehouse</h1>
      </div>

        <div className="button-container">

            <div className="left-button-container">
                <button type="button" onClick={() => setIsModalOpen(true)}>
                    New Transit
                </button>
            </div>

            <div className="right-button-container">
                <CitySelector label="Vælg en by:" className="page-selector-form" />
            </div>

        </div>


      <div className="tablecontainer">
        <DataTable
          columns={stockColumns}
          rows={stocks}
          getRowKey={(stock) => `${stock.name}-${stock.inhouseLocation}`}
          renderActions={(stock) => (
            <button type="button" onClick={() => setSelectedStock(stock)}>Edit</button>
          )}
        />
      </div>

      {isModalOpen && <AddTransitModal onClose={() => setIsModalOpen(false)} />}
      {selectedStock && (
        <EditWarehouseModal
          onClose={() => setSelectedStock(null)}
          initialValues={selectedStock}
        />
      )}
    </div>
  );
}

export default WarehousePage;
