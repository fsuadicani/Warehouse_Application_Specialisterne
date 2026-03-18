import { useState } from 'react';
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
  validateRequired = false,
}) {
  const [values, setValues] = useState(
    Object.fromEntries(fields.map((field) => [field.name, initialValues[field.name] ?? ''])),
  );
  const [errors, setErrors] = useState({});

  const handleChange = (fieldName, fieldValue) => {
    setValues((currentValues) => ({ ...currentValues, [fieldName]: fieldValue }));
    setErrors((currentErrors) => {
      if (!currentErrors[fieldName]) {
        return currentErrors;
      }

      const nextErrors = { ...currentErrors };
      delete nextErrors[fieldName];
      return nextErrors;
    });
  };

  const handleSubmit = () => {
    if (!validateRequired) {
      onClose();
      return;
    }

    const nextErrors = fields.reduce((collectedErrors, field) => {
      if (!String(values[field.name] ?? '').trim()) {
        collectedErrors[field.name] = true;
      }
      return collectedErrors;
    }, {});

    if (Object.keys(nextErrors).length > 0) {
      setErrors(nextErrors);
      return;
    }

    onClose();
  };

  return (
    <Modal
      onClose={onClose}
      className={modalClassName}
      overlayClassName={overlayClassName}
      closeLabel={closeLabel}
    >
      <div className="nested-modal-content">
        <h2>{title}</h2>
        {Object.keys(errors).length > 0 && (
          <p className="modal-error-message">Please fill out all required fields.</p>
        )}
        {fields.map((field) => (
          <label key={field.name} className="modal-field">
            <span>{field.label}</span>
            {field.type === 'select' ? (
              <select
                className={`modal-input${errors[field.name] ? ' modal-input-error' : ''}`}
                value={values[field.name] ?? field.options?.[0]?.value ?? ''}
                onChange={(event) => handleChange(field.name, event.target.value)}
              >
                {field.options?.map((option) => (
                  <option key={option.value} value={option.value}>
                    {option.label}
                  </option>
                ))}
              </select>
            ) : (
              <input
                type={field.type ?? 'text'}
                className={`modal-input${errors[field.name] ? ' modal-input-error' : ''}`}
                value={values[field.name] ?? ''}
                onChange={(event) => handleChange(field.name, event.target.value)}
              />
            )}
          </label>
        ))}
        <button type="button" className="modal-accept-button" onClick={handleSubmit}>
          {submitLabel}
        </button>
      </div>
    </Modal>
  );
}

export default FormModal;
