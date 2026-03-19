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
  onSubmit,
}) {
  const [values, setValues] = useState(
    Object.fromEntries(fields.map((field) => [field.name, initialValues[field.name] ?? ''])),
  );
  const [errors, setErrors] = useState({});
  const [submitError, setSubmitError] = useState('');
  const [isSubmitting, setIsSubmitting] = useState(false);

  const handleChange = (fieldName, fieldValue) => {
    setValues((currentValues) => ({ ...currentValues, [fieldName]: fieldValue }));
    setSubmitError('');
    setErrors((currentErrors) => {
      if (!currentErrors[fieldName]) {
        return currentErrors;
      }

      const nextErrors = { ...currentErrors };
      delete nextErrors[fieldName];
      return nextErrors;
    });
  };

  const handleSubmit = async () => {
    if (!validateRequired) {
      onClose();
      return;
    }

    const nextErrors = fields.reduce((collectedErrors, field) => {
      const fieldValue = String(values[field.name] ?? '').trim();

      if (!fieldValue) {
        collectedErrors[field.name] = 'This field is required.';
        return collectedErrors;
      }

      if (field.validateAs === 'number' && Number.isNaN(Number(fieldValue))) {
        collectedErrors[field.name] = 'This field must be a number.';
      }

      return collectedErrors;
    }, {});

    if (Object.keys(nextErrors).length > 0) {
      setErrors(nextErrors);
      return;
    }

    if (!onSubmit) {
      onClose();
      return;
    }

    try {
      setIsSubmitting(true);
      setSubmitError('');
      await onSubmit(values);
      onClose();
    } catch (error) {
      if (error instanceof Error) {
        setSubmitError(error.message);
      } else {
        setSubmitError('Unable to submit the form.');
      }
    } finally {
      setIsSubmitting(false);
    }
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
          <p className="modal-error-message">
            Please fill out all required fields and use numbers where required.
          </p>
        )}
        {submitError && <p className="modal-error-message">{submitError}</p>}
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
                inputMode={field.validateAs === 'number' ? 'decimal' : undefined}
                step={field.step}
                min={field.min}
              />
            )}
            {errors[field.name] && <span className="modal-field-error">{errors[field.name]}</span>}
          </label>
        ))}
        <button
          type="button"
          className="modal-accept-button"
          onClick={handleSubmit}
          disabled={isSubmitting}
        >
          {isSubmitting ? 'Saving...' : submitLabel}
        </button>
      </div>
    </Modal>
  );
}

export default FormModal;
