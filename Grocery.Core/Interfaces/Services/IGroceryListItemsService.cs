using Grocery.Core.Models;

namespace Grocery.Core.Interfaces.Services
{
    public interface IGroceryListItemsService
    {
        List<GroceryListItem> GetAll();

        List<GroceryListItem> GetAllOnGroceryListId(int groceryListId);

        GroceryListItem Add(GroceryListItem item);

        GroceryListItem? Delete(GroceryListItem item);

        GroceryListItem? Get(int id);

        GroceryListItem? Update(GroceryListItem item);

        GroceryListItem? AddProductToList(int groceryListId, Product product);

        bool IncreaseItemQuantity(GroceryListItem item);

        bool DecreaseItemQuantity(GroceryListItem item);
    }
}