import '../css/warehouse.css';

function EmployeesPage() {
  const employees = [
    { employeeId: 'EMP-001', firstName: 'Jens', lastName: 'Hansen', role: 'Admin' },
    { employeeId: 'EMP-002', firstName: 'Anna', lastName: 'Jensen', role: 'User' },
    { employeeId: 'EMP-003', firstName: 'Mikkel', lastName: 'Nielsen', role: 'User' },
  ];

  return (
    <div className="content">
      <h1>Manage Employees</h1>

      <div className="tablecontainer">
        <table>
          <thead>
            <tr>
              <th>Employee ID</th>
              <th>First Name</th>
              <th>Last Name</th>
              <th>Role</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {employees.map((employee) => (
              <tr key={employee.employeeId}>
                <td>{employee.employeeId}</td>
                <td>{employee.firstName}</td>
                <td>{employee.lastName}</td>
                <td>{employee.role}</td>
                <td className="row-actions">
                  <button type="button">Edit</button>
                  <button type="button">Delete</button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default EmployeesPage;
