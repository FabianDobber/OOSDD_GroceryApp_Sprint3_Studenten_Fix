using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Models;

namespace Grocery.Core.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly List<Product> products;
        public ProductRepository()
        {
            products = [
                new Product(1, "Melk", 300),
                new Product(2, "Kaas", 100),
                new Product(3, "Brood", 400),
                new Product(4, "Cornflakes", 0),
                new Product(5, "Eieren", 250),
                new Product(6, "Yoghurt", 180),
                new Product(7, "Boter", 120),
                new Product(8, "Kipfilet", 80),
                new Product(9, "Gehakt", 75),
                new Product(10, "Zalm", 50),
                new Product(11, "Appels", 500),
                new Product(12, "Bananen", 600),
                new Product(13, "Sinaasappels", 350),
                new Product(14, "Tomaten", 280),
                new Product(15, "Komkommer", 150),
                new Product(16, "Sla", 90),
                new Product(17, "Wortels", 220),
                new Product(18, "Uien", 300),
                new Product(19, "Aardappelen", 450),
                new Product(20, "Pasta", 380),
                new Product(21, "Rijst", 320),
                new Product(22, "Tomatensaus", 260),
                new Product(23, "Olijfolie", 110),
                new Product(24, "Zonnebloemolie", 130),
                new Product(25, "Azijn", 95),
                new Product(26, "Meel", 160),
                new Product(27, "Suiker", 210),
                new Product(28, "Zout", 300),
                new Product(29, "Peper", 280),
                new Product(30, "Koffiebonen", 140),
                new Product(31, "Filterkoffie", 120),
                new Product(32, "Thee", 190),
                new Product(33, "Sinaasappelsap", 170),
                new Product(34, "Appelsap", 165),
                new Product(35, "Cola", 290),
                new Product(36, "Mineraalwater", 400),
                new Product(37, "Bier", 230),
                new Product(38, "Wijn", 85),
                new Product(39, "Chips", 310),
                new Product(40, "Notenmix", 140),
                new Product(41, "Chocoladereep", 420),
                new Product(42, "Koekjes", 280),
                new Product(43, "Snoep", 330),
                new Product(44, "Hagelslag", 150),
                new Product(45, "Pindakaas", 130),
                new Product(46, "Jam", 160),
                new Product(47, "Ontbijtkoek", 110),
                new Product(48, "Crackers", 200),
                new Product(49, "Beschuit", 180),
                new Product(50, "Tonijn in blik", 240),
                new Product(51, "Soep in blik", 210),
                new Product(52, "Knäckebröd", 170),
                new Product(53, "Mosterd", 125),
                new Product(54, "Ketchup", 155),
                new Product(55, "Mayonaise", 145),
                new Product(56, "Wc-papier", 350),
                new Product(57, "Keukenrol", 250),
                new Product(58, "Wasmiddel", 90),
                new Product(59, "Wasverzachter", 80),
                new Product(60, "Afwasmiddel", 130),
                new Product(61, "Allesreiniger", 110),
                new Product(62, "Vuilniszakken", 280),
                new Product(63, "Aluminiumfolie", 100),
                new Product(64, "Bakpapier", 120),
                new Product(65, "Tandpasta", 190),
                new Product(66, "Tandenborstel", 220),
                new Product(67, "Shampoo", 140),
                new Product(68, "Douchegel", 160),
                new Product(69, "Zeep", 210),
                new Product(70, "Deodorant", 180),
                new Product(71, "Paprika", 190),
                new Product(72, "Courgette", 80),
                new Product(73, "Aubergine", 70),
                new Product(74, "Broccoli", 100),
                new Product(75, "Bloemkool", 90),
                new Product(76, "Champignons", 130),
                new Product(77, "Knoflook", 250),
                new Product(78, "Rode peper", 150),
                new Product(79, "Gember", 120),
                new Product(80, "Limoen", 180),
                new Product(81, "Avocado", 110),
                new Product(82, "Druiven", 160),
                new Product(83, "Aardbeien", 90),
                new Product(84, "Blauwe bessen", 110),
                new Product(85, "Frambozen", 80),
                new Product(86, "Kwark", 140),
                new Product(87, "Slagroom", 70),
                new Product(88, "Roomkaas", 95),
                new Product(89, "Geraspte kaas", 135),
                new Product(90, "Vleeswaren", 175),
                new Product(91, "Hummus", 85),
                new Product(92, "Olijven", 105),
                new Product(93, "Pesto", 95),
                new Product(94, "Pizza", 120),
                new Product(95, "Friet", 150),
                new Product(96, "IJs", 180),
                new Product(97, "Muesli", 130),
                new Product(98, "Honing", 100),
                new Product(99, "Stroop", 90),
            ];
        }
        public List<Product> GetAll()
        {
            return products;
        }

        public Product? Get(int id)
        {
            return products.FirstOrDefault(p => p.Id == id);
        }

        public Product Add(Product item)
        {
            throw new NotImplementedException();
        }

        public Product? Delete(Product item)
        {
            throw new NotImplementedException();
        }

        public Product? Update(Product item)
        {
            Product? product = products.FirstOrDefault(p => p.Id == item.Id);
            if (product == null) return null;
            product.Id = item.Id;
            return product;
        }
    }
}
