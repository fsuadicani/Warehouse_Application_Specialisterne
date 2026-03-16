import { useState } from 'react';
import AddEmployeeModal from '../components/AddEmployeeModal.jsx';
import DataTable from '../components/DataTable.jsx';
import '../css/ui.css';

function EmployeesPage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedEmployee, setSelectedEmployee] = useState(null);
  const employees = [
    { employeeId: 'EMP-001', mail: 'Jens@work.dk', role: 'Admin' },
    { employeeId: 'EMP-002', mail: 'Anna@work.dk', role: 'User' },
    { employeeId: 'EMP-003', mail: 'Mikkel@work.dk', role: 'User' },
  ];
  const employeeColumns = [
    { key: 'employeeId', label: 'Employee ID' },
    { key: 'mail', label: 'Mail' },
    { key: 'role', label: 'Role' },
  ];

  return (
    <div>
        <div className="header-container">
            <h1>Manage Employees</h1>
        </div>

        <div className="button-container">
            <div className="left-button-container">
            <button type="button" onClick={() => setIsModalOpen(true)}>
                New Employee
            </button>
            </div>
        </div>
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

      {isModalOpen && <AddEmployeeModal onClose={() => setIsModalOpen(false)} />}
      {selectedEmployee && (
        <AddEmployeeModal
          onClose={() => setSelectedEmployee(null)}
          title="Edit Employee"
          submitLabel="Save"
          initialValues={selectedEmployee}
        />
      )}
    </div>
  );
}

export default EmployeesPage;
