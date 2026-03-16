import '../css/warehouse.css';

function WarehousePage() {
  const warehouses = [
    { name: 'Skruetrakker', inhouseLocation: 'A1-14', localPrice: '149.00', localCurrency: 'DKK', inStock: 42 },
    { name: 'Hammer', inhouseLocation: 'B2-07', localPrice: '89.50', localCurrency: 'DKK', inStock: 18 },
    { name: 'Boremaskine', inhouseLocation: 'C3-21', localPrice: '799.00', localCurrency: 'DKK', inStock: 7 },
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
              <th>Name</th>
              <th>Inhouse Location</th>
              <th>Local Price</th>
              <th>Local Currency</th>
              <th>In Stock</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {warehouses.map((warehouse) => (
              <tr key={`${warehouse.name}-${warehouse.inhouseLocation}`}>
                <td>{warehouse.name}</td>
                <td>{warehouse.inhouseLocation}</td>
                <td>{warehouse.localPrice}</td>
                <td>{warehouse.localCurrency}</td>
                <td>{warehouse.inStock}</td>
                <td className="row-actions">
                  <button type="button">Edit</button>
                  <button type="button">Order</button>
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
