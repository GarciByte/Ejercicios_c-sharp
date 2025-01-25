using System.Diagnostics;
using Microsoft.Playwright;

namespace PruebaEbay
{
    internal class Program
    {
        public static async Task Main()
        {
            // Necesario para instalar los navegadores
            Microsoft.Playwright.Program.Main(["install"]);

            using IPlaywright playwright = await Playwright.CreateAsync();
            BrowserTypeLaunchOptions options = new BrowserTypeLaunchOptions()
            {
                Headless = false // Se indica falso para poder ver el navegador
            };

            await using IBrowser browser = await playwright.Chromium.LaunchAsync(options);
            await using IBrowserContext context = await browser.NewContextAsync();
            IPage page = await context.NewPageAsync();

            // Ir a la página de ebay
            await page.GotoAsync("https://www.ebay.es/");

            // Escribimos en la barra de búsqueda lo que queremos buscar
            IElementHandle searchInput = await page.QuerySelectorAsync("#gh-ac");
            await searchInput.FillAsync("Nvidia RTX 4060 TI");

            // Le damos al botón de buscar
            IElementHandle searchButton = await page.QuerySelectorAsync("#gh-btn");
            await searchButton.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            // Recorremos la lista de productos y recolectamos los datos
            List<Product> products = new List<Product>();
            IReadOnlyList<IElementHandle> productElements = await page.QuerySelectorAllAsync("ul li.s-item");

            foreach (IElementHandle productElement in productElements)
            {
                try
                {
                    Product product = await GetProductAsync(productElement);
                    products.Add(product);
                    Console.WriteLine(product);
                }
                catch(Exception ex) { }
            }

            // Con los datos recolectados, buscamos el producto más barato
            Product cheapest = products.MinBy(p => p.Price);
            Console.WriteLine($"La oferta más barata es: {cheapest}");

            // Abrimos el navegador con la oferta más barata
            ProcessStartInfo processInfo = new ProcessStartInfo()
            {
                FileName = cheapest.Url,
                UseShellExecute = true
            };
            Process.Start(processInfo);
        }

        private static async Task<Product> GetProductAsync(IElementHandle element)
        {
            IElementHandle priceElement = await element.QuerySelectorAsync(".s-item__price");
            string priceRaw = await priceElement.InnerTextAsync();
            priceRaw = priceRaw.Replace("EUR", "", StringComparison.OrdinalIgnoreCase);
            priceRaw = priceRaw.Trim();
            priceRaw = priceRaw.Replace(".", "");
            decimal price = decimal.Parse(priceRaw);

            IElementHandle nameElement = await element.QuerySelectorAsync(".s-item__title");
            string name = await nameElement.InnerTextAsync();

            IElementHandle urlElement = await element.QuerySelectorAsync("a");
            string url = await urlElement.GetAttributeAsync("href");

            return new Product(name, url, price);
        }
    }
}

