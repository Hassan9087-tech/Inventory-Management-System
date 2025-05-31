// Developer: Hassan Shahzada
// Reg No: MUST/FA24-BSE-032
// Assignment: OOP Assignment 01
// Purpose: Manage inventory using OOP principles in C#

using System;
using System.Collections.Generic;
using System.Linq;

// Base class for all types of products
class Product
{
    protected int id;
    protected string name;
    protected string category;
    protected double price;
    protected int stockQuantity;

    public Product(int id, string name, string category, double price, int stockQuantity)
    {
        this.id = id;
        this.name = name;
        this.category = category;
        this.price = price;
        this.stockQuantity = stockQuantity;
    }

    public virtual void DisplayInfo()
    {
        Console.WriteLine("ID: " + id);
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Category: " + category);
        Console.WriteLine("Price: " + price);
        Console.WriteLine("Stock: " + stockQuantity);
    }

    public string GetCategory()
    {
        return category;
    }

    public int GetId()
    {
        return id;
    }
}

class DiscountedProduct : Product
{
    private double discountRate;

    public DiscountedProduct(int id, string name, string category, double price, int stockQuantity, double discountRate)
        : base(id, name, category, price, stockQuantity)
    {
        this.discountRate = discountRate;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Discount Rate: " + discountRate + "%");
    }
}

class PerishableProduct : Product
{
    private string expiryDate;

    public PerishableProduct(int id, string name, string category, double price, int stockQuantity, string expiryDate)
        : base(id, name, category, price, stockQuantity)
    {
        this.expiryDate = expiryDate;
    }

    public override void DisplayInfo()
    {
        base.DisplayInfo();
        Console.WriteLine("Expiry Date: " + expiryDate);
    }
}

class Supplier
{
    private int id;
    private string name;
    private string contactInfo;
    private string location;

    public Supplier(int id, string name, string contactInfo, string location)
    {
        this.id = id;
        this.name = name;
        this.contactInfo = contactInfo;
        this.location = location;
    }

    public void DisplayInfo()
    {
        Console.WriteLine("Supplier ID: " + id);
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Contact Info: " + contactInfo);
        Console.WriteLine("Location: " + location);
    }

    public int GetId()
    {
        return id;
    }
}

class InventoryManagement
{
    private List<Product> products = new List<Product>();
    private List<Supplier> suppliers = new List<Supplier>();

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public void AddSupplier(Supplier supplier)
    {
        suppliers.Add(supplier);
    }

    public Product SearchProductById(int id)
    {
        return products.FirstOrDefault(p => p.GetId() == id);
    }

    public Supplier SearchSupplierById(int id)
    {
        return suppliers.FirstOrDefault(s => s.GetId() == id);
    }

    public List<Product> FilterProductsByCategory(string category)
    {
        return products.Where(p => p.GetCategory().ToLower() == category.ToLower()).ToList();
    }

    public void DisplayAllProducts()
    {
        Console.WriteLine("----- All Products -----");
        foreach (var product in products)
        {
            product.DisplayInfo();
            Console.WriteLine();
        }
    }

    public void DisplayAllSuppliers()
    {
        Console.WriteLine("----- All Suppliers -----");
        foreach (var supplier in suppliers)
        {
            supplier.DisplayInfo();
            Console.WriteLine();
        }
    }

    public void DisplayProductsByCategory(string category)
    {
        Console.WriteLine("----- Products in Category:" +category);
        var filtered = FilterProductsByCategory(category);
        if (filtered.Count == 0)
        {
            Console.WriteLine("No products found in this category.");
        }
        else
        {
            foreach (var product in filtered)
            {
                product.DisplayInfo();
                Console.WriteLine();
            }
        }
    }

    public List<Product> FilterByType(Type type)
    {
        return products.Where(p => p.GetType() == type).ToList();
    }
}

class Program
{
    static void Main()
    {
        InventoryManagement inventory = new InventoryManagement();

        inventory.AddProduct(new Product(1, "Laptop", "Electronics", 999.99, 50));
        inventory.AddProduct(new DiscountedProduct(2, "Smartphone", "Electronics", 699.99, 30, 10));
        inventory.AddProduct(new PerishableProduct(3, "Books", "Libraries", 2.99, 100, "2025-05-15"));

        inventory.AddSupplier(new Supplier(1, "Best Supplier Co.", "123-3389-98", "Pakistan"));
        inventory.AddSupplier(new Supplier(2, "Supplier Co.", "345-7777-09", "Pakistan"));
        inventory.AddSupplier(new Supplier(3, "Supplier.", "222-3456-12", "Pakistan"));

        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("=== Inventory Management Menu ===");
            Console.WriteLine("1. Add New Product");
            Console.WriteLine("2. Add New Supplier");
            Console.WriteLine("3. View All Products");
            Console.WriteLine("4. View Discounted or Perishable Products");
            Console.WriteLine("5. View All Suppliers");
            Console.WriteLine("6. Search Product by ID");
            Console.WriteLine("7. Filter Products by Category");
            Console.WriteLine("8. Exit");
            Console.Write("Select an option: ");

            string choice = Console.ReadLine();
            Console.Clear();

            switch (choice)
            {
                case "1":
                    Console.Write("Enter ID: ");
                    int pid = int.Parse(Console.ReadLine());
                    Console.Write("Enter Name: ");
                    string pname = Console.ReadLine();
                    Console.Write("Enter Category: ");
                    string pcategory = Console.ReadLine();
                    Console.Write("Enter Price: ");
                    double pprice = double.Parse(Console.ReadLine());
                    Console.Write("Enter Stock Quantity: ");
                    int pstock = int.Parse(Console.ReadLine());
                    Console.Write("Product Type (1-Regular, 2-Discounted, 3-Perishable): ");
                    string type = Console.ReadLine();

                    if (type == "2")
                    {
                        Console.Write("Enter Discount Rate: ");
                        double drate = double.Parse(Console.ReadLine());
                        inventory.AddProduct(new DiscountedProduct(pid, pname, pcategory, pprice, pstock, drate));
                    }
                    else if (type == "3")
                    {
                        Console.Write("Enter Expiry Date (YYYY-MM-DD): ");
                        string edate = Console.ReadLine();
                        inventory.AddProduct(new PerishableProduct(pid, pname, pcategory, pprice, pstock, edate));
                    }
                    else
                    {
                        inventory.AddProduct(new Product(pid, pname, pcategory, pprice, pstock));
                    }
                    Console.WriteLine("Product added successfully!");
                    break;

                case "2":
                    Console.Write("Enter Supplier ID: ");
                    int sid = int.Parse(Console.ReadLine());
                    Console.Write("Enter Name: ");
                    string sname = Console.ReadLine();
                    Console.Write("Enter Contact Info: ");
                    string contact = Console.ReadLine();
                    Console.Write("Enter Location: ");
                    string location = Console.ReadLine();
                    inventory.AddSupplier(new Supplier(sid, sname, contact, location));
                    Console.WriteLine("Supplier added successfully!");
                    break;

                case "3":
                    inventory.DisplayAllProducts();
                    break;

                case "4":
                    Console.WriteLine("--- Discounted Products ---");
                    foreach (var p in inventory.FilterByType(typeof(DiscountedProduct)))
                    {
                        p.DisplayInfo();
                        Console.WriteLine();
                    }
                    Console.WriteLine("--- Perishable Products ---");
                    foreach (var p in inventory.FilterByType(typeof(PerishableProduct)))
                    {
                        p.DisplayInfo();
                        Console.WriteLine();
                    }
                    break;

                case "5":
                    inventory.DisplayAllSuppliers();
                    break;

                case "6":
                    Console.Write("Enter Product ID to Search: ");
                    int searchId = int.Parse(Console.ReadLine());
                    var found = inventory.SearchProductById(searchId);
                    if (found != null)
                        found.DisplayInfo();
                    else
                        Console.WriteLine("Product not found.");
                    break;

                case "7":
                    Console.Write("Enter Category to Filter: ");
                    string cat = Console.ReadLine();
                    inventory.DisplayProductsByCategory(cat);
                    break;

                case "8":
                    running = false;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }

            if (running)
            {
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        Console.WriteLine("Program exited.");
    }
}
