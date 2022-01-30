using Xunit;
using Moq;
using MVC.Models.IRepository;
using MVC.Models;
using System.Linq;
using MVC.Controllers;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace UnitTests;

public class ProductControllerTests
{
    private readonly ILogger<HomeController> _mockLogger;

    public ProductControllerTests(ILogger<HomeController> logger)
    {
        _mockLogger = logger;
    }
    [Fact]
    public void Can_Use_Repository()
    {
        // Arrange
        Mock<IStoreRepository> mock = new Mock<IStoreRepository>();
        mock.Setup(m => m.Products).Returns((new Product[] {
            new Product {ProductId = 1, Name = "P1"},
            new Product {ProductId = 2, Name = "P2"}
        }).AsQueryable<Product>());

        HomeController controller = new HomeController(_mockLogger, mock.Object);

        // Act
        IEnumerable<Product> result = (controller.Index() as ViewResult).ViewData.Model as IEnumerable<Product>;

        // Assert
        Product[] prodArray = result.ToArray();
        Assert.True(prodArray.Length == 2);
        Assert.Equal("P1", prodArray[0].Name);
        Assert.Equal("P2", prodArray[1].Name);
    }
}