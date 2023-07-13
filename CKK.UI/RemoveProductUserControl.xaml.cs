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
   /// Interaction logic for RemoveProductUserControl.xaml
   /// </summary>
   public partial class RemoveProductUserControl : UserControl
   {
      private IStore store;

      public event Action OnEditingComplete;

      public RemoveProductUserControl(IStore store)
      {
         InitializeComponent();
         this.store = store;
      }

      private void ConfirmButton_Click(object sender, RoutedEventArgs e)
      {
         int productId = int.Parse(ProductIdTextBox.Text);
         int productQuantity = int.Parse(QuantityTextBox.Text);

         store.RemoveStoreItem(productId, productQuantity);

         OnEditingComplete?.Invoke();
      }
   }
}
