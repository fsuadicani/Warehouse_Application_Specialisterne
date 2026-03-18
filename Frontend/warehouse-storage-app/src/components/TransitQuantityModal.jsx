import FormModal from './FormModal.jsx';

const quantityFields = [
  { name: 'quantity', label: 'Quantity' },
];

function TransitQuantityModal({ onClose, productName }) {
  return (
    <FormModal
      onClose={onClose}
      title={productName}
      fields={quantityFields}
      submitLabel="Accept"
      modalClassName="modal modal-small"
      overlayClassName="modal-overlay-front"
      closeLabel="Close quantity modal"
      validateRequired
    />
  );
}

export default TransitQuantityModal;
