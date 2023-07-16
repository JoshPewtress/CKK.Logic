using CKK.Logic.Interfaces;
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
   /// Interaction logic for EditProductsUserControl.xaml
   /// </summary>
   public partial class EditProductsUserControl : UserControl
   {
      private IStore _Store;

      public event EventHandler EditingComplete;
      public event EventHandler AddingComplete;
      public event EventHandler RemovingComplete;

      public EditProductsUserControl(IStore store)
      {
         InitializeComponent();
         this._Store = store;
      }

      private void AddProductButton_Click(object sender, RoutedEventArgs e)
      {
         var addProductUC = new AddProductUserControl(_Store);
         addProductUC.EditingComplete += AddProductUC_EditingComplete;
         
         if (DisplayFrame.Content != null)
         {
            DisplayFrame.Content = null;
         }

         DisplayFrame.Content = addProductUC;
      }

      private void RemoveProductButton_Click(object sender, RoutedEventArgs e)
      {
         var removeProductUC = new RemoveProductUserControl(_Store);
         removeProductUC.EditingComplete += RemoveProductUC_EditingComplete;

         if (DisplayFrame.Content != null)
         {
            DisplayFrame.Content = null;
         }

         DisplayFrame.Content = removeProductUC;
      }

      private void AddProductUC_EditingComplete(object sender, EventArgs e)
      {
         AddingComplete?.Invoke(this, EventArgs.Empty);
      }

      private void RemoveProductUC_EditingComplete(Object sender, EventArgs e)
      {
         RemovingComplete?.Invoke(this, EventArgs.Empty);
      }

      private void OnEditingComplete(object sender, RoutedEventArgs e)
      {
         EditingComplete?.Invoke(this, EventArgs.Empty);
      }
   }
}
