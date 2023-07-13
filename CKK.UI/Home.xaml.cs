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
using System.Windows.Shapes;

namespace CKK.UI
{
   /// <summary>
   /// Interaction logic for Home.xaml
   /// </summary>
   public partial class Home : Window
   {
      private int currentImageIndex = 1;

      public Home()
      {
         InitializeComponent();
         SetTestimonialImage(currentImageIndex);
      }

      private void testimonialImage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         currentImageIndex = (currentImageIndex % 3) + 1;
         SetTestimonialImage(currentImageIndex);
      }

      private void SetTestimonialImage(int index)
      {
         string imagePath = $"Images/Testimonial{index}.png";
         testimonialImage.Source = new BitmapImage(new Uri(imagePath, UriKind.Relative));
      }

      private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
      {
         Products products = new Products();
         products.Left = this.Left;
         products.Top = this.Top;
         
         products.Show();
         this.Close();
      }
   }
}
