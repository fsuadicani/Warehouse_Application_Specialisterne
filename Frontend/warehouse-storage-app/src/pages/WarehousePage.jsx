import '../css/warehouse.css';

function WarehousePage() {
  return (
    <div className="content">
      <h1>Varehuse</h1>

      <div className="tablecontainer">
        <table>
          <tbody>
            <tr>
              <th>By</th>
              <th>Postnummer</th>
              <th>Gade</th>
              <th>Husnummer</th>
              <th>Actions</th>
            </tr>
            <tr>
              <td>Odense</td>
              <td>5000</td>
              <td>Flakhaven</td>
              <td>2</td>
              <td>Action</td>
            </tr>
            <tr>
              <td>København</td>
              <td>1553</td>
              <td>Rådhuspladsen</td>
              <td>1</td>
              <td>Action</td>
            </tr>
            <tr>
              <td>Århus</td>
              <td>8000</td>
              <td>Banegårdspladsen</td>
              <td>1</td>
              <td>Action</td>
            </tr>
          </tbody>
        </table>
      </div>
    </div>
  );
}

export default WarehousePage;
