import { useState } from 'react';
import AddTransitModal from '../components/AddTransitModal.jsx';
import CitySelector from '../components/CitySelector.jsx';
import DataTable from '../components/DataTable.jsx';
import EditWarehouseModal from '../components/EditWarehouseModal.jsx';
import '../css/ui.css';

function WarehousePage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedWarehouse, setSelectedWarehouse] = useState(null);
  const warehouses = [
    { name: 'Skruetrakker', inhouseLocation: 'A1-14', localPrice: '149.00', localCurrency: 'DKK', inStock: 42 },
    { name: 'Hammer', inhouseLocation: 'B2-07', localPrice: '89.50', localCurrency: 'DKK', inStock: 18 },
    { name: 'Boremaskine', inhouseLocation: 'C3-21', localPrice: '799.00', localCurrency: 'DKK', inStock: 7 },
  ];
  const warehouseColumns = [
    { key: 'name', label: 'Name' },
    { key: 'inhouseLocation', label: 'Inhouse Location' },
    { key: 'localPrice', label: 'Local Price' },
    { key: 'localCurrency', label: 'Local Currency' },
    { key: 'inStock', label: 'In Stock' },
  ];


  return (
    <div className="content">
      <h1>Varehuse</h1>

      <CitySelector label="Vælg en by:" className="page-selector-form" />

      <button type="button" onClick={() => setIsModalOpen(true)}>
        New Transit
      </button>

      <div className="tablecontainer">
        <DataTable
          columns={warehouseColumns}
          rows={warehouses}
          getRowKey={(warehouse) => `${warehouse.name}-${warehouse.inhouseLocation}`}
          renderActions={(warehouse) => (
            <button type="button" onClick={() => setSelectedWarehouse(warehouse)}>Edit</button>
          )}
        />
      </div>

      {isModalOpen && <AddTransitModal onClose={() => setIsModalOpen(false)} />}
      {selectedWarehouse && (
        <EditWarehouseModal
          onClose={() => setSelectedWarehouse(null)}
          initialValues={selectedWarehouse}
        />
      )}
    </div>
  );
}

export default WarehousePage;
