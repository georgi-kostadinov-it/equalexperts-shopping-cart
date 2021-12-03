using System;
using NUnit.Framework;
using cartapp;
using CartAppUtils = cartapp.Utils;

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
      public void UtilsTests()
      {
        // arrange:
        
        // act:

        // assert:
        Assert.AreEqual(CartAppUtils.GetThousandth(34.995000000002m), 5);
        Assert.AreEqual(CartAppUtils.GetThousandth(34.9940002m), 4);
        Assert.AreEqual(CartAppUtils.GetThousandth(34.990000000000m), 0);
        Assert.AreEqual(CartAppUtils.GetThousandth(34.99m), 0);
        Assert.AreEqual(CartAppUtils.GetThousandth(34m), 0);
        Assert.True(CartAppUtils.HasThousandth(34.9950m));
        Assert.False(CartAppUtils.HasThousandth(34.99m));
        Assert.AreEqual(CartAppUtils.RoundToTheNearestHundredth(34.995000000002m), 35.00m);
        Assert.AreEqual(CartAppUtils.RoundToTheNearestHundredth(34.9940002m), 34.99m);
        Assert.AreEqual(CartAppUtils.RoundToTheNearestHundredth(98761.00m), 98761.00m);
        Assert.AreEqual(CartAppUtils.RoundToTheNearestHundredth(123456789.99m), 123456789.99m);
        Assert.AreEqual(CartAppUtils.RoundToTheNearestHundredth(123456789.00000000000000001m), 123456789.00m);
        Assert.AreEqual(CartAppUtils.RoundToTheNearestHundredth(18446744073709551.614m), 18446744073709551.61m);
        Assert.AreEqual(CartAppUtils.RoundToTheNearestHundredth(-18446744073709551.614m), -18446744073709551.61m);

        var ex1 = Assert.Throws<Exception>(() => CartAppUtils.RoundToTheNearestHundredth(123456789123123123.00m));
        Assert.That(ex1.Message, Is.EqualTo("Exceeded the maximum number ("+ulong.MaxValue+") supported!"));
      }

      [Test]
      public void AddFiveDoveSoaps()
      {
        // arrange:
        // An empty shopping cart
        cartService.Empty();

        // And a product, Dove Soap with a unit price of 39.99
        var cartItem = new CartItem("Dove Soap", 5, 39.99m);
        
        // act:
        // The user adds 5 Dove Soaps to the shopping cart
        cartService.Add(cartItem);

        // assert:
        // The shopping cart should contain 5 Dove Soaps each with a unit price of 39.99
        // And the shopping cart’s total price should equal 199.95
        Assert.AreEqual(cartService.Items().Count, 1);
        Assert.AreEqual(cartService.Items()[0].ProductName, "Dove Soap");
        Assert.AreEqual(cartService.Items()[0].Quantity, 5);
        Assert.AreEqual(cartService.Items()[0].Price, 39.99m);
        Assert.AreEqual(cartService.TotalPrice(), 199.95m);
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
      public void SalesTaxValidation()
      {
        // arrange:
        
        // act:
        
        // assert:
        // Sales Tax cannot be negative numbers!
        var ex1 = Assert.Throws<Exception>(() => cartService.SalesTax = -12.3456m);
        Assert.That(ex1.Message, Is.EqualTo("The Sales Tax Percent cannot be negative!"));
        Assert.DoesNotThrow(() => cartService.SalesTax = 0.0m);
      }

      [Test]
      public void CartItemValidation()
      {
        // arrange:
 
        // act:

        // assert:
        // Cannot create Cart Items with empty Product Name
        var ex1 = Assert.Throws<Exception>(() => new CartItem("", 0, 0.0m));
        Assert.That(ex1.Message, Is.EqualTo("Cart Item Product Name cannot be empty!"));

        // Cannot create Cart Items with negative or zero Quantity
        var ex2 = Assert.Throws<Exception>(() => new CartItem("N.A.", -1, 0.0m));
        Assert.That(ex2.Message, Is.EqualTo("Cart Item Quantity must be a positive number greater than zero!"));
        var ex3 = Assert.Throws<Exception>(() => new CartItem("N.A.", 0, 0.0m));
        Assert.That(ex3.Message, Is.EqualTo("Cart Item Quantity must be a positive number greater than zero!"));

        // Cannot create Cart Items with negative Price
        var ex4 = Assert.Throws<Exception>(() => new CartItem("N.A.", 1, -12.34m));
        Assert.That(ex4.Message, Is.EqualTo("Cart Item Price cannot be negative!"));

        // Cannot create Cart Items with a Price that has thousandths
        var ex5 = Assert.Throws<Exception>(() => new CartItem("N.A.", 1, 12.345678m));
        Assert.That(ex5.Message, Is.EqualTo("Cart Item Price cannot have thousandths!"));

        // A Cart Item can have a zero Price (a free-be)
        Assert.DoesNotThrow(() => new CartItem("A Free-be.", 1, 0.0m));
      }

      [Test]
      public void ConsequitiveItemAdditions()
      {
        // arrange:
        // An empty shopping cart
        cartService.Empty();
        var cartItem1 = new CartItem("Dove Soap", 5, 39.99m);
        var cartItem2 = new CartItem("Dove Soap", 3, 39.99m);
        
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
        Assert.AreEqual(cartService.Items()[0].Price, 39.99m);
        Assert.AreEqual(cartService.TotalPrice(), 319.92m);
      }

      [Test]
      public void CalcTaxRateMultipleItems()
      {
        // arrange:
        // An empty shopping cart
        // And a product, Dove Soap with a unit price of 39.99
        // And another product, Axe Deo with a unit price of 99.99
        // And a sales tax rate of 12.5%
        cartService.Empty();
        cartService.SalesTax = 12.5m;
        var cartItem1 = new CartItem("Dove Soap", 2, 39.99m);
        var cartItem2 = new CartItem("Axe Deos", 2, 99.99m);
        
        // act:
        // The user adds 2 Dove Soaps to the shopping cart
        // And then adds 2 Axe Deos to the shopping cart
        cartService.Add(cartItem1);
        cartService.Add(cartItem2);

        // assert:
        // The shopping cart should contain 2 Dove Soaps each with a unit price of 39.99
        // And the shopping cart should contain 2 Axe Deos each with a unit price of 99.99
        // And the total sales tax amount for the shopping cart should equal 35.00
        // And the shopping cart’s total price should equal 314.96
        Assert.AreEqual(cartService.Items().Count, 2); // two unique items
        Assert.AreEqual(cartService.Items()[0].ProductName, "Dove Soap");
        Assert.AreEqual(cartService.Items()[0].Quantity, 2);
        Assert.AreEqual(cartService.Items()[0].Price, 39.99m);
        Assert.AreEqual(cartService.Items()[1].ProductName, "Axe Deos");
        Assert.AreEqual(cartService.Items()[1].Quantity, 2);
        Assert.AreEqual(cartService.Items()[1].Price, 99.99m);
        Assert.AreEqual(cartService.TotalSalesTaxAmount(), 35.00m);
        Assert.AreEqual(cartService.TotalPrice(), 314.96m);
      }
  }
}