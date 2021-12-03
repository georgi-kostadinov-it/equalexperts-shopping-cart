using System;
using NUnit.Framework;
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
      public void AddFiveDoveSoaps()
      {
        // arrange:
        // An empty shopping cart
        cartService.Empty();

        // And a product, Dove Soap with a unit price of 39.99
        var cartItem = new CartItem("Dove Soap", 5, 39.99);
        
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

      [Test]
      public void EmptyCart()
      {
        // arrange:
        
        // act:
        cartService.Empty();

        // assert:
        // After the shopping cart is emptied it will contain no items
        Assert.AreEqual(cartService.Items().Count, 0);
      }

      [Test]
      public void CartItemValidation()
      {
        // arrange:
 
        // act:

        // assert:
        // Cannot create Cart Items with empty Product Name
        var ex1 = Assert.Throws<Exception>(() => new CartItem("", 0, 0.0));
        Assert.That(ex1.Message, Is.EqualTo("Cart Item Product Name cannot be empty!"));

        // Cannot create Cart Items with negative or zero Quantity
        var ex2 = Assert.Throws<Exception>(() => new CartItem("N.A.", -1, 0.0));
        Assert.That(ex2.Message, Is.EqualTo("Cart Item Quantity must be a positive number greater than zero!"));
        var ex3 = Assert.Throws<Exception>(() => new CartItem("N.A.", 0, 0.0));
        Assert.That(ex3.Message, Is.EqualTo("Cart Item Quantity must be a positive number greater than zero!"));

        // Cannot create Cart Items with negative Price
        var ex4 = Assert.Throws<Exception>(() => new CartItem("N.A.", 1, -12.34));
        Assert.That(ex4.Message, Is.EqualTo("Cart Item Price cannot be negative!"));
        Assert.DoesNotThrow(() => new CartItem("A Free-be.", 1, 0.0));
      }

      [Test]
      public void ConsequitiveItemAdditions()
      {
        // arrange:
        // An empty shopping cart
        cartService.Empty();
        var cartItem1 = new CartItem("Dove Soap", 5, 39.99);
        var cartItem2 = new CartItem("Dove Soap", 3, 39.99);
        
        // act:
        // The user adds 5 Dove Soaps to the shopping cart
        // And then adds another 3 Dove Soaps to the shopping cart
        cartService.Add(cartItem1);
        cartService.Add(cartItem2);

        // assert:
        // The shopping cart should contain 8 Dove Soaps each with a unit price of 39.99
        // And the shopping cart’s total price should equal 319.92
        Assert.AreEqual(cartService.Items().Count, 1); // one unique item
        Assert.AreEqual(cartService.Items()[0].ProductName, "Dove Soap");
        Assert.AreEqual(cartService.Items()[0].Quantity, 8);
        Assert.AreEqual(cartService.Items()[0].Price, 39.99);
        Assert.AreEqual(cartService.Total(), 319.92);
      }
  }
}