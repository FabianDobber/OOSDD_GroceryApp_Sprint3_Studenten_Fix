using NUnit.Framework;
using Moq;
using Grocery.Core.Models;
using Grocery.Core.Services;
using Grocery.Core.Interfaces.Repositories;

namespace TestCore
{
    public class GroceryListItemsServiceTests
    {
        private Mock<IGroceryListItemsRepository> _groceriesRepositoryMock;
        private Mock<IProductRepository> _productRepositoryMock;
        private GroceryListItemsService _sut;

        [SetUp]
        public void Setup()
        {
            _groceriesRepositoryMock = new Mock<IGroceryListItemsRepository>();
            _productRepositoryMock = new Mock<IProductRepository>();
            _sut = new GroceryListItemsService(_groceriesRepositoryMock.Object, _productRepositoryMock.Object);
        }

        [Test]
        public void IncreaseItemQuantity_WhenStockAvailable_IncreasesAmount_DecreasesStock_AndReturnsTrue()
        {
            var product = new Product(1, "TestProduct", 5);
            var item = new GroceryListItem(1, 1, product.Id, 2) { Product = product };

            _productRepositoryMock.Setup(r => r.Get(product.Id)).Returns(product);
            _productRepositoryMock.Setup(r => r.Update(It.IsAny<Product>()));
            _groceriesRepositoryMock.Setup(r => r.Update(It.IsAny<GroceryListItem>()));

            var result = _sut.IncreaseItemQuantity(item);

            Assert.That(result, Is.True);
            Assert.That(item.Amount, Is.EqualTo(3));
            Assert.That(product.Stock, Is.EqualTo(4));
            _groceriesRepositoryMock.Verify(r => r.Update(It.Is<GroceryListItem>(i => i.Amount == 3)), Times.Once);
            _productRepositoryMock.Verify(r => r.Update(It.Is<Product>(p => p.Stock == 4)), Times.Once);
        }

        [Test]
        public void IncreaseItemQuantity_WhenNoStock_ReturnsFalseAndDoesNotUpdate()
        {
            var product = new Product(1, "TestProduct", 0);
            var item = new GroceryListItem(1, 1, product.Id, 2) { Product = product };

            _productRepositoryMock.Setup(r => r.Get(product.Id)).Returns(product);

            var result = _sut.IncreaseItemQuantity(item);

            Assert.That(result, Is.False);
            Assert.That(item.Amount, Is.EqualTo(2));
            Assert.That(product.Stock, Is.EqualTo(0));
            _groceriesRepositoryMock.Verify(r => r.Update(It.IsAny<GroceryListItem>()), Times.Never);
            _productRepositoryMock.Verify(r => r.Update(It.IsAny<Product>()), Times.Never);
        }

        [Test]
        public void DecreaseItemQuantity_WhenAmountAboveZero_DecreasesAmount_IncreasesStock_AndReturnsTrue()
        {
            var product = new Product(1, "TestProduct", 5);
            var item = new GroceryListItem(1, 1, product.Id, 3) { Product = product };

            _productRepositoryMock.Setup(r => r.Get(product.Id)).Returns(product);
            _productRepositoryMock.Setup(r => r.Update(It.IsAny<Product>()));
            _groceriesRepositoryMock.Setup(r => r.Update(It.IsAny<GroceryListItem>()));

            var result = _sut.DecreaseItemQuantity(item);

            Assert.That(result, Is.True);
            Assert.That(item.Amount, Is.EqualTo(2));
            Assert.That(product.Stock, Is.EqualTo(6));
            _groceriesRepositoryMock.Verify(r => r.Update(It.Is<GroceryListItem>(i => i.Amount == 2)), Times.Once);
            _productRepositoryMock.Verify(r => r.Update(It.Is<Product>(p => p.Stock == 6)), Times.Once);
        }

        [Test]
        public void DecreaseItemQuantity_WhenAmountIsOne_DeletesItemAndIncreasesStock()
        {
            var product = new Product(1, "TestProduct", 5);
            var item = new GroceryListItem(1, 1, product.Id, 1) { Product = product };

            _productRepositoryMock.Setup(r => r.Get(product.Id)).Returns(product);
            _productRepositoryMock.Setup(r => r.Update(It.IsAny<Product>()));
            _groceriesRepositoryMock.Setup(r => r.Delete(It.IsAny<GroceryListItem>()));

            var result = _sut.DecreaseItemQuantity(item);

            Assert.That(result, Is.True);
            Assert.That(item.Amount, Is.EqualTo(0));
            Assert.That(product.Stock, Is.EqualTo(6));
            _groceriesRepositoryMock.Verify(r => r.Delete(It.Is<GroceryListItem>(i => i.Amount == 0)), Times.Once);
            _productRepositoryMock.Verify(r => r.Update(It.Is<Product>(p => p.Stock == 6)), Times.Once);
        }

        [Test]
        public void IncreaseItemQuantity_WhenItemIsNull_ReturnsFalse()
        {
            var result = _sut.IncreaseItemQuantity(null);

            Assert.That(result, Is.False);
            _groceriesRepositoryMock.Verify(r => r.Update(It.IsAny<GroceryListItem>()), Times.Never);
            _productRepositoryMock.Verify(r => r.Update(It.IsAny<Product>()), Times.Never);
        }

        [Test]
        public void DecreaseItemQuantity_WhenItemIsNull_ReturnsFalse()
        {
            var result = _sut.DecreaseItemQuantity(null);

            Assert.That(result, Is.False);
            _groceriesRepositoryMock.Verify(r => r.Update(It.IsAny<GroceryListItem>()), Times.Never);
            _productRepositoryMock.Verify(r => r.Update(It.IsAny<Product>()), Times.Never);
        }
    }
}
