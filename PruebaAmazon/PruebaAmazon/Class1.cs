
using System.Text;

namespace PruebaAmazon
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
            stringBuilder.AppendLine($"Price: {Price} EUR");

            return stringBuilder.ToString();
        }
    }
}
