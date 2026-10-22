using C__Advanced_session_02.ShopMaster;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

namespace C__Advanced_session_02;

public class Program
{
    static void Main(string[] args)
    {
        static List <Product> SearchProducts(List<Product> products , Func<Product, bool> filter)
        {
            return products.Where(filter).ToList();
        }
       
       static void PrintProducts(List<Product> products)
        {
            foreach(Product p in products)
            {
                Console.WriteLine($" Name: {p.Name}, Price: ${p.Price}, Stock: {p.Stock}");
            }
        }
        ProductCatalog productCatalog = new ProductCatalog();

        List<Product> electronics = SearchProducts(productCatalog.catalog, p => p.Category == "Electronics");
        Console.WriteLine("---------- Electroincs Products : ----------");
        PrintProducts(electronics);

        List<Product> cheapprice = SearchProducts(productCatalog.catalog, P => P.Price < 50);
        Console.WriteLine("---------- Under $50 : ----------");
        PrintProducts(cheapprice);

        List<Product> stock = SearchProducts(productCatalog.catalog, p => p.Stock > 0);
        Console.WriteLine("---------- In Stock : ----------");
        PrintProducts(stock);

        List<Product> cheaperclothing = SearchProducts(productCatalog.catalog, p => p.Price < 100 &&  p.Category== "Clothing");
        Console.WriteLine("---------- Clothing Under $100 : -----------");
        PrintProducts(cheaperclothing);

        static void PrintReport(List<Product> products, Action<Product> action)
        {
            foreach(Product p in products)
            {
                action(p);
            }
        }
        Console.WriteLine("---------- Short Report : ----------");
        PrintReport(productCatalog.catalog, p => Console.WriteLine($"{p.Name} - {p.Price}"));

        Console.WriteLine("---------- Detaild Report : ----------");
        PrintReport(productCatalog.catalog, p => Console.WriteLine($"[{p.Category}] {p.Name} | Price: ${p.Price} | Stock: {p.Stock}"));


        static List <T> Transformproducts <T>(List<Product> products, Func<Product , T> transfrom)
        {
            List<T> result = new List<T>();
            foreach(Product p in products)
            {
                result.Add(transfrom(p));
            }
            return result;
        }

        Console.WriteLine("---------- Summary List -----------");
        List<string> summary = Transformproducts (productCatalog.catalog, p => $"{p.Name} - {p.Price}");
        foreach(string item in summary)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("---------- Price Labels ----------");
        List<string> pricelabels = Transformproducts(productCatalog.catalog, p => p.Price > 100 ? "Expensive" : "Affordable");
        for(int i = 0; i < productCatalog.catalog.Count; i++)
        {
            Console.WriteLine($"{productCatalog.catalog[i].Name} : {pricelabels[i]} ");
        }

        List<Product> FilterProducts<Product>(List<Product> products, Predicate<Product> filter)
        {
            List<Product> result = new List<Product>();
            foreach(Product p in products)
            
                if (filter(p))
                  result.Add(p);

            return result;
        }
        Console.WriteLine("---------- Low Stock Alert ----------");
        List<Product> lowstock = FilterProducts(productCatalog.catalog, p => p.Stock < 20);
        foreach(Product p in lowstock)
        {
            Console.WriteLine($"[Low Stock] {p.Name} : only {p.Stock} left");
        }
    }
    
}
