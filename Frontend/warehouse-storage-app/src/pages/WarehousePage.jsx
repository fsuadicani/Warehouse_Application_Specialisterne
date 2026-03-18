import { useState } from 'react';
import AddTransitModal from '../components/AddTransitModal.jsx';
import DataTable from '../components/DataTable.jsx';
import EditWarehouseModal from '../components/EditWarehouseModal.jsx';
import '../css/ui.css';
import { stocks } from '../testdata/tableTestData.js';
import WarehouseSelector from '../components/WarehouseSelector.jsx';

function WarehousePage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedStock, setSelectedStock] = useState(null);
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
                <WarehouseSelector label="Choose a Warehouse:" className="page-selector-form" />
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
