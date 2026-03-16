import Modal from './Modal.jsx';

function TransitQuantityModal({ onClose, productName }) {
  return (
    <Modal
      onClose={onClose}
      className="modal modal-small"
      overlayClassName="modal-overlay-front"
      closeLabel="Close quantity modal"
    >
      <div className="nested-modal-content">
        <h2>{productName}</h2>
        <input type="text" className="modal-input" />
        <button type="button" className="modal-accept-button">
          Accept
        </button>
      </div>
    </Modal>
  );
}

export default TransitQuantityModal;
