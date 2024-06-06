using System.ComponentModel;
using System.Security.Cryptography.X509Certificates;

namespace Testing_2;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void CreateProduct()
    {
        Product product = new(1, "Laptop", 1000, "Electrónica");
        Assert.Multiple(() =>
        {
            Assert.That(product.Id, Is.EqualTo(1));
            Assert.That(product.Name, Is.EqualTo("Laptop"));
            Assert.That(product.Price, Is.EqualTo(1000));
            Assert.That(product.Category, Is.EqualTo("Electrónica"));
        });
    }

    [Test]
    public void AddProduct()
    {
            ProductManager productManager = new ProductManager();
            Product product = new(1, "Laptop", 1000, "Electrónica");

            productManager.AddProduct(product);

            var foundProduct = productManager.FindProductById(1);
            Assert.That(foundProduct, Is.Not.Null);
            Assert.That(foundProduct.Name, Is.EqualTo("Laptop"));
    }

     [Test]
    public void FindProduct()
    {
            ProductManager productManager = new ProductManager();
            Product product1 = new(2, "Tablet", 800, "Electrónica");
            Product product2 = new(3, "Atún", 50, "Alimentos");

            productManager.AddProduct(product1);
            productManager.AddProduct(product2);

            var foundProduct = productManager.FindProductById(3);
            Assert.That(foundProduct, Is.Not.Null);
            Assert.That(foundProduct.Name, Is.EqualTo("Atún"));
            Assert.That(foundProduct.Id, Is.EqualTo(3));
            Assert.That(foundProduct.Price, Is.EqualTo(50));
            Assert.That(foundProduct.Category, Is.EqualTo("Alimentos"));
    }


     [Test]
    public void CalculateTotalPriceElectronica()
    {
        Product product = new(1, "Laptop", 1000, "Electrónica");

            double totalPrice = ProductManager.CalculateTotalPrice(product);

            Assert.That(totalPrice, Is.EqualTo(1100));
    }

    
     [Test]
    public void CalculateTotalPriceAlimentos()
    {
            Product product = new(3, "Atún", 50, "Alimentos");

            double totalPrice = ProductManager.CalculateTotalPrice(product);

            Assert.That(totalPrice, Is.EqualTo(52.5));     
    }

}

public class Product{
    public int id;
    public string name;
    public double price;
    public string category;

    public Product(int id, string name, double price, string category){
        this.id = id;
        this.name = name;
        
        if (price < 0) {
            throw new ArgumentException("El precio no puede ser negativo", nameof(price));
             } else {
                this.price = price;
             }
          if (category!="Electrónica" && category!="Alimentos") {
            throw new ArgumentException("La categoría no es válida", nameof(category));
             } else {
                 this.category = category;
             }                   
    }

     public int Id
        {
            get { return id; }
            set { id = value; }
        }
     public string Name
        {
            get { return name; }
            set { name = value; }
        }
    public double Price
        {
            get { return price; }
            set { price = value; }
        }
    public string Category
        {
            get { return category; }
            set { category = value; }
        }
    
}

public class ProductManager
{
    private readonly List<Product> products = new List<Product>();

    public void AddProduct(Product product)
    {
        products.Add(product);
    }

    public static double CalculateTotalPrice(Product product)
    {
        double taxRate;

        if (product.Category == "Electrónica")
        {
            taxRate = 0.1;
        }
        else if (product.Category == "Alimentos")
        {
            taxRate = 0.05;
        }
        else
        {
            throw new ArgumentException("Categoría no válida", nameof(product.Category));
        }

        return product.Price * (1 + taxRate);
    }
    public Product FindProductById(int id)
    {
        Product product = products.FirstOrDefault(p => p.Id == id);

        if (product != null)
        {
            Console.WriteLine($"**Producto encontrado:**");
            Console.WriteLine($"Id: {product.Id}");
            Console.WriteLine($"Nombre: {product.Name}");
            Console.WriteLine($"Precio: {product.Price}");
            Console.WriteLine($"Categoría: {product.Category}");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine($"No se encontró ningún producto con el ID: {id}");
        }

        return product;
    }
}

