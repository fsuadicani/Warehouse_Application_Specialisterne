import FormModal from './FormModal.jsx';

const warehouseFields = [
  { name: 'name', label: 'Name' },
  { name: 'inhouseLocation', label: 'Inhouse Location' },
  { name: 'localPrice', label: 'Local Price' },
  { name: 'localCurrency', label: 'Local Currency' },
  { name: 'inStock', label: 'In Stock' },
];

function EditWarehouseModal({ onClose, initialValues = {} }) {
  return (
    <FormModal
      onClose={onClose}
      title="Edit Stock Item"
      fields={warehouseFields}
      initialValues={initialValues}
      submitLabel="Save"
      validateRequired
    />
  );
}

export default EditWarehouseModal;
