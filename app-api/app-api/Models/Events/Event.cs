using app.Models;

namespace api.Models.Events {
    public class Event : BaseEntity {
        public DateTime From {get;set;}
        public DateTime To {get;set;}
        public string Name {get;set;} = string.Empty;
        public string Description { get; set; } = string.Empty;
        public Order? Order {get;set;}
    }

    public class Order : BaseEntity {
        public List<Product> Products { get; set; }= new();
        public required Address Address {get;set;}
        public required Category Category { get; set; }
    }

    public class Address : BaseEntity {
        public required City City {get;set;}
        public Region? Region {get;set;}

        public required Country Country {get;set;}
    }

    public class City : BaseEntity {
        public string Name {get;set;} = string.Empty;

        public required District District {get;set;}
        public string Code {get;set;} = string.Empty;
        public string PostalCode {get;set;} = string.Empty;
        public int Population {get;set;}
    }

    public class District : BaseEntity {
        public List<City> Cities {get;set;} = new List<City>();
        public required Region Region { get;set; }
        public string Name {get;set;} = string.Empty;
        public string Code {get;set;} = string.Empty;
    }

    public class Region : BaseEntity {
        public List<District> Districts {get;set;} = new List<District>();
        public required Country Country { get;set; }
        public string Name {get;set;} = string.Empty;
        public string Code {get;set;} = string.Empty;
    }

    public class Country : BaseEntity {
        public List<Region> Regions {get;set;} = new List<Region>();
        public string Name {get;set;} = string.Empty;
        public string Code {get;set;} = string.Empty;
    }

    public class Product : BaseEntity {
        public string Name { get; set; } = string.Empty;
        public required Category Category { get; set; }
    }

    public class Category : BaseEntity {
        public string Name { get; set; } = string.Empty;
        public virtual List<Product> Products { get; set; } = new List<Product>();
        public virtual List<Order> Orders { get; set; } = new List<Order>();
    }
}