using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Diagnostics;

namespace Grocery.Core.Services
{
    public class GroceryListItemsService : IGroceryListItemsService
    {
        private readonly IGroceryListItemsRepository _groceriesRepository;
        private readonly IProductRepository _productRepository;

        public GroceryListItemsService(IGroceryListItemsRepository groceriesRepository, IProductRepository productRepository)
        {
            _groceriesRepository = groceriesRepository;
            _productRepository = productRepository;
        }

        public List<GroceryListItem> GetAll()
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public List<GroceryListItem> GetAllOnGroceryListId(int groceryListId)
        {
            List<GroceryListItem> groceryListItems = _groceriesRepository.GetAll().Where(g => g.GroceryListId == groceryListId).ToList();
            FillService(groceryListItems);
            return groceryListItems;
        }

        public GroceryListItem? AddProductToList(int groceryListId, Product product)
        {
            if (product == null || product.Stock <= 0) return null;

            GroceryListItem item = new(0, groceryListId, product.Id, 1);
            var addedItem = _groceriesRepository.Add(item);

            if (addedItem != null)
            {
                product.Stock--;
                _productRepository.Update(product);
                addedItem.Product = product;
            }

            return addedItem;
        }

        public bool IncreaseItemQuantity(GroceryListItem item)
        {
            if (item == null) return false;

            var product = _productRepository.Get(item.ProductId);
            if (product != null && product.Stock > 0)
            {
                item.Amount++;
                product.Stock--;

                _groceriesRepository.Update(item);
                _productRepository.Update(product);
                return true;
            }
            return false;
        }

        public bool DecreaseItemQuantity(GroceryListItem item)
        {
            if (item == null) return false;

            item.Amount--;

            var product = _productRepository.Get(item.ProductId);
            if (product != null)
            {
                product.Stock++;
                _productRepository.Update(product);
            }

            if (item.Amount <= 0)
            {
                _groceriesRepository.Delete(item);
            }
            else
            {
                _groceriesRepository.Update(item);
            }
            return true;
        }


        public GroceryListItem Add(GroceryListItem item)
        {
            var addedItem = _groceriesRepository.Add(item);
            addedItem.Product = _productRepository.Get(addedItem.ProductId) ?? new(0, "None", 0);
            return addedItem;
        }

        public GroceryListItem? Delete(GroceryListItem item)
        {
            return _groceriesRepository.Delete(item);
        }

        public GroceryListItem? Get(int id)
        {
            throw new NotImplementedException();
        }

        public GroceryListItem? Update(GroceryListItem item)
        {
            return _groceriesRepository.Update(item);
        }

        private void FillService(List<GroceryListItem> groceryListItems)
        {
            foreach (GroceryListItem g in groceryListItems)
            {
                g.Product = _productRepository.Get(g.ProductId) ?? new(0, "", 0);
            }
        }
    }
}