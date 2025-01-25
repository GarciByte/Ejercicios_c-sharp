using System.Data;
using System.Text;

namespace PruebaEbay
{
    internal class Product
    {
        public string Name { get; init; }
        public string Url { get; init; }
        public decimal Price { get; init; }

        public Product(string name, string url, decimal price)
        {
            Name = name;
            Url = url;
            Price = price;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Name: {Name}");
            stringBuilder.AppendLine($"Url: {Url}");
            stringBuilder.AppendLine($"Price: {Price}");

            return stringBuilder.ToString();
        }

    }
}
