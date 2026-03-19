import { useState } from 'react';
import AddEmployeeModal from '../components/AddEmployeeModal.jsx';
import AddWarehouseModal from '../components/AddWarehouseModal.jsx';
import DataTable from '../components/DataTable.jsx';
import '../css/ui.css';
import { employees, warehouses } from '../testdata/tableTestData.js';

function AdminPage() {
  const [isEmployeeModalOpen, setIsEmployeeModalOpen] = useState(false);
  const [isWarehouseModalOpen, setIsWarehouseModalOpen] = useState(false);
  const [selectedEmployee, setSelectedEmployee] = useState(null);
  const [selectedWarehouse, setSelectedWarehouse] = useState(null);
  const employeeColumns = [
    { key: 'employeeId', label: 'Employee ID' },
    { key: 'mail', label: 'Mail' },
    { key: 'role', label: 'Role' },
  ];
  const warehouseColumns = [
    { key: 'city', label: 'City' },
    { key: 'zipCode', label: 'ZIP Code' },
    { key: 'street', label: 'Street' },
    { key: 'number', label: 'Number' },
  ];

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
          <DataTable
            columns={warehouseColumns}
            rows={warehouses}
            getRowKey={(warehouse) => `${warehouse.city}-${warehouse.zipCode}-${warehouse.street}-${warehouse.number}`}
            renderActions={(warehouse) => (
              <>
                <button type="button" onClick={() => setSelectedWarehouse(warehouse)}>Edit</button>
                <button type="button">Delete</button>
              </>
            )}
          />
        </div>
      </div>

      {isEmployeeModalOpen && <AddEmployeeModal onClose={() => setIsEmployeeModalOpen(false)} />}
      {isWarehouseModalOpen && <AddWarehouseModal onClose={() => setIsWarehouseModalOpen(false)} />}
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
        />
      )}
    </div>
  );
}

export default AdminPage;
