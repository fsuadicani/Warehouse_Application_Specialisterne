import { useEffect, useState } from 'react';
import AddProductModal from '../components/AddProductModal.jsx';
import DataTable from '../components/DataTable.jsx';
import '../css/ui.css';

const PRODUCTS_ENDPOINT = '/api/product/filter?take=1000';
const CREATE_PRODUCT_ENDPOINT = '/api/product/add';

const formatPrice = (value) => {
  const numericValue = Number(value);

  if (Number.isNaN(numericValue)) {
    return value;
  }

  return numericValue.toFixed(2);
};

const normalizeProduct = (product) => ({
  ...product,
  defaultPrice: formatPrice(product.defaultPrice),
});

const toProductPayload = (productLike) => ({
  name: productLike.name.trim(),
  number: productLike.number.trim(),
  defaultPrice: Number(productLike.defaultPrice),
  defaultCurrency: productLike.defaultCurrency.trim().toUpperCase(),
});

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

function ProductsPage() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [selectedProduct, setSelectedProduct] = useState(null);
  const [products, setProducts] = useState([]);
  const [isLoadingProducts, setIsLoadingProducts] = useState(true);
  const [productLoadError, setProductLoadError] = useState('');
  const productColumns = [
    { key: 'name', label: 'Name' },
    { key: 'number', label: 'Number' },
    { key: 'defaultPrice', label: 'Default Price' },
    { key: 'defaultCurrency', label: 'Default Currency' },
  ];

  useEffect(() => {
    const abortController = new AbortController();

    const loadProducts = async () => {
      setIsLoadingProducts(true);
      setProductLoadError('');

      try {
        const response = await fetch(PRODUCTS_ENDPOINT, {
          signal: abortController.signal,
        });

        if (!response.ok) {
          throw new Error(`Failed to load products (${response.status} ${response.statusText}).`);
        }

        const data = await response.json();

        if (!Array.isArray(data)) {
          throw new Error('Received an invalid product list from the server.');
        }

        setProducts(data.map(normalizeProduct));
      } catch (error) {
        if (abortController.signal.aborted) {
          return;
        }

        if (error instanceof TypeError) {
          setProductLoadError('Could not reach the backend. Make sure the API is running.');
        } else if (error instanceof Error) {
          setProductLoadError(error.message);
        } else {
          setProductLoadError('Failed to load products.');
        }
      } finally {
        if (!abortController.signal.aborted) {
          setIsLoadingProducts(false);
        }
      }
    };

    loadProducts();

    return () => {
      abortController.abort();
    };
  }, []);

  const handleCreateProduct = async (formValues) => {
    const response = await fetch(CREATE_PRODUCT_ENDPOINT, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(toProductPayload(formValues)),
    });

    if (!response.ok) {
      throw new Error(
        await getResponseErrorMessage(response, 'Failed to create the product.'),
      );
    }

    const createdProduct = await response.json();

    setProductLoadError('');
    setProducts((currentProducts) => [normalizeProduct(createdProduct), ...currentProducts]);
  };

  const handleDeleteProduct = async (productToDelete) => {
    const response = await fetch(`/api/product/delete/${productToDelete.id}`, {
      method: 'DELETE',
    });

    if (!response.ok) {
      throw new Error(
        await getResponseErrorMessage(response, 'Failed to delete the product.'),
      );
    }

    setProductLoadError('');
    setProducts((currentProducts) => currentProducts.filter((product) => product.id !== productToDelete.id));

    if (selectedProduct?.id === productToDelete.id) {
      setSelectedProduct(null);
    }
  };

  const handleDuplicateProduct = async (productToDuplicate) => {
    const response = await fetch(CREATE_PRODUCT_ENDPOINT, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(toProductPayload(productToDuplicate)),
    });

    if (!response.ok) {
      throw new Error(
        await getResponseErrorMessage(response, 'Failed to duplicate the product.'),
      );
    }

    const duplicatedProduct = await response.json();

    setProductLoadError('');
    setProducts((currentProducts) => [normalizeProduct(duplicatedProduct), ...currentProducts]);
  };

  const handleUpdateProduct = async (formValues) => {
    const originalProduct = selectedProduct;
    const createResponse = await fetch(CREATE_PRODUCT_ENDPOINT, {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(toProductPayload(formValues)),
    });

    if (!createResponse.ok) {
      throw new Error(
        await getResponseErrorMessage(createResponse, 'Failed to update the product.'),
      );
    }

    const recreatedProduct = normalizeProduct(await createResponse.json());
    const deleteResponse = await fetch(`/api/product/delete/${originalProduct.id}`, {
      method: 'DELETE',
    });

    if (!deleteResponse.ok) {
      try {
        await fetch(`/api/product/delete/${recreatedProduct.id}`, {
          method: 'DELETE',
        });
      } catch {
        // Best-effort rollback of the recreated row.
      }

      throw new Error(
        await getResponseErrorMessage(deleteResponse, 'Failed to finalize the product update.'),
      );
    }

    setProducts((currentProducts) => currentProducts.map((product) => (
      product.id === originalProduct.id ? recreatedProduct : product
    )));
    setProductLoadError('');
  };

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
        {isLoadingProducts && <p>Loading products...</p>}
        {!isLoadingProducts && productLoadError && <p className="login-error">{productLoadError}</p>}
        {!isLoadingProducts && !productLoadError && products.length === 0 && <p>No products found.</p>}
        {!isLoadingProducts && !productLoadError && products.length > 0 && (
        <DataTable
          columns={productColumns}
          rows={products}
          getRowKey={(product) => product.id ?? product.number}
          renderActions={(product) => (
            <>
              <button  type="button" onClick={() => setSelectedProduct(product)}>Edit</button>
              <button
                type="button"
                onClick={async () => {
                  try {
                    await handleDeleteProduct(product);
                  } catch (error) {
                    setProductLoadError(
                      error instanceof Error ? error.message : 'Failed to delete the product.',
                    );
                  }
                }}
              >
                Delete
              </button>
              <button
                type="button"
                onClick={async () => {
                  try {
                    await handleDuplicateProduct(product);
                  } catch (error) {
                    setProductLoadError(
                      error instanceof Error ? error.message : 'Failed to duplicate the product.',
                    );
                  }
                }}
              >
                Duplicate
              </button>
            </>
          )}
        />
        )}
      </div>

      {isModalOpen && (
        <AddProductModal
          onClose={() => setIsModalOpen(false)}
          onSubmit={handleCreateProduct}
        />
      )}
      {selectedProduct && (
        <AddProductModal
          onClose={() => setSelectedProduct(null)}
          title="Edit Product"
          submitLabel="Save"
          initialValues={selectedProduct}
          onSubmit={handleUpdateProduct}
        />
      )}
    </div>
  );
}

export default ProductsPage;
