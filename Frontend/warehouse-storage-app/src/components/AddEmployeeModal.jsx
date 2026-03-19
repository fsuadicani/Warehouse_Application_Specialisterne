import FormModal from './FormModal.jsx';

const employeeFields = [
  { name: 'employeeId', label: 'Employee ID' },
  { name: 'mail', label: 'Mail' },
  {
    name: 'role',
    label: 'Role',
    type: 'select',
    options: [
      { value: 'Admin', label: 'Admin' },
      { value: 'User', label: 'User' },
    ],
  },
];

function AddEmployeeModal({ onClose, title = 'New Employee', submitLabel = 'Create', initialValues = {} }) {
  const modalValues = { role: 'User', ...initialValues };

  return (
    <FormModal
      onClose={onClose}
      title={title}
      fields={employeeFields}
      initialValues={modalValues}
      submitLabel={submitLabel}
      validateRequired
    />
  );
}

export default AddEmployeeModal;
