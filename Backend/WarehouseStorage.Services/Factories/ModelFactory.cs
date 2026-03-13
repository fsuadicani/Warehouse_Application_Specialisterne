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
        

        public static Transit CreateTransit(TransitDTO transit)
        {
            var transitId = (Guid?)null;
            var deliveryStatus = Enum.TryParse<DeliveryStatus>(transit.DeliveryStatus, ignoreCase: true, out var parsed)
                ? parsed
                : DeliveryStatus.WAITING;

            return new Transit(
                DomainFactory.CreateTransitNumber(transit.TransitNumber),
                DomainFactory.CreatePickupCode(transit.PickUpCode),
                DomainFactory.CreateCoordinate(transit.GpsLocation),
                DomainFactory.CreateCompany(transit.Distributor),
                deliveryStatus,
                transitId);
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
                stockId);
        }

        public static Warehouse CreateWarehouse(WarehouseDTO warehouse)
        {
            var warehouseId = (Guid?)null;
            var addressId = (Guid?)null;

            var createdWarehouse = new Warehouse(warehouseId)
            {
                Address = new Address(
                    DomainFactory.CreateCity(warehouse.City),
                    DomainFactory.CreateStreetName(warehouse.Street),
                    DomainFactory.CreateStreetNumber(warehouse.StreetNumber),
                    DomainFactory.CreateZipCode(warehouse.ZipCode),
                    addressId)
            };

            return createdWarehouse;
        }
    }
}