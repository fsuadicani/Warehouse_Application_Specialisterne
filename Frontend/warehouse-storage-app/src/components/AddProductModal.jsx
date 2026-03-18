import FormModal from './FormModal.jsx';

const productFields = [
  { name: 'name', label: 'Name' },
  { name: 'number', label: 'Number' },
  { name: 'defaultPrice', label: 'Default Price', validateAs: 'number' },
  { name: 'defaultCurrency', label: 'Default Currency' },
];

function AddProductModal({ onClose, title = 'New Product', submitLabel = 'Create', initialValues = {} }) {
  return (
    <FormModal
      onClose={onClose}
      title={title}
      fields={productFields}
      initialValues={initialValues}
      submitLabel={submitLabel}
      validateRequired
    />
  );
}

export default AddProductModal;
