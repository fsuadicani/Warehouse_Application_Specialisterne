function WarehouseSelector({ label, className = '' }) {
  const formClasses = ['modal-selector-form', className].filter(Boolean).join(' ');

  return (
    <form className={formClasses}>
      <span>{label}</span>
      <select className="warehouseSelector">
        <option value="Odense">Odense</option>
        <option value="København">København</option>
        <option value="Århus">Århus</option>
      </select>
    </form>
  );
}

export default WarehouseSelector;
