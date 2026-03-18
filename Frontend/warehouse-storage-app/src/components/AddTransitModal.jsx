import { useState } from 'react';
import DataTable from './DataTable.jsx';
import Modal from './Modal.jsx';
import TransitQuantityModal from './TransitQuantityModal.jsx';
import { transitGroups } from '../testdata/tableTestData.js';
import WarehouseSelector from './WarehouseSelector.jsx';

function AddTransitModal({ onClose }) {
  const [selectedProduct, setSelectedProduct] = useState(null);
  const startColumns = [
    { key: 'name', label: 'Name' },
    { key: 'productNumber', label: 'Product Number' },
    { key: 'fromStock', label: 'From Stock' },
    { key: 'toStock', label: 'To Stock' },
  ];
  const transitColumns = [
    { key: 'name', label: 'Name' },
    { key: 'productNumber', label: 'Product Number' },
    { key: 'inStock', label: 'In Stock' },
  ];

  const renderTransitActions = (tableIndex, item) => {
    if (tableIndex === 0) {
      return (
        <button type="button" onClick={() => setSelectedProduct(item.name)}>
          {'>>'}
        </button>
      );
    }

    if (tableIndex === 1) {
      return <button type="button">{'<<'}</button>;
    }

    return null;
  };

  return (
    <Modal onClose={onClose} className="transit-modal">
      <div className="transit-modal-content">
        <div className="modal-table-grid">
          {transitGroups.map((group, index) => (
            <div key={group.title} className="modal-table-section">

              <div className="modal-transit-header-container">
                <h2>{group.title}</h2>
              </div>

              <div className="modal-transit-button-container">
                <div className="modal-transit-button-container-left">
                  {
                      (index === 0) &&
                      <WarehouseSelector label={'From'} />
                  }
                </div>

                <div className="modal-transit-button-container-right">
                  {
                      (index === 0) &&
                      <WarehouseSelector label={'To'} />
                  }
                </div>

              </div>

              <div>

              <div className="modal-table-scroll">
                <DataTable
                  columns={index === 0 ? startColumns : transitColumns}
                  rows={group.items}
                  getRowKey={(item, itemIndex) => `${group.title}-${item.productNumber}-${itemIndex}`}
                  renderActions={index === 0 || index === 1 ? (item) => renderTransitActions(index, item) : undefined}
                  actionPosition={index === 1 ? 'start' : 'end'}
                  actionHeader="Action"
                  tableClassName="modal-table"
                />
              </div>

              </div>

            </div>
          ))}
        </div>

        {selectedProduct && (
          <TransitQuantityModal
            productName={selectedProduct}
            onClose={() => setSelectedProduct(null)}
          />
        )}
      </div>
    </Modal>
  );
}

export default AddTransitModal;
