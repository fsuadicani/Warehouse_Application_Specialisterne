import { useEffect, useState } from 'react';
import AddTransitModal from '../components/AddTransitModal.jsx';
import DataTable from '../components/DataTable.jsx';
import EditWarehouseModal from '../components/EditWarehouseModal.jsx';
import '../css/ui.css';
import { stocks } from '../testdata/tableTestData.js';
import WarehouseSelector from '../components/WarehouseSelector.jsx';

const WAREHOUSES_ENDPOINT = '/api/warehouse/filter?take=1000';

function WarehousePage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedStock, setSelectedStock] = useState(null);
  const [warehouseOptions, setWarehouseOptions] = useState([]);
  const stockColumns = [
    { key: 'name', label: 'Name' },
    { key: 'inhouseLocation', label: 'Inhouse Location' },
    { key: 'localPrice', label: 'Local Price' },
    { key: 'localCurrency', label: 'Local Currency' },
    { key: 'inStock', label: 'In Stock' },
  ];

  useEffect(() => {
    const abortController = new AbortController();

    const loadWarehouses = async () => {
      try {
        const response = await fetch(WAREHOUSES_ENDPOINT, {
          signal: abortController.signal,
        });

        if (!response.ok) {
          return;
        }

        const data = await response.json();

        if (!Array.isArray(data)) {
          return;
        }

        const uniqueCities = [...new Set(
          data
            .map((warehouse) => warehouse.city)
            .filter((city) => typeof city === 'string' && city.trim()),
        )];

        setWarehouseOptions(uniqueCities);
      } catch {
        if (!abortController.signal.aborted) {
          setWarehouseOptions([]);
        }
      }
    };

    loadWarehouses();

    return () => {
      abortController.abort();
    };
  }, []);


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
                <WarehouseSelector
                  label="Choose a Warehouse:"
                  className="page-selector-form"
                  options={warehouseOptions}
                  disabled={warehouseOptions.length === 0}
                />
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
