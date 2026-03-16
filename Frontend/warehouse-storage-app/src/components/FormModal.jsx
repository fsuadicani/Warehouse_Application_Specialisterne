import Modal from './Modal.jsx';

function FormModal({
  onClose,
  title,
  fields,
  initialValues = {},
  submitLabel,
  modalClassName = 'modal',
  overlayClassName = '',
  closeLabel = 'Close modal',
}) {
  return (
    <Modal
      onClose={onClose}
      className={modalClassName}
      overlayClassName={overlayClassName}
      closeLabel={closeLabel}
    >
      <div className="nested-modal-content">
        <h2>{title}</h2>
        {fields.map((field) => (
          <label key={field.name} className="modal-field">
            <span>{field.label}</span>
            <input
              type={field.type ?? 'text'}
              className="modal-input"
              defaultValue={initialValues[field.name] ?? ''}
            />
          </label>
        ))}
        <button type="button" className="modal-accept-button">
          {submitLabel}
        </button>
      </div>
    </Modal>
  );
}

export default FormModal;
