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
      private IStore store;

      public event EventHandler EditingComplete;

      public EditProductsUserControl(IStore store)
      {
         InitializeComponent();
         this.store = store;
      }

      private void AddProductButton_Click(object sender, RoutedEventArgs e)
      {
         var addProductUC = new AddProductUserControl(store);
         addProductUC.ProductAdded
         
         if (DisplayFrame.Content != null)
         {
            DisplayFrame.Content = null;
         }

         DisplayFrame.Content = addProductUC;
      }

      private void RemoveProductButton_Click(object sender, RoutedEventArgs e)
      {
         var removeProductUC = new RemoveProductUserControl(store);
         removeProductUC.OnEditingComplete += HandleEditingComplete;

         if (DisplayFrame.Content != null)
         {
            DisplayFrame.Content = null;
         }

         DisplayFrame.Content = removeProductUC;
      }

      private void HandleEditingComplete()
      {
         OnEditingComplete?.Invoke();
      }
   }
}
