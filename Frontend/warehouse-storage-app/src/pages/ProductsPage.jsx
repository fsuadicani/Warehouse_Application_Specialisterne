import { useState } from 'react';
import AddProductModal from '../components/AddProductModal.jsx';
import DataTable from '../components/DataTable.jsx';
import '../css/ui.css';

function ProductsPage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState(null);
  const products = [
    { name: 'Skruetrakker', number: 'PRD-1001', defaultPrice: '149.00', defaultCurrency: 'DKK' },
    { name: 'Hammer', number: 'PRD-1002', defaultPrice: '89.50', defaultCurrency: 'DKK' },
    { name: 'Boremaskine', number: 'PRD-1003', defaultPrice: '799.00', defaultCurrency: 'DKK' },
  ];
  const productColumns = [
    { key: 'name', label: 'Name' },
    { key: 'number', label: 'Number' },
    { key: 'defaultPrice', label: 'Default Price' },
    { key: 'defaultCurrency', label: 'Default Currency' },
  ];

  return (
    <div>

        <div className="header-container">
            <h1>Products</h1>
        </div>

        <div className="button-container">
            <div className="left-button-container">
                <button type="button" onClick={() => setIsModalOpen(true)}>
                    New Product
                </button>
            </div>
        </div>

      <div className="tablecontainer">
        <DataTable
          columns={productColumns}
          rows={products}
          getRowKey={(product) => product.number}
          renderActions={(product) => (
            <>
              <button type="button" onClick={() => setSelectedProduct(product)}>Edit</button>
              <button type="button">Delete</button>
              <button type="button">Duplicate</button>
            </>
          )}
        />
      </div>

      {isModalOpen && <AddProductModal onClose={() => setIsModalOpen(false)} />}
      {selectedProduct && (
        <AddProductModal
          onClose={() => setSelectedProduct(null)}
          title="Edit Product"
          submitLabel="Save"
          initialValues={selectedProduct}
        />
      )}
    </div>
  );
}

export default ProductsPage;
