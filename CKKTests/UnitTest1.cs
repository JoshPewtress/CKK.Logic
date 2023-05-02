using CKK.Logic.Models;

namespace CKKTests
{
   public class UnitTest1
   {
      [Fact]
      public void AddingProduct()
      {
         // Assemble
         Customer cust = new Customer();
         ShoppingCart cart = new ShoppingCart(cust);
         Product juice = new Product();
         juice.SetId(1);
         juice.SetName("Juice");
         juice.SetPrice(10.89M);
         string expected = "Juice";

         // Act
         cart.AddProduct(juice);
         string actual = juice.GetName();

         // Assert
         Assert.Equal(expected, actual);
      }

      [Fact]
      public void RemoveProduct()
      {
         // Assemble
         Customer cust = new Customer();
         ShoppingCart cart = new ShoppingCart(cust);
         Product juice = new Product();
         juice.SetId(1);
         juice.SetName("Juice");
         juice.SetPrice(9.99M);
         cart.AddProduct(juice, 2);
         int expected = 1;

         // Act
         cart.RemoveProduct(juice, 1);
         int actual = cart.GetProductById(1).GetQuantity();
         

         // Assert
         Assert.Equal(expected, actual);
      }

      [Fact]
      public void GetTotal()
      {
         // Assemble
         Customer cust = new Customer();
         ShoppingCart cart = new ShoppingCart(cust);
         Product juice = new Product();
            juice.SetId(1);
            juice.SetName("Juice");
            juice.SetPrice(2M);
         Product milk = new Product();
            milk.SetId(2);
            milk.SetName("Milk");
            milk.SetPrice(3M);
         Product soda = new Product();
            soda.SetId(3);
            soda.SetName("Soda");
            soda.SetPrice(4M);
         cart.AddProduct(juice, 4);
         cart.AddProduct(milk, 2);
         cart.AddProduct(soda, 1);

         decimal expected = 18M;

         // Act
         decimal actual = cart.GetTotal();

         // Assert
         Assert.Equal(expected, actual);

      }
   }
}