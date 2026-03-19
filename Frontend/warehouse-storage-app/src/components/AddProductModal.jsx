import FormModal from './FormModal.jsx';

const productFields = [
  { name: 'name', label: 'Name' },
  { name: 'number', label: 'Number' },
  { name: 'defaultPrice', label: 'Default Price', type: 'number', validateAs: 'number', step: '0.01', min: '0.01' },
  { name: 'defaultCurrency', label: 'Default Currency' },
];

function AddProductModal({
  onClose,
  title = 'New Product',
  submitLabel = 'Create',
  initialValues = {},
  onSubmit,
}) {
  return (
    <FormModal
      onClose={onClose}
      title={title}
      fields={productFields}
      initialValues={initialValues}
      submitLabel={submitLabel}
      validateRequired
      onSubmit={onSubmit}
    />
  );
}

export default AddProductModal;
