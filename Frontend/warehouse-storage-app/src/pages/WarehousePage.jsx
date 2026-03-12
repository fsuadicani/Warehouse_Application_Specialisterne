import '../css/warehouse.css';

function WarehousePage() {
  const warehouses = [
    { by: 'Odense', postnummer: '5000', gade: 'Flakhaven', husnummer: '2' },
    { by: 'Kobenhavn', postnummer: '1553', gade: 'Radhuspladsen', husnummer: '1' },
    { by: 'Aarhus', postnummer: '8000', gade: 'Banegardspladsen', husnummer: '1' },
  ];


  return (
    <div className="content">
      <h1>Varehuse</h1>

      <form>
        <a>
          Vælg en by:
        </a>
        <select className="citySelector">
          <option value="Odense">Odense</option>
          <option value="København">København</option>
          <option value="Århus">Århus</option>
        </select>
      </form>

      <div className="tablecontainer">
        <table>
          <thead>
            <tr>
              <th>By</th>
              <th>Postnummer</th>
              <th>Gade</th>
              <th>Husnummer</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {warehouses.map((warehouse) => (
              <tr key={`${warehouse.by}-${warehouse.postnummer}-${warehouse.gade}-${warehouse.husnummer}`}>
                <td>{warehouse.by}</td>
                <td>{warehouse.postnummer}</td>
                <td>{warehouse.gade}</td>
                <td>{warehouse.husnummer}</td>
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

export default WarehousePage;
