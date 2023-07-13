using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CKK.UI
{
   /// <summary>
   /// Interaction logic for AddProductUserControl.xaml
   /// </summary>
   public partial class AddProductUserControl : UserControl
   {
      private IStore store;

      public event Action OnEditingComplete;

      public AddProductUserControl(IStore store)
      {
         InitializeComponent();
         this.store = store;
      }

      private void ConfirmButton_Click(object sender, RoutedEventArgs e)
      {
         int productId = int.Parse(ProductIdTextBox.Text);
         string productName = ProductNameTextBox.Text;
         decimal productPrice = decimal.Parse(ProductPriceTextBox.Text);
         int productQuantity = int.Parse(ProductQuantityTextBox.Text);

         Product product = new Product()
         {
            Id = productId,
            Name = productName,
            Price = productPrice,
         };

         store.AddStoreItem(product, productQuantity);

         OnEditingComplete?.Invoke();
      }
   }
}
