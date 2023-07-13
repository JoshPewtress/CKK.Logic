using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CKK.UI
{
   /// <summary>
   /// Interaction logic for Products.xaml
   /// </summary>
   public partial class Products : Window
   {
      private ObservableCollection<StoreItem> productsList;
      private ListView lwInventoryList;
      private IStore store;

      public Products()
      {
         InitializeComponent();
         CreateListView();

         store = new Store();

         RefreshListView();
      }

      private void CreateListView()
      {
         productsList = new ObservableCollection<StoreItem>();
         lwInventoryList = new ListView()
         {
            ItemsSource = productsList,
            Background = Brushes.LightGray,
            Margin = new Thickness(10),
            BorderThickness = new Thickness(1),
            BorderBrush = Brushes.Black,
            FontSize = 17
         };

         GridView gridView = new GridView();

         GridViewColumn nameColumn = new GridViewColumn()
         {
            Header = "Name",
            DisplayMemberBinding = new Binding("Product.Name")
         };
         gridView.Columns.Add(nameColumn);

         GridViewColumn priceColumn = new GridViewColumn()
         {
            Header = "Price",
            DisplayMemberBinding = new Binding("Product.Price")
         };
         gridView.Columns.Add(priceColumn);

         GridViewColumn quantityColumn = new GridViewColumn()
         {
            Header = "Quantity",
            DisplayMemberBinding = new Binding("Quantity")
         };
         gridView.Columns.Add(quantityColumn);

         GridViewColumn idColumn = new GridViewColumn()
         {
            Header = "Id",
            DisplayMemberBinding = new Binding("Product.Id")
         };
         gridView.Columns.Add(idColumn);

         lwInventoryList.View = gridView;

         Grid.SetRow(lwInventoryList, 4);
         Grid.SetColumnSpan(lwInventoryList, 3);

         DisplayControl.Content = lwInventoryList;
      }

      private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         // Sorting logic here
      }

      private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         Home home = new Home();
         home.Left = Left;
         home.Top = Top;

         home.Show();
         Close();
      }

      private void EditButton_Click(object sender, RoutedEventArgs e)
      {
         if (lwInventoryList.Visibility == Visibility.Visible)
         {
            lwInventoryList.Visibility = Visibility.Collapsed;
            DisplayEditArea();
         }
         else
         {
            lwInventoryList.Visibility = Visibility.Visible;
            DisplayListView();
         }
      }

      private void DisplayEditArea()
      {
         var editProductsUC = new EditProductsUserControl(store);
         editProductsUC.EditingComplete += OnEditingComplete;

         if (DisplayControl.Content != null)
         {
            DisplayControl.Content = null;
         }

         DisplayControl.Content = editProductsUC;
      }

      private void DisplayListView()
      {
         DisplayControl.Content = lwInventoryList;
      }

      public void RefreshListView()
      {
         productsList.Clear();

         foreach (var storeItem in store.GetStoreItems())
         {
            productsList.Add(storeItem);
         }
      }

      private void OnEditingComplete(object sender, RoutedEventArgs e)
      {
         RefreshListView();
         lwInventoryList.Visibility = Visibility.Visible;
      }
   }
}
