import FormModal from './FormModal.jsx';

const warehouseFields = [
  { name: 'city', label: 'City' },
  { name: 'zipCode', label: 'ZIP Code', validateAs: 'number' },
  { name: 'street', label: 'Street' },
  { name: 'number', label: 'Number', validateAs: 'number' },
];

function AddWarehouseModal({ onClose, title = 'New Warehouse', submitLabel = 'Create', initialValues = {} }) {
  return (
    <FormModal
      onClose={onClose}
      title={title}
      fields={warehouseFields}
      initialValues={initialValues}
      submitLabel={submitLabel}
      validateRequired
    />
  );
}

export default AddWarehouseModal;
