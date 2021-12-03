using System;
using Moq;
using NUnit.Framework;
using System.Linq;
using System.Collections.Generic;
using cartapp;

namespace Test
{
  // Be sure to follow the instructions (not everyone does)!  Don't rush.
  // We are looking for a simple, elegant solution that solves the expressed problem, not more (many people over-complicate).
  // It needs to be test-driven so we need to see sufficient unit tests (many people under test) and a test-first approach.
  // All acceptance criteria need tests (many people do some but not all).
  // You can add more tests to improve the solution. EqualExperts greatly values TDD approach to dev work, so you can do extra things 
  // like write tests cases for some edge cases (time permitting :) ), eg. Validation of bad data, passing strings instead of float, negative values etc
  public class CartAppTests
  {
    private CartService cartService;

      [SetUp]
      public void Setup()
      {
        cartService = new CartService();
      }

      [Test]
      public void AddItemsToShoppingCart()
      {
        // arrange:
        // An empty shopping cart
        cartService.Empty();

        // And a product, Dove Soap with a unit price of 39.99
        var cartItem = GetCartItemMock("Dove Soap", 5, 39.99);
        
        // act:
        // The user adds 5 Dove Soaps to the shopping cart
        cartService.Add(cartItem);

        // assert:
        // The shopping cart should contain 5 Dove Soaps each with a unit price of 39.99
        // And the shopping cart’s total price should equal 199.95
        Assert.AreEqual(cartService.Items().Count, 1);
        Assert.AreEqual(cartService.Items()[0].ProductName, "Dove Soap");
        Assert.AreEqual(cartService.Items()[0].Quantity, 5);
        Assert.AreEqual(cartService.Items()[0].Price, 39.99);
        Assert.AreEqual(cartService.Total(), 199.95);
      }

      private static ICartItem GetCartItemMock(string productName, int quantity, double price)
      {
        var cartItemMock = new Mock<ICartItem>();
        cartItemMock.Setup(item => item.ProductName).Returns(productName);
        cartItemMock.Setup(item => item.Price).Returns(price);
        cartItemMock.Setup(item => item.Quantity).Returns(quantity);
        return cartItemMock.Object;
      }
  }
}