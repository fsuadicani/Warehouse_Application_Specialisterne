import { useState } from 'react';
import CitySelector from './CitySelector.jsx';
import DataTable from './DataTable.jsx';
import Modal from './Modal.jsx';
import TransitQuantityModal from './TransitQuantityModal.jsx';

function AddTransitModal({ onClose }) {
  const [selectedProduct, setSelectedProduct] = useState(null);
  const transitGroups = [
    {
      title: 'Start Warehouse',
      items: [
        { name: 'Skruetrakker', productNumber: 'PRD-1001', fromStock: 42, toStock: 0 },
        { name: 'Hammer', productNumber: 'PRD-1002', fromStock: 18, toStock: 0 },
        { name: 'Skruetrakker', productNumber: 'PRD-1001', fromStock: 42, toStock: 0 },
        { name: 'Hammer', productNumber: 'PRD-1002', fromStock: 18, toStock: 0 },
        { name: 'Skruetrakker', productNumber: 'PRD-1001', fromStock: 42, toStock: 0 },
        { name: 'Hammer', productNumber: 'PRD-1002', fromStock: 18, toStock: 0 },
        { name: 'Skruetrakker', productNumber: 'PRD-1001', fromStock: 42, toStock: 0 },
        { name: 'Hammer', productNumber: 'PRD-1002', fromStock: 18, toStock: 0 },
        { name: 'Skruetrakker', productNumber: 'PRD-1001', fromStock: 42, toStock: 0 },
        { name: 'Hammer', productNumber: 'PRD-1002', fromStock: 18, toStock: 0 },
        { name: 'Skruetrakker', productNumber: 'PRD-1001', fromStock: 42, toStock: 0 },
        { name: 'Hammer', productNumber: 'PRD-1002', fromStock: 18, toStock: 0 },
        { name: 'Skruetrakker', productNumber: 'PRD-1001', fromStock: 42, toStock: 0 },
        { name: 'Hammer', productNumber: 'PRD-1002', fromStock: 18, toStock: 0 },
        { name: 'Skruetrakker', productNumber: 'PRD-1001', fromStock: 42, toStock: 0 },
        { name: 'Hammer', productNumber: 'PRD-1002', fromStock: 18, toStock: 0 },
        { name: 'Skruetrakker', productNumber: 'PRD-1001', fromStock: 42, toStock: 0 },
        { name: 'Hammer', productNumber: 'PRD-1002', fromStock: 18, toStock: 0 },
        { name: 'Skruetrakker', productNumber: 'PRD-1001', fromStock: 42, toStock: 0 },
        { name: 'Hammer', productNumber: 'PRD-1002', fromStock: 18, toStock: 0 },
      ],
    },
    {
      title: 'In Transit',
      items: [
        { name: 'Boremaskine', productNumber: 'PRD-1003', inStock: 7 },
        { name: 'Sav', productNumber: 'PRD-1004', inStock: 11 },
      ],
    },
  ];
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
                      <CitySelector label={'From'} />
                  }
                </div>

                <div className="modal-transit-button-container-right">
                  {
                      (index === 0) &&
                      <CitySelector label={'To'} />
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
