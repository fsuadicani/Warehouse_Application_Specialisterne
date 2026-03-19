import { useEffect, useState } from 'react';
import AddEmployeeModal from '../components/AddEmployeeModal.jsx';
import AddWarehouseModal from '../components/AddWarehouseModal.jsx';
import DataTable from '../components/DataTable.jsx';
import '../css/ui.css';
import { employees } from '../testdata/tableTestData.js';

const WAREHOUSES_ENDPOINT = '/api/warehouse/filter?take=1000';
const CREATE_WAREHOUSE_ENDPOINT = '/api/warehouse/add';

const getResponseErrorMessage = async (response, fallbackMessage) => {
  const contentType = response.headers.get('content-type') ?? '';

  try {
    if (contentType.includes('application/json')) {
      const body = await response.json();

      if (typeof body?.message === 'string' && body.message.trim()) {
        return body.message.trim();
      }
    }

    const text = await response.text();
    if (text.trim()) {
      return text.trim();
    }
  } catch {
    return fallbackMessage;
  }

  return fallbackMessage;
};

const toWarehousePayload = (warehouseLike) => ({
  city: warehouseLike.city.trim(),
  zipCode: warehouseLike.zipCode.trim(),
  street: warehouseLike.street.trim(),
  streetNumber: warehouseLike.streetNumber.trim(),
});

function AdminPage() {
  const [isEmployeeModalOpen, setIsEmployeeModalOpen] = useState(false);
  const [isWarehouseModalOpen, setIsWarehouseModalOpen] = useState(false);
  const [selectedEmployee, setSelectedEmployee] = useState(null);
  const [selectedWarehouse, setSelectedWarehouse] = useState(null);
  const [warehouses, setWarehouses] = useState([]);
  const [isLoadingWarehouses, setIsLoadingWarehouses] = useState(true);
  const [warehouseLoadError, setWarehouseLoadError] = useState('');
  const employeeColumns = [
    { key: 'employeeId', label: 'Employee ID' },
    { key: 'mail', label: 'Mail' },
    { key: 'role', label: 'Role' },
  ];
  const warehouseColumns = [
    { key: 'city', label: 'City' },
    { key: 'zipCode', label: 'ZIP Code' },
    { key: 'street', label: 'Street' },
    { key: 'streetNumber', label: 'Number' },
  ];

  useEffect(() => {
    const abortController = new AbortController();

    const loadWarehouses = async () => {
      setIsLoadingWarehouses(true);
      setWarehouseLoadError('');

      try {
        const response = await fetch(WAREHOUSES_ENDPOINT, {
          signal: abortController.signal,
        });

        if (!response.ok) {
          throw new Error(`Failed to load warehouses (${response.status} ${response.statusText}).`);
        }

        const data = await response.json();

        if (!Array.isArray(data)) {
          throw new Error('Received an invalid warehouse list from the server.');
        }

        setWarehouses(data);
      } catch (error) {
        if (abortController.signal.aborted) {
          return;
        }

        if (error instanceof TypeError) {
          setWarehouseLoadError('Could not reach the backend. Make sure the API is running.');
        } else if (error instanceof Error) {
          setWarehouseLoadError(error.message);
        } else {
          setWarehouseLoadError('Failed to load warehouses.');
        }
      } finally {
        if (!abortController.signal.aborted) {
          setIsLoadingWarehouses(false);
        }
      }
    };

    loadWarehouses();

    return () => {
      abortController.abort();
    };
  }, []);

  const handleCreateWarehouse = async (formValues) => {
    const response = await fetch(CREATE_WAREHOUSE_ENDPOINT, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(toWarehousePayload(formValues)),
    });

    if (!response.ok) {
      throw new Error(
        await getResponseErrorMessage(response, 'Failed to create the warehouse.'),
      );
    }

    const createdWarehouse = await response.json();

    setWarehouseLoadError('');
    setWarehouses((currentWarehouses) => [createdWarehouse, ...currentWarehouses]);
  };

  const handleUpdateWarehouse = async (formValues) => {
    const originalWarehouse = selectedWarehouse;

    const createResponse = await fetch(CREATE_WAREHOUSE_ENDPOINT, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(toWarehousePayload(formValues)),
    });

    if (!createResponse.ok) {
      throw new Error(
        await getResponseErrorMessage(createResponse, 'Failed to update the warehouse.'),
      );
    }

    const recreatedWarehouse = await createResponse.json();
    const deleteResponse = await fetch(`/api/warehouse/${originalWarehouse.id}`, {
      method: 'DELETE',
    });

    if (!deleteResponse.ok) {
      try {
        await fetch(`/api/warehouse/${recreatedWarehouse.id}`, {
          method: 'DELETE',
        });
      } catch {
        // Best-effort rollback of the recreated row.
      }

      throw new Error(
        await getResponseErrorMessage(deleteResponse, 'Failed to finalize the warehouse update.'),
      );
    }

    setWarehouses((currentWarehouses) => currentWarehouses.map((warehouse) => (
      warehouse.id === originalWarehouse.id ? recreatedWarehouse : warehouse
    )));
    setWarehouseLoadError('');
  };

  const handleDeleteWarehouse = async (warehouseToDelete) => {
    const response = await fetch(`/api/warehouse/${warehouseToDelete.id}`, {
      method: 'DELETE',
    });

    if (!response.ok) {
      throw new Error(
        await getResponseErrorMessage(response, 'Failed to delete the warehouse.'),
      );
    }

    setWarehouseLoadError('');
    setWarehouses((currentWarehouses) => currentWarehouses.filter((warehouse) => warehouse.id !== warehouseToDelete.id));

    if (selectedWarehouse?.id === warehouseToDelete.id) {
      setSelectedWarehouse(null);
    }
  };

  return (
    <div>
      <div className="header-container">
        <h1>Admin</h1>
      </div>

      <div className="button-container">
        <div className="left-button-container">
          <button type="button" onClick={() => setIsEmployeeModalOpen(true)}>
            New Employee
          </button>
        </div>
        <div className="right-button-container">
          <button type="button" onClick={() => setIsWarehouseModalOpen(true)}>
            New Warehouse
          </button>
        </div>
      </div>
      <div className="admin-table-grid">
        <div className="tablecontainer">
          <DataTable
            columns={employeeColumns}
            rows={employees}
            getRowKey={(employee) => employee.employeeId}
            renderActions={(employee) => (
              <>
                <button type="button" onClick={() => setSelectedEmployee(employee)}>Edit</button>
                <button type="button">Delete</button>
              </>
            )}
          />
        </div>
        <div className="tablecontainer">
          {isLoadingWarehouses && <p>Loading warehouses...</p>}
          {!isLoadingWarehouses && warehouseLoadError && <p className="login-error">{warehouseLoadError}</p>}
          {!isLoadingWarehouses && !warehouseLoadError && warehouses.length === 0 && <p>No warehouses found.</p>}
          {!isLoadingWarehouses && !warehouseLoadError && warehouses.length > 0 && (
            <DataTable
              columns={warehouseColumns}
              rows={warehouses}
              getRowKey={(warehouse) => warehouse.id ?? `${warehouse.city}-${warehouse.zipCode}-${warehouse.street}-${warehouse.streetNumber}`}
              renderActions={(warehouse) => (
                <>
                  <button type="button" onClick={() => setSelectedWarehouse(warehouse)}>Edit</button>
                  <button
                    type="button"
                    onClick={async () => {
                      try {
                        await handleDeleteWarehouse(warehouse);
                      } catch (error) {
                        setWarehouseLoadError(
                          error instanceof Error ? error.message : 'Failed to delete the warehouse.',
                        );
                      }
                    }}
                  >
                    Delete
                  </button>
                </>
              )}
            />
          )}
        </div>
      </div>

      {isEmployeeModalOpen && <AddEmployeeModal onClose={() => setIsEmployeeModalOpen(false)} />}
      {isWarehouseModalOpen && (
        <AddWarehouseModal
          onClose={() => setIsWarehouseModalOpen(false)}
          onSubmit={handleCreateWarehouse}
        />
      )}
      {selectedEmployee && (
        <AddEmployeeModal
          onClose={() => setSelectedEmployee(null)}
          title="Edit Employee"
          submitLabel="Save"
          initialValues={selectedEmployee}
        />
      )}
      {selectedWarehouse && (
        <AddWarehouseModal
          onClose={() => setSelectedWarehouse(null)}
          title="Edit Warehouse"
          submitLabel="Save"
          initialValues={selectedWarehouse}
          onSubmit={handleUpdateWarehouse}
        />
      )}
    </div>
  );
}

export default AdminPage;
