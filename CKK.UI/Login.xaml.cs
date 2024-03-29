﻿using System;
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
using System.Windows.Shapes;
using CKK.Logic.Interfaces;
using CKK.Logic.Models;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {

        public Login()
        {
            InitializeComponent();
        }

      private void logoImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         MainWindow mainWindow = new MainWindow();
         mainWindow.Left = this.Left;
         mainWindow.Top = this.Top;

         mainWindow.Show();
         this.Close();
      }

      private void loginButton_Click(object sender, RoutedEventArgs e)
      {
         Home home = new Home();
         home.Left = this.Left;
         home.Top = this.Top;

         home.Show();
         this.Close();
      }
   }
}
