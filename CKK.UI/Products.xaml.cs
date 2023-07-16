using CKK.Logic.Interfaces;
using CKK.Logic.Models;
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

namespace CKK.UI
{
   /// <summary>
   /// Interaction logic for Products.xaml
   /// </summary>
   public partial class Products : Window
   {
      public ObservableCollection<StoreItem> _Items { get; } = new ObservableCollection<StoreItem>();
      private ListView lwInventoryList;
      private IStore _Store;
      private EditProductsUserControl editUserControl;

      public Products()
      {
         InitializeComponent();
         CreateListView();

         _Store = new Store();

         editUserControl = new EditProductsUserControl(_Store);
         editUserControl.EditingComplete += EditUserControl_HandleEditingComplete;
         editUserControl.AddingComplete += EditUserControl_HandleEditingComplete;
         editUserControl.RemovingComplete += EditUserControl_HandleEditingComplete;

         RefreshListView();
         DisplayListView();
      }

      private void EditUserControl_HandleEditingComplete(object sender, EventArgs e)
      {
         RefreshListView();
         DisplayListView();
      }

      private void CreateListView()
      {
         lwInventoryList = new ListView()
         {
            ItemsSource = _Items,
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
         // Get the selecte ditem from the SortComboBox
         var selecteditem = (ComboBoxItem)SortComboBox.SelectedItem;

         // Get the selected value from the ComboBox
         var selectedSort = selecteditem.Content.ToString();

         // Apply sorting based on the selected value
         switch (selectedSort)
         {
            case "Name":
               SortByName();
               break;
            case "ID":
               SortByID();
               break;
            default:
               break;
         }
      }

      private void SortByName()
      {
         var sortedItems = _Items.OrderBy(item => item.Product.Name).ToList();
         UpdateListViewItems(sortedItems);
      }

      private void SortByID()
      {
         var sortedItems = _Items.OrderBy(item => item.Product.Id).ToList();
         UpdateListViewItems(sortedItems);
      }

      private void UpdateListViewItems(List<StoreItem> sortedItems)
      {
         _Items.Clear();
         foreach (var item in sortedItems)
         {
            _Items.Add(item);
         }
         DisplayListView();
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
         if (DisplayControl.Content != null)
         {
            DisplayControl.Content = null;
         }

         DisplayControl.Content = editUserControl;
      }

      private void DisplayListView()
      {
         if (DisplayControl.Content != null)
         {
            DisplayControl.Content = null;
         }
         DisplayControl.Content = lwInventoryList;
         lwInventoryList.Visibility = Visibility.Visible;
      }

      public void RefreshListView()
      {
         _Items.Clear();

         foreach (var storeItem in _Store.GetStoreItems())
         {
            _Items.Add(storeItem);
         }
      }
   }
}
