using WarehouseStorage.Domain.DomainPrimitives;
using WarehouseStorage.Domain.Enums;
using WarehouseStorage.Domain.Models;
using WarehouseStorage.DTOs.DataTransferObjects;

namespace WarehouseStorage.Services.Factories
{
    
    public static class ModelFactory
    {

        public static Product[] CreateMultipleProducts(ProductDTO product, int quantity)
        {
            var currencyCode = string.IsNullOrWhiteSpace(product.DefaultCurrency) ? "USD" : product.DefaultCurrency;
            var defaultPrice = product.DefaultPrice <= 0 ? 1m : product.DefaultPrice;

            Product[] products = new Product[quantity];
            for (int i = 0; i < quantity; i++)
            {
                products[i] = new Product(
                    DomainFactory.CreateProductName(product.Name),
                    DomainFactory.CreateProductNumber(product.Number),
                    DomainFactory.CreatePrice(defaultPrice),
                    DomainFactory.CreateCurrency(currencyCode),
                    null);
            }
            return products;
        }       

        
        public static Product CreateProduct(ProductDTO product)
        {
            var productId = (Guid?)null;

            var currencyCode = string.IsNullOrWhiteSpace(product.DefaultCurrency) ? "USD" : product.DefaultCurrency;
            var defaultPrice = product.DefaultPrice <= 0 ? 1m : product.DefaultPrice;

            return new Product(
                DomainFactory.CreateProductName(product.Name),
                DomainFactory.CreateProductNumber(product.Number),
                DomainFactory.CreatePrice(defaultPrice),
                DomainFactory.CreateCurrency(currencyCode),
                productId);
        }

        public static ProductDTO CreateProductDTO(Product product)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name.value,
                Number = product.Number.value,
                DefaultPrice = product.DefaultPrice.value,
                DefaultCurrency = product.DefaultCurrency.value
            };
        }
        

        public static Transit CreateTransit(TransitDTO transit)
        {
            var transitId = (Guid?)null;
            var deliveryStatus = Enum.TryParse<DeliveryStatus>(transit.DeliveryStatus, ignoreCase: true, out var parsed)
                ? parsed
                : DeliveryStatus.CREATED;

            return new Transit(
                DomainFactory.CreateTransitNumber(transit.TransitNumber),
                DomainFactory.CreatePickupCode(transit.PickUpCode),
                DomainFactory.CreateCoordinate(transit.GpsLocation),
                DomainFactory.CreateCompany(transit.Distributor),
                deliveryStatus,
                transitId)
            {
                DestinationId = transit.DestinationId,
                Location = new Location()
                {
                    Address = null,
                    Stocks = new List<Stock>()
                }
            };
        }


        public static Stock CreateStock(StockDTO stock)
        {
            var stockId = (Guid?)null;
            var localPrice = stock.LocalPrice <= 0 ? 1m : stock.LocalPrice;
            var localCurrency = string.IsNullOrWhiteSpace(stock.LocalCurrency) ? "USD" : stock.LocalCurrency;

            return new Stock(
                DomainFactory.CreateStockLocation(stock.InHouseLocation),
                DomainFactory.CreateQuantity(stock.Quantity),
                DomainFactory.CreatePrice(localPrice),
                DomainFactory.CreateCurrency(localCurrency),
                stockId)
            {
                ProductId = stock.ProductId
            };
        }

        public static StockDTO CreateStockDTO(Stock stock)
        {
            return new StockDTO
            {
                InHouseLocation = stock.InHouseLocation.value,
                Quantity = stock.Quantity.value,
                LocalPrice = stock.LocalPrice.value,
                LocalCurrency = stock.LocalCurrency.value,
                ProductId = stock.ProductId
            };
        }

        public static Warehouse CreateWarehouse(WarehouseDTO warehouse)
        {
            var warehouseId = (Guid?)null;
            var addressId = (Guid?)null;

            var createdWarehouse = new Warehouse(warehouseId)
            {
                Location = new Location()
                {
                    Address = new Address(
                        DomainFactory.CreateCity(warehouse.City),
                        DomainFactory.CreateStreetName(warehouse.Street),
                        DomainFactory.CreateStreetNumber(warehouse.StreetNumber),
                        DomainFactory.CreateZipCode(warehouse.ZipCode),
                        addressId)
                    
                }
            };

            return createdWarehouse;
        }

        public static WarehouseDTO CreateWarehouseDTO(Warehouse warehouse)
        {
            return new WarehouseDTO
            {
                Id = warehouse.Id,
                City = warehouse.Location.Address.City.value,
                Street = warehouse.Location.Address.Street.value,
                StreetNumber = warehouse.Location.Address.StreetNumber.value,
                ZipCode = warehouse.Location.Address.ZipCode.value
            };
        }
    }
}