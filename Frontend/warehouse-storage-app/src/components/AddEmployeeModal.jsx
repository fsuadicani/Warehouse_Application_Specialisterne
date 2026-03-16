import FormModal from './FormModal.jsx';

const employeeFields = [
  { name: 'employeeId', label: 'Employee ID' },
  { name: 'mail', label: 'Mail' },
  { name: 'role', label: 'Role' },
];

function AddEmployeeModal({ onClose, title = 'New Employee', submitLabel = 'Create', initialValues = {} }) {
  return (
    <FormModal
      onClose={onClose}
      title={title}
      fields={employeeFields}
      initialValues={initialValues}
      submitLabel={submitLabel}
    />
  );
}

export default AddEmployeeModal;
