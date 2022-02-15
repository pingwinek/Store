using Xunit;
using Moq;
using MVC.Models.IRepository;
using MVC.Models;
using System.Linq;
using MVC.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.ViewModels;

namespace UnitTests
{
    public class ProductControllerTests
    {
        [Fact]
        public void Can_Filter_Products()
        {
            // Arrange
            Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {ProductId = 1, Name = "P1", Category = "Cat1"},
                new Product {ProductId = 2, Name = "P2", Category = "Cat2"},
                new Product {ProductId = 3, Name = "P3", Category = "Cat1"},
                new Product {ProductId = 4, Name = "P4", Category = "Cat1"},
                new Product {ProductId = 5, Name = "P5", Category = "Cat3"}
            }).AsQueryable<Product>());

            Mock<ILogger<HomeController>> mockLogger = new Mock<ILogger<HomeController>>();

            HomeController controller = new HomeController(mockLogger.Object, mock.Object);
            controller.PageSize = 3;

            // Act
            Product[] result = ((controller.Index("Cat1", 1) as ViewResult).Model as ProductsListViewModel).Products.ToArray();

            // Assert
            Assert.Equal(3, result.Length);
            Assert.True(result[0].Name == "P1" && result[0].Category == "Cat1");
            Assert.True(result[1].Name == "P3" && result[0].Category == "Cat1");
            Assert.True(result[2].Name == "P4" && result[0].Category == "Cat1");
        }
    }
}