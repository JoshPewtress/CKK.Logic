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
using System.Windows.Shapes;

namespace CKK.UI
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : Window
    {
        public SignUp()
        {
            InitializeComponent();
        }

      private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         MainWindow mainWindow = new MainWindow();
         mainWindow.Left = this.Left;
         mainWindow.Top = this.Top;

         mainWindow.Show();
         this.Close();
      }

      private void signUpButton_Click(object sender, RoutedEventArgs e)
      {
         Login login = new Login();
         login.Left = this.Left;
         login.Top = this.Top;

         login.Show();
         this.Close();
      }
   }
}
