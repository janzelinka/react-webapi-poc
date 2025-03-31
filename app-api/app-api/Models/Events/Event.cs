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
        public State? State {get;set;}

        public required Country Country {get;set;}
    }

    public class City : BaseEntity {
        public string Name {get;set;} = string.Empty;
        public State? State {get;set;}
        public string Code {get;set;} = string.Empty;
        public string PostalCode {get;set;} = string.Empty;
        public int Population {get;set;}
    }

    public class State : BaseEntity {
        public List<City> Cities {get;set;} = new List<City>();
        public Country? Country { get;set; }
        public string Name {get;set;} = string.Empty;
        public string Code {get;set;} = string.Empty;
    }

    public class Country : BaseEntity {
        public List<State> States {get;set;} = new List<State>();
        public string Name {get;set;} = string.Empty;
        public string Code {get;set;} = string.Empty;
        // public string ImportCountryId {get;set;} = string.Empty;
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