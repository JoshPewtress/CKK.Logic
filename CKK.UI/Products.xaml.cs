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
      IConnectionFactory conn = new DatabaseConnectionFactory();
      IUnitOfWork _unitOfWork;

      public Products()
      {
         InitializeComponent();

         _unitOfWork = new UnitOfWork(conn);
         PopulateListView();
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
      
      private void ToggleVisibility()
      {
         ClearFields();
         PopulateListView();
         ProductListView.Visibility = Visibility.Visible;
         editPanel.Visibility = Visibility.Collapsed;
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

      private void addButton_Click(object sender, RoutedEventArgs e)
      {
         Product newProduct = new Product
         {
            Price = decimal.Parse(priceTextBox.Text),
            Quantity = int.Parse(quantityTextBox.Text),
            Name = nameTextBox.Text
         };

         _unitOfWork.Products.Add(newProduct);

         ToggleVisibility();
      }

      private void removeButton_Click(object sender, RoutedEventArgs e)
      {
         var toDelete = int.Parse(idTextBox.Text);
         _unitOfWork.Products.Delete(toDelete);

         ToggleVisibility();
      }

      private void updateButton_Click(object sender, RoutedEventArgs e)
      {
         var toUpdate = new Product
         {
            Id = int.Parse(idTextBox.Text),
            Price = decimal.Parse(priceTextBox.Text),
            Quantity = int.Parse(quantityTextBox.Text),
            Name = nameTextBox.Text
         };

         _unitOfWork.Products.Update(toUpdate);

         ToggleVisibility();
      }

      private void SortComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
      {
         ComboBoxItem selectedComboBoxItem = SortComboBox.SelectedItem as ComboBoxItem;

         if (selectedComboBoxItem != null)
         {
            string selectedItemContent = selectedComboBoxItem.Content.ToString();

            if (selectedItemContent == "Name")
            {
               ProductListView.ItemsSource = _unitOfWork.Products.GetAll().OrderBy(x => x.Name).ToList();   
            }
            else if (selectedItemContent == "ID")
            {
               ProductListView.ItemsSource = _unitOfWork.Products.GetAll().OrderBy(x => x.Id).ToList();
            }
         }
      }

		private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
		{
            string searchText = SearchBox.Text.ToLower();

            var filteredItems = _unitOfWork.Products.GetAll()
                .Where(p => p.Name.ToLower().Contains(searchText) || p.Id.ToString().Contains(searchText))
                .ToList();

            ProductListView.ItemsSource = filteredItems;
		}
	}
}
