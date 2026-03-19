import { useEffect, useState } from 'react';

const WAREHOUSES_ENDPOINT = '/api/warehouse/filter?take=1000';

function WarehouseSelector({
  label,
  className = '',
  options,
  disabled = false,
}) {
  const formClasses = ['modal-selector-form', className].filter(Boolean).join(' ');
  const [fetchedOptions, setFetchedOptions] = useState([]);

  useEffect(() => {
    if (Array.isArray(options)) {
      return undefined;
    }

    const abortController = new AbortController();

    const loadWarehouses = async () => {
      try {
        const response = await fetch(WAREHOUSES_ENDPOINT, {
          signal: abortController.signal,
        });

        if (!response.ok) {
          return;
        }

        const data = await response.json();

        if (!Array.isArray(data)) {
          return;
        }

        const uniqueCities = [...new Set(
          data
            .map((warehouse) => warehouse.city)
            .filter((city) => typeof city === 'string' && city.trim()),
        )];

        setFetchedOptions(uniqueCities);
      } catch {
        if (!abortController.signal.aborted) {
          setFetchedOptions([]);
        }
      }
    };

    loadWarehouses();

    return () => {
      abortController.abort();
    };
  }, [options]);

  const resolvedOptions = Array.isArray(options) ? options : fetchedOptions;

  return (
    <form className={formClasses}>
      <span>{label}</span>
      <select className="warehouseSelector" disabled={disabled || resolvedOptions.length === 0}>
        {resolvedOptions.map((option) => (
          <option key={option} value={option}>
            {option}
          </option>
        ))}
      </select>
    </form>
  );
}

export default WarehouseSelector;
