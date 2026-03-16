import '../css/warehouse.css';

function ProductsPage() {
  const products = [
    { name: 'Skruetrakker', number: 'PRD-1001', defaultPrice: '149.00', defaultCurrency: 'DKK' },
    { name: 'Hammer', number: 'PRD-1002', defaultPrice: '89.50', defaultCurrency: 'DKK' },
    { name: 'Boremaskine', number: 'PRD-1003', defaultPrice: '799.00', defaultCurrency: 'DKK' },
  ];

  return (
    <div className="content">
      <h1>Products</h1>

      <div className="tablecontainer">
        <table>
          <thead>
            <tr>
              <th>Name</th>
              <th>Number</th>
              <th>Default Price</th>
              <th>Default Currency</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            {products.map((product) => (
              <tr key={product.number}>
                <td>{product.name}</td>
                <td>{product.number}</td>
                <td>{product.defaultPrice}</td>
                <td>{product.defaultCurrency}</td>
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

export default ProductsPage;
