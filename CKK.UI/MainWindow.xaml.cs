﻿using CKK.Logic.Interfaces;
using CKK.Logic.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Xaml;

namespace CKK.UI
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {

      public MainWindow()
      {
         InitializeComponent();
      }

      private void loginButton_Click(object sender, RoutedEventArgs e)
      {
         Login login = new Login();
         login.Left = this.Left;
         login.Top = this.Top;

         login.Show();
         this.Close();
      }

      private void signUpButton_Click(object sender, RoutedEventArgs e)
      {
         SignUp signUp = new SignUp();
         signUp.Left = this.Left;
         signUp.Top = this.Top;

         signUp.Show();
         this.Close();
      }
   }
}
