using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using CKK.Persistance.Models;
using CKK.DB.UOW;
using System;
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
using CKK.DB.Interfaces;

namespace CKK.UI
{
   /// <summary>
   /// Interaction logic for Products.xaml
   /// </summary>
   public partial class Products : Window
   {
      private IStore _Store;
      IConnectionFactory conn = new DatabaseConnectionFactory();
      IUnitOfWork _unitOfWork;

      public Products()
      {
         InitializeComponent();

         _Store = new FileStore();
         _unitOfWork = new UnitOfWork(conn);
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
         if (ProductListView.Visibility == Visibility.Visible)
         {
            // Switch to edit mode
            ProductListView.Visibility = Visibility.Collapsed;
            editPanel.Visibility = Visibility.Visible;
            ClearFields();
         }
         else
         {
            // Switch to view mode
            ProductListView.Visibility = Visibility.Visible;
            editPanel.Visibility = Visibility.Collapsed;
            ClearFields();
         }
      }

      public void ClearFields()
      {
         // Clear EditButton TextBox's
         nameTextBox.Text = string.Empty;
         idTextBox.Text = string.Empty;
         priceTextBox.Text = string.Empty;
         quantityTextBox.Text = string.Empty;
      }

      private void addButton_Click(object sender, RoutedEventArgs e)
      {
         Product newProduct = new Product
         {
            Id = int.Parse(idTextBox.Text),
            Price = decimal.Parse(priceTextBox.Text),
            Quantity = int.Parse(quantityTextBox.Text),
            Name = nameTextBox.Text
         };

         _unitOfWork.Products.Add(newProduct);
         ClearFields();
         PopulateListView();
      }

      private void PopulateListView()
      {
         var products = _unitOfWork.Products.GetAll();

         // Create a list of Product objects by extracting fields from products
         var productViewModels = products.Select(p => new Product
         {
            Id = p.Id,
            Price = p.Price,
            Quantity = p.Quantity,
            Name = p.Name
         }).ToList();

         ProductListView.ItemsSource = productViewModels;
      }










      //private void EditUserControl_HandleEditingComplete(object sender, EventArgs e)
      //{
      //   RefreshListView();
      //   DisplayListView();
      //}

      //private void CreateListView()
      //{
      //   lwInventoryList = new ListView()
      //   {
      //      ItemsSource = _Items,
      //      Background = Brushes.LightGray,
      //      Margin = new Thickness(10),
      //      BorderThickness = new Thickness(1),
      //      BorderBrush = Brushes.Black,
      //      FontSize = 17
      //   };

      //   GridView gridView = new GridView();

      //   GridViewColumn nameColumn = new GridViewColumn()
      //   {
      //      Header = "Name",
      //      DisplayMemberBinding = new Binding("Product.Name")
      //   };
      //   gridView.Columns.Add(nameColumn);

      //   GridViewColumn priceColumn = new GridViewColumn()
      //   {
      //      Header = "Price",
      //      DisplayMemberBinding = new Binding("Product.Price")
      //   };
      //   gridView.Columns.Add(priceColumn);

      //   GridViewColumn quantityColumn = new GridViewColumn()
      //   {
      //      Header = "Quantity",
      //      DisplayMemberBinding = new Binding("Quantity")
      //   };
      //   gridView.Columns.Add(quantityColumn);

      //   GridViewColumn idColumn = new GridViewColumn()
      //   {
      //      Header = "Id",
      //      DisplayMemberBinding = new Binding("Product.Id")
      //   };
      //   gridView.Columns.Add(idColumn);

      //   lwInventoryList.View = gridView;

      //   Grid.SetRow(lwInventoryList, 4);
      //   Grid.SetColumnSpan(lwInventoryList, 3);

      //   DisplayControl.Content = lwInventoryList;
      //}

      //private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
      //{
      //   // Get the selecte ditem from the SortComboBox
      //   var selecteditem = (ComboBoxItem)SortComboBox.SelectedItem;

      //   // Get the selected value from the ComboBox
      //   var selectedSort = selecteditem.Content.ToString();

      //   // Apply sorting based on the selected value
      //   switch (selectedSort)
      //   {
      //      case "Name":
      //         SortByName();
      //         break;
      //      case "ID":
      //         SortByID();
      //         break;
      //      default:
      //         break;
      //   }
      //}

      //private void SortByName()
      //{
      //   var sortedItems = _Items.OrderBy(item => item.Product.Name).ToList();
      //   UpdateListViewItems(sortedItems);
      //}

      //private void SortByID()
      //{
      //   var sortedItems = _Items.OrderBy(item => item.Product.Id).ToList();
      //   UpdateListViewItems(sortedItems);
      //}

      //private void UpdateListViewItems(List<StoreItem> sortedItems)
      //{
      //   _Items.Clear();
      //   foreach (var item in sortedItems)
      //   {
      //      _Items.Add(item);
      //   }
      //   DisplayListView();
      //}

      //private void EditButton_Click(object sender, RoutedEventArgs e)
      //{
      //   if (lwInventoryList.Visibility == Visibility.Visible)
      //   {
      //      lwInventoryList.Visibility = Visibility.Collapsed;
      //      DisplayEditArea();
      //   }
      //   else
      //   {
      //      lwInventoryList.Visibility = Visibility.Visible;
      //      DisplayListView();
      //   }
      //}

      //private void DisplayEditArea()
      //{
      //   if (DisplayControl.Content != null)
      //   {
      //      DisplayControl.Content = null;
      //   }

      //   DisplayControl.Content = editUserControl;
      //}

      //private void DisplayListView()
      //{
      //   if (DisplayControl.Content != null)
      //   {
      //      DisplayControl.Content = null;
      //   }
      //   DisplayControl.Content = lwInventoryList;
      //   lwInventoryList.Visibility = Visibility.Visible;
      //}

      //public void RefreshListView()
      //{
      //   _Items.Clear();

      //   foreach (var storeItem in _Store.GetStoreItems())
      //   {
      //      _Items.Add(storeItem);
      //   }
      //}

      //private void SearchBox_KeyUp(object sender, KeyEventArgs e)
      //{
      //   if (e.Key == Key.Enter)
      //   {
      //      string searchTerm = SearchBox.Text.Trim();

      //      // Call GetAllProductsByName method to get the search results
      //      List<StoreItem> searchResults = _Store.GetAllProductsByName(searchTerm);

      //      if (searchResults.Any())
      //      {
      //         string message = "Search Results:\n\n";
      //         foreach (var item in searchResults)
      //         {
      //            message += $"{item.Product.Name} - Price: {item.Product.Price:C} - Quantity: {item.Quantity}\n";
      //         }
      //         MessageBox.Show(message, "Search Results", MessageBoxButton.OK, MessageBoxImage.Information);
      //      }
      //      else
      //      {
      //         MessageBox.Show("No matching items found.", "Search Results", MessageBoxButton.OK, MessageBoxImage.Information);
      //      }
      //   }
      //}
   }
}
