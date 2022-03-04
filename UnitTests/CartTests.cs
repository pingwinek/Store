using Xunit;
using Moq;
using MVC.Models.IRepository;
using MVC.Models;
using System.Linq;
using MVC.Controllers;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using MVC.Models.ViewModels;
using System;

namespace UnitTests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductId = 1, Name = "P1"};
            Product p2 = new Product { ProductId = 2, Name = "P2"};

            // create new cart
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(p1, results[0].Product);
            Assert.Equal(p2, results[1].Product);
        }

        [Fact]
        public void Can_Add_Quantity_To_Existing_List()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductId = 1, Name = "P1"};
            Product p2 = new Product { ProductId = 2, Name = "P2"};

            // create new cart
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);
            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.Equal(2, results.Length);
            Assert.Equal(4, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductId = 1, Name = "P1"};
            Product p2 = new Product { ProductId = 2, Name = "P2"};

            // create new cart
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);

            target.RemoveLine(p1);
            CartLine[] results = target.Lines.ToArray();

            // Assert
            Assert.Equal(1, results.Length);
            Assert.Equal(p2, results[0].Product);
            Assert.Equal(1, results[0].Quantity);
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductId = 1, Name = "P1", Price = 3.0M };
            Product p2 = new Product { ProductId = 2, Name = "P2", Price = 6.0M };

            // create new cart
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);

            decimal sum = target.ComputeTotalValue();

            // Assert
            Assert.Equal(18.0M, sum);
        }

        [Fact]
        public void Can_Clear_Content()
        {
            // Arrange - create some test products
            Product p1 = new Product { ProductId = 1, Name = "P1", Price = 3.0M };
            Product p2 = new Product { ProductId = 2, Name = "P2", Price = 6.0M };

            // create new cart
            Cart target = new Cart();

            // Act
            target.AddItem(p1, 1);
            target.AddItem(p2, 1);
            target.AddItem(p1, 3);

            target.Clear();

            // Assert
            Assert.Empty(target.Lines);
        }
    }
}