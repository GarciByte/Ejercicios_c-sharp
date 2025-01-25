using System.Diagnostics;
using Microsoft.Playwright;

namespace PruebaAmazon
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

            // Ir a la página de Amazon
            await page.GotoAsync("https://www.amazon.es/");

            // Esperamos a que cargue la página
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            // Aceptar condiciones
            IElementHandle? acceptButton = await page.QuerySelectorAsync("#sp-cc-accept");
            if (acceptButton != null) await acceptButton.ClickAsync();

            // Se intenta seleccionar la barra de búsqueda
            IElementHandle searchInput = await page.QuerySelectorAsync("#twotabsearchtextbox");

            if (searchInput == null)
            {
                // Si no la encuentra, prueba con otro selector
                searchInput = await page.QuerySelectorAsync("#nav-bb-search");
            }

            // Escribimos en la barra de búsqueda lo que queremos buscar
            await searchInput.FillAsync("Nvidia RTX 4070 Super");

            // Se intenta seleccionar el botón de buscar
            IElementHandle searchButton = await page.QuerySelectorAsync("#nav-search-submit-button");

            if (searchButton == null)
            {
                // Si no la encuentra, prueba con otro selector
                searchButton = await page.QuerySelectorAsync("#nav-bb-searchbar > form > input");
            }

            // Le damos al botón de buscar
            await searchButton.ClickAsync();
            await page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);

            // Lista de cadenas de texto que deseas buscar en los nombres de los productos
            List<string> searchFilters = new List<string> { "4070", "Super" };

            // Recorremos la lista de productos y recolectamos los datos
            List<Product> products = new List<Product>();
            IReadOnlyList<IElementHandle> productElements = await page.QuerySelectorAllAsync("div.s-main-slot div.s-result-item");

            foreach (IElementHandle productElement in productElements)
            {
                try
                {
                    // Pasamos la lista de filtros a la función GetProductAsync
                    Product product = await GetProductAsync(productElement, searchFilters);

                    // Solo agregamos productos válidos (que no sean null)
                    if (product != null)
                    {
                        products.Add(product);
                        Console.WriteLine(product);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error procesando un producto: {ex.Message}");
                }
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
            await Task.Delay(-1);
        }

        private static async Task<Product> GetProductAsync(IElementHandle element, List<string> filters)
        {
            // Inicializamos valores por defecto
            string name = "Producto sin nombre";
            string url = "#";
            decimal price = 0m;

            try
            {
                // Obtener el nombre del producto
                IElementHandle nameElement = await element.QuerySelectorAsync("h2 a span");
                if (nameElement != null)
                {
                    name = await nameElement.InnerTextAsync();
                }

                // Obtener la URL del producto
                IElementHandle urlElement = await element.QuerySelectorAsync("h2 a");
                if (urlElement != null)
                {
                    url = await urlElement.GetAttributeAsync("href");
                    // En Amazon, el enlace puede ser relativo, así que debemos convertirlo en absoluto
                    url = "https://www.amazon.es" + url;
                }

                // Obtener el precio del producto
                IElementHandle priceWholeElement = await element.QuerySelectorAsync(".a-price-whole");
                IElementHandle priceFractionElement = await element.QuerySelectorAsync(".a-price-fraction");

                if (priceWholeElement != null && priceFractionElement != null)
                {
                    string priceWhole = await priceWholeElement.InnerTextAsync();
                    string priceFraction = await priceFractionElement.InnerTextAsync();

                    // Limpiar los caracteres no numéricos (excepto el punto decimal)
                    priceWhole = priceWhole.Replace(".", "").Replace(",", "").Trim(); // Elimina separadores de miles
                    priceFraction = priceFraction.Trim(); // Solo espacios en blanco

                    // Concatenar parte entera y fraccionaria en formato decimal adecuado
                    string priceRaw = priceWhole + "." + priceFraction;

                    // Parsear el precio a decimal
                    price = decimal.Parse(priceRaw, System.Globalization.CultureInfo.InvariantCulture);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extrayendo datos del producto: {ex.Message}");
            }

            // Filtrar productos que no contienen todas las cadenas de filtro en su nombre
            if (name == "Producto sin nombre" || url == "#" || price == 0 ||
                !filters.All(f => name.Contains(f, StringComparison.OrdinalIgnoreCase)))
            {
                return null; // No retornamos productos inválidos o que no cumplen con los filtros
            }

            return new Product(name, url, price);
        }

    }
}