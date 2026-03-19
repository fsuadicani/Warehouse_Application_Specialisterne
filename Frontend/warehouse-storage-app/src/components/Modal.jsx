function Modal({
  onClose,
  children,
  className = 'modal',
  overlayClassName = '',
  closeLabel = 'Close modal',
}) {
  const overlayClasses = ['modal-overlay', overlayClassName].filter(Boolean).join(' ');

  return (
    <div className={overlayClasses} onClick={onClose}>
      <div className={className} onClick={(event) => event.stopPropagation()}>
        <button
          type="button"
          className="modal-close"
          aria-label={closeLabel}
          onClick={onClose}
        >
          x
        </button>
        {children}
      </div>
    </div>
  );
}

export default Modal;
