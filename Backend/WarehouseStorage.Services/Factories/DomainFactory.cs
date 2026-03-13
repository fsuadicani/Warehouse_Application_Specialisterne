using System.ComponentModel.DataAnnotations;
using System.Security.Authentication;
using WarehouseStorage.Domain.DomainPrimitives;


namespace WarehouseStorage.Services.Factories
{
    
    public class DomainFactory
    {   

        public static City CreateCity(string city)
        {
            return new City(city);
        }

        public static CompanyName CreateCompany(string name)
        {
            return new CompanyName(name);
        }

        public static Coordinates CreateCoordinate(string coordinates)
        {
            return new Coordinates(coordinates);
        }


        public static Currency CreateCurrency(string currency)
        {
            return new Currency(currency);
        }

        public static Name CreateName(string name)
        {
            return new Name(name);
        }

        public static PickUpCode CreatePickupCode(string pickupcode)
        {
            return new PickUpCode(pickupcode);
        }

        public static Price CreatePrice(decimal price)
        {
            return new Price(price);
        }

        public static ProductName CreateProductName(string productname)
        {
            return new ProductName(productname);
        }

        public static ProductNumber CreateProductNumber(string productnumber)
        {
            return new ProductNumber(productnumber);
        }

        public static Quantity CreateQuantity(int quantity)
        {
            return new Quantity(quantity);
        }

        public static StockLocation CreateStockLocation(string stocklocation)
        {
            return new StockLocation(stocklocation);
        }

        public static StreetName CreateStreetName(string streetname)
        {
            return new StreetName(streetname);
        }

        public static StreetNumber CreateStreetNumber(string streetnumber)
        {
            return new StreetNumber(streetnumber);
        }

        public static TransitNumber CreateTransitNumber(string transitnumber)
        {
            return new TransitNumber(transitnumber);
        }

        public static ZipCode CreateZipCode(string zipcode)
        {
            return new ZipCode(zipcode);
        }

    }
}