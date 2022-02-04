using Xunit;
using Moq;
using MVC.Models.IRepository;
using MVC.Models;
using System.Linq;
using MVC.Controllers;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.ViewModels;

namespace UnitTests;

public class HomeControllerTests
{
    [Fact]
    public void Can_Use_Repository()
    {
        // Arrange
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns((new Product[] {
            new Product {ProductId = 1, Name = "P1"},
            new Product {ProductId = 2, Name = "P2"}
        }).AsQueryable<Product>());

        Mock<ILogger<HomeController>> mockLogger = new Mock<ILogger<HomeController>>();

        HomeController controller = new HomeController(mockLogger.Object, mock.Object);
        controller.PageSize = 3;

        // Act
        ProductsListViewModel result = (controller.Index() as ViewResult).Model as ProductsListViewModel;

        // Assert
        Product[] prodArray = result.Products.ToArray();

        Assert.True(prodArray.Length == 2);
        Assert.Equal("P1", prodArray[0].Name);
        Assert.Equal("P2", prodArray[1].Name);
    }

    [Fact]
    public void Can_Paginate() {

        // Arrange
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns((new Product[] {
            new Product {ProductId = 1, Name = "P1"},
            new Product {ProductId = 2, Name = "P2"},
            new Product {ProductId = 3, Name = "P3"},
            new Product {ProductId = 4, Name = "P4"},
            new Product {ProductId = 5, Name = "P5"}
        }).AsQueryable<Product>());

        Mock<ILogger<HomeController>> mockLogger = new Mock<ILogger<HomeController>>();

        HomeController controller = new HomeController(mockLogger.Object, mock.Object);
        controller.PageSize = 3;

        // Act
        ProductsListViewModel result = (controller.Index(2) as ViewResult).ViewData.Model as ProductsListViewModel;

        // Assert
        Product[] prodArray = result.Products.ToArray();

        Assert.True(prodArray.Length == 2);
        Assert.Equal("P4", prodArray[0].Name);
        Assert.Equal("P5", prodArray[1].Name);
    }
}